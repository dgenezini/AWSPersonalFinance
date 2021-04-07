using ExcelDataReader;
using PersonalFinance.Application.Repositories;
using PersonalFinance.Domain.Entities;
using PersonalFinance.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Services.Itau
{
    public class ItaucardExcelServices : IImportTransactionsService
    {
        private readonly int COLUNA_DATA = 0;
        private readonly int COLUNA_LOCAL = 1;
        private readonly int COLUNA_VALOR = 3;

        public string Description { get; } = "Itaúcard Excel";

        private readonly ContaRepository _ContaRepository;

        public ItaucardExcelServices(ContaRepository contaRepository)
        {
            _ContaRepository = contaRepository;
        }

        public async Task<IEnumerable<Transacao>> GetTransactionsAsync(Stream stream)
        {
            List<Transacao> Transacoes = new List<Transacao>();

            var Contas = await _ContaRepository.GetAllAsync();

            stream.Position = 0;

            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                string finalCartao = null;
                var CulturePtBr = CultureInfo.GetCultureInfo("pt-BR");

                do
                {
                    while (reader.Read())
                    {
                        if (reader.GetValue(COLUNA_DATA)?.ToString()?.IndexOf("(titular)", StringComparison.OrdinalIgnoreCase) > 0)
                        {
                            var PosStart = reader.GetValue(COLUNA_DATA).ToString().IndexOf("(titular)") - 5;

                            finalCartao = reader.GetValue(COLUNA_DATA).ToString().Substring(PosStart, 4);
                        }
                        else if (reader.GetValue(COLUNA_LOCAL)?.ToString()?.Equals("dólar de conversão", StringComparison.OrdinalIgnoreCase) ?? false)
                        {
                            var Data = DateTime.Parse(reader.GetValue(COLUNA_DATA).ToString(), CulturePtBr, DateTimeStyles.None);

                            //string ValorMoedaStr = reader.GetValue(COLUNA_VALOR).ToString().Replace("+", "").Trim();
                            //float ValorMoeda = Convert.ToSingle(ValorMoedaStr, CulturePtBr);

                            var ValorMoeda = (float)reader.GetDouble(COLUNA_VALOR);

                            reader.Read();

                            //string ValorStr = reader.GetValue(COLUNA_VALOR).ToString().Replace("+", "").Trim();
                            //float Valor = Convert.ToSingle(ValorStr, CulturePtBr);
                            var Valor = (float)reader.GetDouble(COLUNA_VALOR);

                            var ContaOrigem = Contas
                                .FirstOrDefault(a => a.FinalCartao == finalCartao);
                            var ContaDestino = Contas
                                .FirstOrDefault(a => a.Descricao == "Despesas");

                            var Transacao = new Transacao();
                            Transacao.DataHora = Data;
                            Transacao.LocalPagto = reader.GetValue(COLUNA_LOCAL).ToString().Trim();
                            Transacao.Moeda = "R$";
                            Transacao.Valor = Valor;
                            Transacao.Cotacao = ValorMoeda;

                            Transacao.ContaOrigemId = ContaOrigem?.ContaId;
                            Transacao.ContaDestinoId = ContaDestino?.ContaId;

                            Transacoes.Add(Transacao);
                        }
                        else if (DateTime.TryParse(reader.GetValue(COLUNA_DATA)?.ToString(), CulturePtBr, DateTimeStyles.None, out var Data) &&
                                 (!reader.GetValue(COLUNA_LOCAL)?.ToString().Equals("PAGAMENTO EFETUADO", StringComparison.OrdinalIgnoreCase) ?? true) &&
                                 !string.IsNullOrEmpty(reader.GetValue(COLUNA_VALOR)?.ToString()))
                        {
                            //string ValorStr = reader.GetValue(COLUNA_VALOR).ToString().Replace("+", "").Trim();
                            //float Valor = Convert.ToSingle(ValorStr, CulturePtBr);
                            var Valor = (float)reader.GetDouble(COLUNA_VALOR);

                            var ContaOrigem = Contas
                                .FirstOrDefault(a => a.FinalCartao == finalCartao);
                            var ContaDestino = Contas
                                .FirstOrDefault(a => a.Descricao == "Despesas");

                            var Transacao = new Transacao();
                            Transacao.DataHora = Data;
                            Transacao.LocalPagto = reader.GetValue(COLUNA_LOCAL).ToString().Trim();
                            Transacao.Moeda = "R$";
                            Transacao.Valor = Valor;

                            Transacao.ContaOrigemId = ContaOrigem?.ContaId;
                            Transacao.ContaDestinoId = ContaDestino?.ContaId;

                            Transacoes.Add(Transacao);
                        }
                    }
                } while (reader.NextResult());

                return await Task.FromResult(Transacoes);
            }
        }
    }
}
