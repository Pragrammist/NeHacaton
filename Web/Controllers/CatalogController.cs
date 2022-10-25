﻿using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Web.Dtos.Sales.Inventory;
using Web.Models.Inventory;
using Web.Services;


namespace Web.Controllers
{
    [EnableCors]
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
        public async Task<IActionResult> Inventories([FromBody]InventorySearchModel? search = null)
        {
            var inputData = _mapper.Map<InputSearchInventoryDto>(search);
            var inventories = await _saleService.GetInventories(inputData).ToListAsync();
            return Json(inventories);
        }
    }
}
