using ArquillesWPF.Core;
using Microsoft.Win32;
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
        public RelayCommand SubirArquivos { get; set; }

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

        public string TextoUsuario
        {
            get { return _textoUsuario; }
            set { _textoUsuario = value; OnPropertyChanged(); }
        }
        public string TextoSenha
        {
            get { return _textoSenha; }
            set { _textoSenha = value; OnPropertyChanged(); }
        }

        public HomeViewModel()
        {
            IniciarFtp = new RelayCommand(o =>
            {
                _textoEndereco = TextoEndereco;
                TextoCaixaHistorico = _textoEndereco;
            });

            SubirArquivos = new RelayCommand(p =>
            {
                TextoCaixaHistorico = Transferir();
            });
        }

        public string Transferir()
        {
            string caminhoArquivo;
            string nomeArquivo;
            FtpTransfer ftp = new FtpTransfer(TextoEndereco, TextoUsuario, TextoSenha);
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();

                if (ofd.ShowDialog() == true)
                {
                    caminhoArquivo = ofd.FileName;
                    nomeArquivo = caminhoArquivo.Split('\\')[caminhoArquivo.Split('\\').Length - 1];
                    TextoCaixaHistorico = "O arquivo escolhido foi: " + nomeArquivo + "\nEle pode ser encontrado em: " + caminhoArquivo.ToString();

                    return ftp.Transferencia();
                }
                return "Escolha um arquivo para transferir";
            }
            catch (Exception erro)
            {
                return erro.Message;
            }
        }
    }
}

