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

namespace PersonalFinance.Application.Services.Nubank
{
    public class NubankCSVServices : IImportTransactionsService
    {
        public string Description { get; } = "Nubank CSV";

        private readonly ContaRepository _ContaRepository;

        public NubankCSVServices(ContaRepository contaRepository)
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

                while (csv.Read())
                {
                    var Conta = Contas
                        .FirstOrDefault(a => a.Descricao == "Nubank");

                    var Valor = Convert.ToSingle(csv.GetField<string>("amount"), CultureInfo.InvariantCulture);

                    var Transacao = new Transacao();
                    Transacao.DataHora = csv.GetField<DateTime>("date");
                    Transacao.LocalPagto = csv.GetField<string>("title").Trim();
                    Transacao.Moeda = "R$";
                    Transacao.Valor = Valor;

                    if (Valor > 0)
                    {
                        var ContaDespesas = Contas
                            .FirstOrDefault(a => a.Descricao == "Despesas");

                        Transacao.ContaOrigemId = Conta?.ContaId;
                        Transacao.ContaDestinoId = ContaDespesas?.ContaId;
                    }
                    else
                    {
                        Transacao.ContaDestinoId = Conta?.ContaId;
                    }

                    Transacoes.Add(Transacao);
                }

                return await Task.FromResult(Transacoes);
            }
        }
    }
}
