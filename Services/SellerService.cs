using ProjetoMVC.Data;
using ProjetoMVC.Models;
using ProjetoMVC.Models.ModelViews;

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

        public void Edit(Seller seller)
        {
            _contexto.Update(seller);
            _contexto.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _contexto.Sellers.FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _contexto.Sellers.Find(id);
            _contexto.Sellers.Remove(obj);
            _contexto.SaveChanges();
        }
    }
}