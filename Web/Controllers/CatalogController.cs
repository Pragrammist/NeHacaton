using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.Dtos.Sales.Inventory;
using Web.Models.Inventory;
using Web.Services;
using static Web.Helprers.ControllerExtensions;

namespace Web.Controllers
{
    public class CatalogController : Controller
    {
        SaleService _saleService;
        IMapper _mapper;
        public CatalogController(SaleService saleService, IMapper mapper)
        {
            _mapper = mapper;
            _saleService = saleService;
        }
        [HttpPost]
        public async Task<IActionResult> Inventories(CancellationToken cancellation, [FromBody]InventorySearchModel? search = null)
        {
            var inputData = GetInputSearchInvetory(search);
            var inventories = await _saleService.GetInventories(cancellation, inputData).ToArrayAsync();
            return Json(inventories);
        }

        InputSearchInventoryDto GetInputSearchInvetory(InventorySearchModel? search)
        {
            var inputData = _mapper.Map<InputSearchInventoryDto>(search);
            inputData.City = this.GetCityFromHttpContext();
            return inputData;
        }
    }
}
