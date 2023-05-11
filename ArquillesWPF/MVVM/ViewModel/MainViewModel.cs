using ArquillesWPF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArquillesWPF.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public HomeViewModel homeVm { get; set; }
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            homeVm = new HomeViewModel();
            CurrentView = homeVm;
        }
    }
}
