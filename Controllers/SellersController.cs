using Microsoft.AspNetCore.Mvc;
using ProjetoMVC.Models;
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

        //chama a pagina Create
        public IActionResult Create()
        {
            return View();
        }

        //Pega os dados da pagina Create
        [HttpPost]
        [ValidateAntiForgeryToken] //previne ataque CSRF
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit()
        {
            return View();
        }   
    }
}
