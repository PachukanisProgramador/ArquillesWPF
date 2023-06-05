using ArquillesWPF.Core;
using ArquillesWPF.MVVM.View;
using System;
using System.Threading;
using System.Windows;

namespace ArquillesWPF.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public HomeViewModel TranferenciaSimplesView { get; set; }
        public AboutViewModel SobreOProjetoViewModel { get; set; }
        private object _currentView;

        /* Commands */
        public RelayCommand MoveWindowCommand { get; set; }

        public RelayCommand ShutDownProgramCommand { get; set; }

        public RelayCommand MinimizeWindowCommand { get; set; }

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            Application.Current.MainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Application.Current.MainWindow.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            MoveWindowCommand = new RelayCommand(o => { Application.Current.MainWindow.DragMove(); });

            ShutDownProgramCommand = new RelayCommand(o => 
            {
                ConfirmationBoxView caixaDeConfirmacao = new ConfirmationBoxView
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                caixaDeConfirmacao.ShowDialog();
            });

            MinimizeWindowCommand = new RelayCommand(o => { Application.Current.MainWindow.WindowState = WindowState.Minimized; });

            TranferenciaSimplesView = new HomeViewModel();
            CurrentView = TranferenciaSimplesView;
        }
    }
}
