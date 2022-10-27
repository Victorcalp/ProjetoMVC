using Microsoft.EntityFrameworkCore;
using ProjetoMVC.Data;
using ProjetoMVC.Models;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjetoMVC.Services
{
    public class SalesService
    {
        private readonly Contexto _contexto;
        public SalesService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<SalesRecord>> SimpleSearchAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _contexto.SalesRecords select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            //fez o join com Seller e deparment
            return await result.Include(x => x.Seller).Include(x => x.Seller.Department).OrderByDescending(x => x.Date).ToListAsync();
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> GroupingSearchAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _contexto.SalesRecords select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            var search = await result.Include(x => x.Seller)
                               .Include(x => x.Seller.Department)
                               .OrderByDescending(x => x.Date)

                               .ToListAsync();

            return search
                .GroupBy(x => x.Seller.Department)
                .ToList();
        }
    }
}
