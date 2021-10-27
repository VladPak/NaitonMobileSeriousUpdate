namespace NaitonGPS.Models
{
    public class PickListItem
    {
        public int PickListId { get; set; }
        public int PickListOrderDetailsId { get; set; }
        public int DeliveryOrderDetailsId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }        
        public string RackName { get; set; }
        public string Sizes { get; set; }
        public int? StockRackId { get; set; }
        public int StatusId { get; set; }
        public int? Sequence { get; set; }
        public string StatusColor 
        {
            get
            {
                return StatusId == 9 ? "#66a103" : "#F2F3F4";
            }
        }
    }
}
