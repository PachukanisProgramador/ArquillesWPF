using ArquillesWPF.Core;
using ArquillesWPF.MVVM.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArquillesWPF.MVVM.ViewModel
{
    class HomeViewModel : ObservableObject
    {
        private object _currentView;
        private string _textoCaixaHistorico;
        private double _progressoTransmissao;
        private string _servidor;
        private string _usuario;
        private string _senha;

        private FtpTransfer _ftp;

        /* Commands */

        public RelayCommand IniciarFtp { get; set; }
        public RelayCommand SubirArquivos { get; set; }
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

        public string TextoCaixaHistorico
        {
            get { return _textoCaixaHistorico; }
            set { _textoCaixaHistorico = value; OnPropertyChanged(); }
        }

        public HomeViewModel()
        {
            IniciarFtp = new RelayCommand(o =>
            {
                _ftp = new FtpTransfer(VerificarDiretorios());
                TextoCaixaHistorico = _ftp.Conectar();
            });

            SubirArquivos = new RelayCommand(o =>
            {
                _ftp = new FtpTransfer(EscolherArquivos());
            });

            Enviar = new RelayCommand(o => { _ftp.Transferir(); }) ;
        }

        private Transmissao VerificarDiretorios()
        {
            transmissao.Servidor = "192.168.100.10";
            transmissao.Usuario = "Usuario1";
            transmissao.Senha = "Login@123";
            return transmissao;
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

