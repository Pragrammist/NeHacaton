using Newtonsoft.Json;

namespace Web.Dtos.Sales.Inventory
{
    public class OutputValueDto
    {
        public int Id { get; set; }
        public string Period { get; set; }
        public string Value { get; set; }
        public string MoreThen { get; set; }
        public bool IsFixed { get; set; }
    }
}
