using Microsoft.AspNetCore.Mvc;
using ProjetoMVC.Services;

namespace ProjetoMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }
        public IActionResult Index()
        {
            //chama o model, pega a lista e encaminha para a view
            var list = _sellerService.FindAll();
            return View(list);
        }
    }
}
