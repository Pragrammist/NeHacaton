using Newtonsoft.Json;

namespace Web.Dtos.Sales.Inventory
{
    public class OutputValueDto
    {
        public int Id { get; set; }
        public string Period { get; set; } = null!;
        public int Value { get; set; }
        public string MoreThen { get; set; } = null!;
        public bool IsFixed { get; set; }
    }
}
