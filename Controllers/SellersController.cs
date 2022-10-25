﻿using Microsoft.AspNetCore.Mvc;
using ProjetoMVC.Models;
using ProjetoMVC.Models.ModelViews;
using ProjetoMVC.Services;

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

        //chama a pagina Create
        public IActionResult Create()
        {
            //vai buscar no banco de dados todos os departamento
            var departments = _departmentService.FiendAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Seller seller)
        {
            _sellerService.Edit(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var del = _sellerService.FindById(id.Value);

            if (del == null)
            {
                return NotFound();
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
            if(id == null)
            {
                return NotFound();
            }

            var list = _sellerService.FindById(id.Value);

            if(list == null)
            {
                return NotFound();
            }

            return View(list);
        }
    }
}
