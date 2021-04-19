namespace HeartlandArtifact.Models
{
    public class ItemMarkAsSoldModel
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string PerceivedValue { get; set; }        
        public string Cost { get; set; }
        public int SoldPrice { get; set; }
        public string SoldDate { get; set; }
    }
    public class ApiSoldItemModel
    {
        public ItemMarkAsSoldModel SoldItemDetail { get; set; }
        public ApiItemModel ItemDetail { get; set; }
    }

}
