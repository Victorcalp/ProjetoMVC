using ProjetoMVC.Data;
using ProjetoMVC.Models;

namespace ProjetoMVC.Services
{
    public class SellerService
    {
        private readonly Contexto _contexto;

        public SellerService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public List<Seller> FindAll()
        {
            //vai retornar uma lista de usuarios do BD
            return _contexto.Sellers.ToList();
        }

        public void Insert(Seller seller)
        {
            _contexto.Add(seller);
            _contexto.SaveChanges();
        }
    }
}
