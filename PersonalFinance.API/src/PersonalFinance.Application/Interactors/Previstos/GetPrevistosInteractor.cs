using PersonalFinance.Application.Repositories;
using PersonalFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Interactors
{
    public class GetPrevistosInteractor
    {
        private readonly PrevistoRepository _PrevistoRepository;

        public GetPrevistosInteractor(PrevistoRepository previstoRepository)
        {
            _PrevistoRepository = previstoRepository;
        }

        public async Task<IEnumerable<Previsto>> GetPrevistosAsync()
        {
            var Previstos = new List<Previsto>();

            //var Categorias = await _CategoriaRepository.GetAllAsync();

            Previstos.AddRange(await GetPeriodo(DateTime.Today.Year, DateTime.Today.Month));
            Previstos.AddRange(await GetPeriodo(DateTime.Today.Year, null));
            Previstos.AddRange(await GetPeriodo(null, DateTime.Today.Month));
            Previstos.AddRange(await GetPeriodo(null, null));

            return Previstos;
        }

        public async Task<Previsto> GetPrevistoAsync(Guid id)
        {
            return await _PrevistoRepository.GetByIdAsync(id);
        }

        private async Task<IEnumerable<Previsto>> GetPeriodo(int? ano, int? mes)
        {
            var Previstos = (await _PrevistoRepository
                .GetByPeriodoAsync(ano, mes))
                .OrderBy(a => a.Ano)
                .ThenBy(a => a.Mes)
                .ToList();

            //foreach (var Previsto in Previstos)
            //{
            //    Previsto.CategoriaDescricao = Categorias
            //        .SingleOrDefault(a => a.CategoriaId == Previsto.CategoriaId)?.Descricao;
            //}

            return Previstos;
        }
    }
}
