using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Web.Models.Inventory
{
    public class InventorySearchModel
    {
        public string? Search { get; set; }

        public string[]? Tags { get; set; }
    }
}
