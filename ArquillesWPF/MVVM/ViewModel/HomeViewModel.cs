using ArquillesWPF.Core;
using ArquillesWPF.MVVM.View;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace ArquillesWPF.MVVM.ViewModel
{
    class HomeViewModel : ObservableObject
    {
        private string _servidor;
        private string _usuario;
        private string _senha;
        private FtpTransfer _ftp;
        private SendConfirmationBoxView _mensagem;

        /* Commands */
        public RelayCommand Enviar { get; set; }  

        public struct Transmissao 
        {
            public string Servidor { get; set; }
            public string Usuario { get; set; }
            public string Senha { get; set; }
            public string NomeArquivo { get; set; }
            public string  CaminhoArquivo { get; set; }
        }
        public string Servidor { get { return _servidor; } set { _servidor = value; OnPropertyChanged(); } }
        public string Usuario { get { return _usuario; } set { _usuario = value; OnPropertyChanged(); } }
        public string Senha { get { return _senha; } set { _senha = value; OnPropertyChanged(); } }

        public Transmissao transmissao = new Transmissao();

        public HomeViewModel()
        {
            Enviar = new RelayCommand(o => {
                EscolherArquivos();

                _ftp = new FtpTransfer(transmissao);
                _ftp.Transferir();

                _mensagem = new SendConfirmationBoxView
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                _mensagem.ShowDialog();
            }) ;
        }
        private Transmissao EscolherArquivos()
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();

                if (ofd.ShowDialog() == true)
                {
                    FileInfo arquivo = new FileInfo(ofd.FileName);
                    transmissao.Servidor = "192.168.100.10";
                    transmissao.Usuario = "Usuario1";
                    transmissao.Senha = "Login@123";
                    transmissao.CaminhoArquivo = arquivo.FullName;
                    transmissao.NomeArquivo = arquivo.Name;
                    return transmissao;
                }
                else
                {
                    return transmissao;
                }
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
    }
}

