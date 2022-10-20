using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web.Controllers
{
    public class SalesController : Controller
    {
        SaleService _saleService;
        public SalesController(SaleService saleService)
        {
            _saleService = saleService;
        }
        public async Task<IActionResult> Inventory()
        {
            
            var invents = await _saleService.GetInventories();

            return Json(invents);
        }
    }
}
