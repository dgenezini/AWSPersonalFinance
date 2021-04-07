using PersonalFinance.Application.Services;
using PersonalFinance.Application.Services.Itau;
using PersonalFinance.Application.Services.Nubank;
using PersonalFinance.Domain.Entities;
using PersonalFinance.Domain.Exceptions;
using PersonalFinance.Domain.Values;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Interactors
{
    public class ImportarInteractor
    {
        private readonly ImportadorServices _ImportadorServices;
        private readonly ItaucardExcelServices _ItaucardExcelServices;
        private readonly ItauCSVServices _ItauCSVServices;
        private readonly NubankCSVServices _NubankCSVServices;

        public ImportarInteractor(ImportadorServices importadorServices,
            ItaucardExcelServices itaucardExcelServices,
            ItauCSVServices itauCSVServices,
            NubankCSVServices nubankCSVServices)
        {
            _ImportadorServices = importadorServices;
            _ItaucardExcelServices = itaucardExcelServices;
            _ItauCSVServices = itauCSVServices;
            _NubankCSVServices = nubankCSVServices;
        }

        public async Task ImportarAsync(ImportacaoTipo importacaoTipo, Stream arquivo)
        {
            if (arquivo == null)
            {
                throw new PersonalFinanceControlException("Tipo de importação necessita de arquivo");
            }

            IEnumerable<Transacao> transacoes;

            if (importacaoTipo == ImportacaoTipo.ItauCardExcel)
            {
                transacoes = await _ItaucardExcelServices.GetTransactionsAsync(arquivo);
            }
            else if (importacaoTipo == ImportacaoTipo.ItauCSV)
            {
                transacoes = await _ItauCSVServices.GetTransactionsAsync(arquivo);
            }
            else if (importacaoTipo == ImportacaoTipo.NubankCSV)
            {
                transacoes = await _NubankCSVServices.GetTransactionsAsync(arquivo);
            }
            else
            {
                throw new Exception("Invalid Import Transaction Type");
            }

            await _ImportadorServices.ImportarTransacoesAsync(transacoes);
        }
    }
}
