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
    public class ContasController : ControllerBase
    {
        private readonly GetContasInteractor _GetContasInteractor;
        private readonly PostContasInteractor _PostContasInteractor;
        private readonly PutContasInteractor _PutContasInteractor;
        private readonly DeleteContasInteractor _DeleteContasInteractor;

        public ContasController(GetContasInteractor getContasInteractor,
            PostContasInteractor postContasInteractor,
            PutContasInteractor putContasInteractor,
            DeleteContasInteractor deleteContasInteractor)
        {
            _GetContasInteractor = getContasInteractor;
            _PostContasInteractor = postContasInteractor;
            _PutContasInteractor = putContasInteractor;
            _DeleteContasInteractor = deleteContasInteractor;
        }

        [HttpGet]
        public async Task<IEnumerable<Conta>> GetAsync()
        {
            return await _GetContasInteractor.GetContasAsync();
        }

        [HttpGet("{id}")]
        public async Task<Conta> GetAsync(Guid id)
        {
            return await _GetContasInteractor.GetContaAsync(id);
        }

        [HttpPost]
        public async Task<bool> PostAsync(Conta conta)
        {
            return await _PostContasInteractor.AddContaAsync(conta);
        }

        [HttpPut]
        public async Task<bool> PutAsync(Conta conta)
        {
            return await _PutContasInteractor.UpdateContaAsync(conta);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _DeleteContasInteractor.DeleteContaAsync(id);
        }
    }
}
