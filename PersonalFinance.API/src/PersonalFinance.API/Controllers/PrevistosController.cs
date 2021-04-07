
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
    public class PrevistosController : ControllerBase
    {
        private readonly GetPrevistosInteractor _GetPrevistosInteractor;
        private readonly PostPrevistosInteractor _PostPrevistosInteractor;
        private readonly PutPrevistosInteractor _PutPrevistosInteractor;
        private readonly DeletePrevistosInteractor _DeletePrevistosInteractor;

        public PrevistosController(GetPrevistosInteractor getPrevistosInteractor,
            PostPrevistosInteractor postPrevistosInteractor,
            PutPrevistosInteractor putPrevistosInteractor,
            DeletePrevistosInteractor deletePrevistosInteractor)
        {
            _GetPrevistosInteractor = getPrevistosInteractor;
            _PostPrevistosInteractor = postPrevistosInteractor;
            _PutPrevistosInteractor = putPrevistosInteractor;
            _DeletePrevistosInteractor = deletePrevistosInteractor;
        }

        [HttpGet]
        public async Task<IEnumerable<Previsto>> GetAsync(
            Guid? categoriaId, DateTime? periodo, bool dataPagamento, bool isSemCategoria,
            string filtro)
        {
            return await _GetPrevistosInteractor.GetPrevistosAsync();
        }

        [HttpGet("{id}")]
        public async Task<Previsto> GetAsync(Guid id)
        {
            return await _GetPrevistosInteractor.GetPrevistoAsync(id);
        }

        [HttpPost]
        public async Task<bool> PostAsync(Previsto previsto)
        {
            return await _PostPrevistosInteractor.AddContaAsync(previsto);
        }

        [HttpPut]
        public async Task<bool> PutAsync(Previsto previsto)
        {
            return await _PutPrevistosInteractor.UpdateContaAsync(previsto);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _DeletePrevistosInteractor.DeleteContaAsync(id);
        }
    }
}
