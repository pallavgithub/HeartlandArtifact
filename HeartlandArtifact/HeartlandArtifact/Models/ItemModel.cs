using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HeartlandArtifact.Models
{
    public class ItemModel
    {
        public int ItemId { get; set; }
        public int CollectionId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Material { get; set; }
        public string FoundBy { get; set; }
        public string ExCollection { get; set; }
        public string PerceivedValue { get; set; }
        public string Cost { get; set; }
        public string Length { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Notes { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastModDate { get; set; }
        public int CreatorId { get; set; }
        public int ModifierId { get; set; }
        [JsonIgnore]
        public List<IFormFile> ItemImages { get; set; }
    }
}
