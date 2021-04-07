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
    public class CategoriasController : ControllerBase
    {
        private readonly GetCategoriasInteractor _GetCategoriasInteractor;
        private readonly PostCategoriasInteractor _PostCategoriasInteractor;
        private readonly PutCategoriasInteractor _PutCategoriasInteractor;
        private readonly DeleteCategoriasInteractor _DeleteCategoriasInteractor;

        public CategoriasController(GetCategoriasInteractor getCategoriasInteractor,
            PostCategoriasInteractor postCategoriasInteractor,
            PutCategoriasInteractor putCategoriasInteractor,
            DeleteCategoriasInteractor deleteCategoriasInteractor)
        {
            _GetCategoriasInteractor = getCategoriasInteractor;
            _PostCategoriasInteractor = postCategoriasInteractor;
            _PutCategoriasInteractor = putCategoriasInteractor;
            _DeleteCategoriasInteractor = deleteCategoriasInteractor;
        }

        [HttpGet]
        public async Task<IEnumerable<Categoria>> GetAsync()
        {
            return await _GetCategoriasInteractor.GetCategoriasAsync();
        }

        [HttpGet("{id}")]
        public async Task<Categoria> GetAsync(Guid id)
        {
            return await _GetCategoriasInteractor.GetCategoriaAsync(id);
        }

        [HttpPost]
        public async Task<bool> PostAsync(Categoria categoria)
        {
            return await _PostCategoriasInteractor.AddCategoriaAsync(categoria);
        }

        [HttpPut]
        public async Task<bool> PutAsync(Categoria categoria)
        {
            return await _PutCategoriasInteractor.UpdateCategoriaAsync(categoria);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _DeleteCategoriasInteractor.DeleteCategoriaAsync(id);
        }
    }
}
