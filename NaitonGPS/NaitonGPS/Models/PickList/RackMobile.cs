namespace NaitonGPS.Models
{
    public class RackMobile
    {
        public int StockRackId { get; set; }
        public string RackName { get; set; }
        public decimal QuantityInStock { get; set; }
        public int? PickListItemId { get; set; }
    }
}
