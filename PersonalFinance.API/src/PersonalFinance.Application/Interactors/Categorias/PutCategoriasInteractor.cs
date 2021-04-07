using PersonalFinance.Application.Repositories;
using PersonalFinance.Domain.Entities;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Interactors
{
    public class PutCategoriasInteractor
    {
        private readonly CategoriaRepository _CategoriaRepository;

        public PutCategoriasInteractor(CategoriaRepository categoriaRepository)
        {
            _CategoriaRepository = categoriaRepository;
        }

        public async Task<bool> UpdateCategoriaAsync(Categoria categoria)
        {
            //var ContaDB = await _ContaRepository
            //    .GetByIdAsync(conta.ContaId);

            //Mapper.Map(conta, ContaDB);

            return await _CategoriaRepository.UpdateAsync(categoria);
        }
    }
}
