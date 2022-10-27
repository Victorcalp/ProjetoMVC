using Microsoft.AspNetCore.Mvc;
using ProjetoMVC.Services;

namespace ProjetoMVC.Controllers
{
    public class SalesController : Controller
    {
        private readonly SalesService _salesService;
        public SalesController(SalesService salesService)
        {
            _salesService = salesService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearchAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = await _salesService.SimpleSearchAsync(minDate, maxDate);
            return View(result);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            var result = await _salesService.GroupingSearchAsync(minDate, maxDate);
            return View(result);
        }
    }
}
