using CsvHelper;
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
    public class ItauCSVServices : IImportTransactionsService
    {
        public string Description { get; } = "Itaú CSV";

        private readonly ContaRepository _ContaRepository;

        public ItauCSVServices(ContaRepository contaRepository)
        {
            _ContaRepository = contaRepository;
        }

        public async Task<IEnumerable<Transacao>> GetTransactionsAsync(Stream stream)
        {
            List<Transacao> Transacoes = new List<Transacao>();

            var Contas = await _ContaRepository.GetAllAsync();

            string CsvContent;

            using (var reader = new StreamReader(stream))
            {
                CsvContent = reader.ReadToEnd();
            }

            using (TextReader textReader = new StringReader(CsvContent))
            {
                var csv = new CsvReader(textReader);
                csv.Configuration.Delimiter = ";";
                csv.Configuration.HasHeaderRecord = false;

                while (csv.Read())
                {
                    float Valor = Convert.ToSingle(csv.GetField<string>(2), CultureInfo.GetCultureInfo("pt-BR"));

                    var Conta = Contas
                        .SingleOrDefault(a => a.Descricao == "Itaú");

                    var Transacao = new Transacao();
                    Transacao.DataHora = Convert.ToDateTime(csv.GetField<string>(0), CultureInfo.GetCultureInfo("pt-BR"));
                    Transacao.LocalPagto = csv.GetField<string>(1).Trim();
                    Transacao.Moeda = "R$";

                    if (Valor < 0)
                    {
                        Transacao.ContaOrigemId = Conta?.ContaId;
                        Transacao.Valor = Valor * -1;
                    }
                    else
                    {
                        Transacao.ContaDestinoId = Conta?.ContaId;
                        Transacao.Valor = Valor;
                    }

                    Transacoes.Add(Transacao);
                }

                return Transacoes;
            }
        }
    }
}
