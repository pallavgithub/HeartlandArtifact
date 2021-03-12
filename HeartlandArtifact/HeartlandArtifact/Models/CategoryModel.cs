using System;

namespace HeartlandArtifact.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public int CollectionId { get; set; }
        public string CategoryName { get; set; }
        public int CreatorId { get; set; }
        public int ModifierId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
