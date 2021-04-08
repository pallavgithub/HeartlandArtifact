namespace HeartlandArtifact.Models
{
    public class ItemMarkAsSoldModel
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int PerceivedValue { get; set; }
        public int Cost { get; set; }
        public int SoldPrice { get; set; }
        public string SoldDate { get; set; }
    }
}
