using Microsoft.EntityFrameworkCore;
using ProjetoMVC.Data;
using ProjetoMVC.Models;
using ProjetoMVC.Models.ModelViews;
using ProjetoMVC.Services.Exception;

namespace ProjetoMVC.Services
{
    public class SellerService
    {
        private readonly Contexto _contexto;

        public SellerService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            //vai retornar uma lista de usuarios do BD
            return await _contexto.Sellers.ToListAsync();
        }
        public async Task Insert(Seller seller)
        {
            _contexto.Add(seller);
            await _contexto.SaveChangesAsync();
        }
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _contexto.Sellers.Include(x => x.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _contexto.Sellers.FindAsync(id);
            _contexto.Sellers.Remove(obj);
            await _contexto.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller seller)
        {
            if (!await _contexto.Sellers.AnyAsync(x => x.Id == seller.Id))
            {
                throw new NotFoundException("ID não encontrado");
            }
            try
            {
                _contexto.Update(seller);
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException message)
            {
                throw new DbConcurrencyException(message.Message);
            }
        }
    }
}