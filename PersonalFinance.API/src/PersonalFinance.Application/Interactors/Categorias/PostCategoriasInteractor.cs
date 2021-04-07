using PersonalFinance.Application.Repositories;
using PersonalFinance.Domain.Entities;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Interactors
{
    public class PostCategoriasInteractor
    {
        private readonly CategoriaRepository _CategoriaRepository;

        public PostCategoriasInteractor(CategoriaRepository categoriaRepository)
        {
            _CategoriaRepository = categoriaRepository;
        }

        public async Task<bool> AddCategoriaAsync(Categoria categoria)
        {
            //var ContaDB = Mapper.Map<Conta>(Conta);

            return await _CategoriaRepository.AddAsync(categoria);
        }
    }
}
