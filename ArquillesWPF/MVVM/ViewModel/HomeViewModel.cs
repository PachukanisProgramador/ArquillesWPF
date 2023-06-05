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
        public HomeViewModel HomeVm { get; set; }

        private string _textoEndereco;
        private object _currentView;
        private string _textoCaixaHistorico;
        private string _textoUsuario;
        private string _textoSenha;
        private double _progressoTransmissao;
        private FtpTransfer _ftp;

        /* Commands */

        public RelayCommand IniciarFtp { get; set; }
        public RelayCommand SubirArquivos { get; set; }

        public string TextoEndereco
        {
            get { return _textoEndereco; }
            set { _textoEndereco = value; OnPropertyChanged(); }
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
        public string TextoCaixaHistorico
        {
            get { return _textoCaixaHistorico; }
            set { _textoCaixaHistorico = value; OnPropertyChanged(); }
        }

        public double ProgressoTransmissao
        {
            get { return _progressoTransmissao; }
            set { _progressoTransmissao = value; OnPropertyChanged(); }
        }

        public HomeViewModel()
        {
            _ftp = new FtpTransfer(TextoEndereco, TextoUsuario, TextoSenha);

            IniciarFtp = new RelayCommand(o =>
            {
                TextoCaixaHistorico = _ftp.Conectar();
            });

            SubirArquivos = new RelayCommand(p =>
            {
                ProgressoTransmissao = _ftp.Transferir(EscolherArquivos());
            });
        }

        private string EscolherArquivos()
        {
            string caminhoArquivo;
            string nomeArquivo;

            try
            {
                OpenFileDialog ofd = new OpenFileDialog();

                if (ofd.ShowDialog() == true)
                {
                    caminhoArquivo = ofd.FileName;
                    nomeArquivo = caminhoArquivo.Split('\\')[caminhoArquivo.Split('\\').Length - 1];
                    MessageBox.Show($"Arquivo escolhido para transferência: {nomeArquivo} \n\n({caminhoArquivo.ToString()})");
                    return caminhoArquivo;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
    }
}

