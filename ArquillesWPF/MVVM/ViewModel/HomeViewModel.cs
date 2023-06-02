using ArquillesWPF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArquillesWPF.MVVM.ViewModel
{
    class HomeViewModel : ObservableObject
    {
        public HomeViewModel HomeVm { get; set; }

        private string _textoEndereco;
        private object _currentView;
        private string _textoCaixaHistorico;
        private string _textoUsuario;
        private string _textoSenha;

        /* Commands */
        public RelayCommand MoveWindowCommand { get; set; }

        public RelayCommand ShutDownProgramCommand { get; set; }

        public RelayCommand MinimizeWindowCommand { get; set; }

        public RelayCommand IniciarFtp { get; set; }

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public string TextoEndereco
        {
            get { return _textoEndereco; }
            set { _textoEndereco = value; OnPropertyChanged(); }
        }

        public string TextoCaixaHistorico
        {
            get { return _textoCaixaHistorico; }
            set { _textoCaixaHistorico = value; OnPropertyChanged(); }
        }

        public string Usuario
        {
            get { return _textoUsuario; }
            set { _textoUsuario = value; OnPropertyChanged(); }
        }
        public string Senha
        {
            get { return _textoSenha; }
            set { _textoSenha = value; OnPropertyChanged(); }
        }

        public HomeViewModel()
        {
            IniciarFtp = new RelayCommand(o =>
            {
                _textoEndereco = TextoEndereco;
                FtpTransfer ftp = new FtpTransfer();
                TextoCaixaHistorico = _textoEndereco;
            });
        }
    }
}

