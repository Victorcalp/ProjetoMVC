using Microsoft.EntityFrameworkCore;
using ProjetoMVC.Data;
using ProjetoMVC.Models;

namespace ProjetoMVC.Services
{
    public class DepartmentService
    {
        private readonly Contexto _contexto;

        public DepartmentService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Department>> FiendAllAsync()
        {
            return await _contexto.Departments.OrderBy(x => x.Name).ToListAsync();
        }
    }
}