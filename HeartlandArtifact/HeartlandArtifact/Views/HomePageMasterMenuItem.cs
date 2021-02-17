using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}