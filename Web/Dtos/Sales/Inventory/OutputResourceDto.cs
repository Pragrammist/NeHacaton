using Newtonsoft.Json;

namespace Web.Dtos.Sales.Inventory
{
    public class OutputResourceDto
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Body { get; set; }
        public DateTime DeletedAt { get; set; }
        public string? @Const { get; set; }
    }
}
