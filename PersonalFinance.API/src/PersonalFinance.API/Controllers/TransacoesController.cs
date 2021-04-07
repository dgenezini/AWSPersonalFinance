using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Application.Interactors;
using PersonalFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalFinance.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransacoesController : ControllerBase
    {
        private readonly GetTransacoesInteractor _GetTransacoesInteractor;
        private readonly PostTransacoesInteractor _PostTransacoesInteractor;
        private readonly PutTransacoesInteractor _PutTransacoesInteractor;
        private readonly DeleteTransacoesInteractor _DeleteTransacoesInteractor;

        public TransacoesController(GetTransacoesInteractor getTransacoesInteractor,
            PostTransacoesInteractor postTransacoesInteractor,
            PutTransacoesInteractor putTransacoesInteractor,
            DeleteTransacoesInteractor deleteTransacoesInteractor)
        {
            _GetTransacoesInteractor = getTransacoesInteractor;
            _PostTransacoesInteractor = postTransacoesInteractor;
            _PutTransacoesInteractor = putTransacoesInteractor;
            _DeleteTransacoesInteractor = deleteTransacoesInteractor;
        }

        [HttpGet]
        public async Task<IEnumerable<Transacao>> GetAsync(
            Guid? categoriaId, DateTime? periodo, bool? dataPagamento, bool? isSemCategoria,
            string filtro)
        {
            return await _GetTransacoesInteractor.GetContasAsync(categoriaId, periodo,
                dataPagamento ?? false, isSemCategoria ?? false, filtro);
        }

        [HttpGet("{id}")]
        public async Task<Transacao> GetAsync(Guid id)
        {
            return await _GetTransacoesInteractor.GetTransacaoAsync(id);
        }

        [HttpPost]
        public async Task<bool> PostAsync(Transacao transacao)
        {
            return await _PostTransacoesInteractor.AddTransacaoAsync(transacao);
        }

        [HttpPut]
        public async Task<bool> PutAsync(Transacao transacao)
        {
            return await _PutTransacoesInteractor.UpdateTransacaoAsync(transacao);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(Guid id, string sortKey)
        {
            return await _DeleteTransacoesInteractor.DeleteTransacaoAsync(id, sortKey);
        }
    }
}
