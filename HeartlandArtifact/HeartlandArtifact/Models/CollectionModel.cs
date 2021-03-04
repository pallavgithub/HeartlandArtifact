using System;

namespace HeartlandArtifact.Models
{
    public class CollectionModel
    {
        public int CollectionId { get; set; }
        public string CollectionName { get; set; }
        public int CreatorId { get; set; }
        public int ModifierId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
