using Newtonsoft.Json;

namespace Web.Dtos.Sales.Inventory
{

   

    public class OutputPointDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Phone { get; set; }
        public string? PlaceText { get; set; }
    }


}
