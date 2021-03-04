using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeartlandArtifact.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public INavigationService nav;
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            nav = navigationService;
            Title = "Main Page";
        }
    }
}
