using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.Dtos.Sales.Inventory;
using Web.Models.Inventory;
using Web.Services;

namespace Web.Controllers
{
    public class SalesController : Controller
    {
        SaleService _saleService;
        IMapper _mapper;
        public SalesController(SaleService saleService, IMapper mapper)
        {
            _mapper = mapper;
            _saleService = saleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetInventory([FromBody]InventorySearchModel? search = null)
        {
            var inputData = _mapper.Map<InputSearchInventoryDto>(search);
            var invents = await _saleService.GetInventories(inputData);
            

            return Json(invents);
        }
    }
}
