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
    }
}
