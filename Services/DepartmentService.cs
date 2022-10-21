using ProjetoMVC.Data;
using ProjetoMVC.Models;
using System.Linq;

namespace ProjetoMVC.Services
{
    public class DepartmentService
    {
        private readonly Contexto _contexto;

        public DepartmentService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public List<Department> FiendAll()
        {
            return _contexto.Departments.OrderBy(x =>x.Name).ToList();
        }
    }
}