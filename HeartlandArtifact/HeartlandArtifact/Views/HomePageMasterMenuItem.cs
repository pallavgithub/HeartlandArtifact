using System;
namespace HeartlandArtifact.Views
{
    public class HomePageMasterMenuItem
    {
        public HomePageMasterMenuItem()
        {
            TargetType = typeof(HomePageMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string IconImage { get; set; }
        public Type TargetType { get; set; }
        public string TextColor { get; set; }
    }
}