using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoMVC.Models;
using ProjetoMVC.Models.ModelViews;
using ProjetoMVC.Services;
using ProjetoMVC.Services.Exception;
using System.Diagnostics;

namespace ProjetoMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            //chama o model, pega a lista e encaminha para a view
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            //vai buscar no banco de dados todos os departamento
            var departments = _departmentService.FiendAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //previne ataque CSRF
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not provided" });
            }

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not Found" });
            }

            //para povoar a pagina
            List<Department> departments = _departmentService.FiendAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return BadRequest();
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException obj)
            {
                return RedirectToAction(nameof(Error), new { message = obj.Message });
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not Found" });
            }

            var del = _sellerService.FindById(id.Value);

            if (del == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not Found" });
            }

            return View(del);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not Found" });
            }

            var list = _sellerService.FindById(id.Value);

            if (list == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not Found" });
            }

            return View(list);
        }
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}