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
    public class FormasPagamentoController : ControllerBase
    {
        private readonly GetFormasPagtoInteractor _GetFormasPagtoInteractor;
        private readonly PostFormasPagtoInteractor _PostFormasPagtoInteractor;
        private readonly PutFormasPagtoInteractor _PutFormasPagtoInteractor;
        private readonly DeleteFormasPagtoInteractor _DeleteFormasPagtoInteractor;

        public FormasPagamentoController(GetFormasPagtoInteractor getFormasPagtoInteractor,
            PostFormasPagtoInteractor postFormasPagtoInteractor,
            PutFormasPagtoInteractor putFormasPagtoInteractor,
            DeleteFormasPagtoInteractor deleteFormasPagtoInteractor)
        {
            _GetFormasPagtoInteractor = getFormasPagtoInteractor;
            _PostFormasPagtoInteractor = postFormasPagtoInteractor;
            _PutFormasPagtoInteractor = putFormasPagtoInteractor;
            _DeleteFormasPagtoInteractor = deleteFormasPagtoInteractor;
        }

        [HttpGet]
        public async Task<IEnumerable<FormaPagamento>> GetAsync()
        {
            return await _GetFormasPagtoInteractor.GetContasAsync();
        }

        [HttpGet("{id}")]
        public async Task<FormaPagamento> GetAsync(Guid id)
        {
            return await _GetFormasPagtoInteractor.GetContaAsync(id);
        }

        [HttpPost]
        public async Task<bool> PostAsync(FormaPagamento formaPagamento)
        {
            return await _PostFormasPagtoInteractor.AddContaAsync(formaPagamento);
        }

        [HttpPut]
        public async Task<bool> PutAsync(FormaPagamento formaPagamento)
        {
            return await _PutFormasPagtoInteractor.UpdateContaAsync(formaPagamento);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _DeleteFormasPagtoInteractor.DeleteContaAsync(id);
        }
    }
}
