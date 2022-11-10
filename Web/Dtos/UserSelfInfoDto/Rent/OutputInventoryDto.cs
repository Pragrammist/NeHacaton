using Newtonsoft.Json;

namespace Web.Dtos.UserSelfInfoDto.Rent
{
    public class OutputInventoryDto
    {
        public int Id { get; set; }
        public DateTime TimeEnd { get; set; }
        public DateTime TimeStart { get; set; }
        public int Sum { get; set; }       
        public int SumInventory { get; set; }
        public int SumBroken { get; set; }
        public int SumService { get; set; }
        public bool Finished { get; set; }
        public int SumAmountPayment { get; set; }
        public int SumOne { get; set; }
        public int Count { get; set; }
        public int ParentInventoryId { get; set; }
        public int SumDiscount { get; set; }
        public int SumTotal { get; set; }
        public int KitNumber { get; set; }
        public OutputInnerInventoryDto Inventory { get; set; } = null!;
        public OutputPriceDto Price { get; set; } = null!;
    }
}
