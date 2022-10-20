using Microsoft.EntityFrameworkCore;

namespace ProjetoMVC.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    }
}
