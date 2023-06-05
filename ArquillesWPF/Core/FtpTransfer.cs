using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ArquillesWPF.MVVM.View;
using Microsoft.Win32;
using System.Windows.Controls;
using System.ComponentModel;
using System.Xml.Schema;
using System.Security.Policy;
using ArquillesWPF.MVVM.ViewModel;
using System.Windows;

namespace ArquillesWPF.Core
{
    internal class FtpTransfer
    {
        private readonly string _endereco;
        private readonly string _usuario;
        private readonly string _senha;
        private readonly string _nomeArquivo;
        private readonly string _caminhoArquivo;

        private string _mensagemLog;
        private Uri _uri;
        private FtpWebRequest _requisicao;


        public string Endereco { get { return _endereco; } set { value = _endereco; } }
        public string Usuario { get { return _usuario; } set { value = _usuario; } }
        public string Senha { get { return _senha; } set { value = _senha; } }
        public string MensagemLog { get { return _mensagemLog; } set { _mensagemLog = value; } }

        public FtpTransfer(HomeViewModel.Transmissao transmissao)
        {
            _endereco = transmissao.Servidor;
            _usuario = transmissao.Usuario;
            _senha = transmissao.Senha;
            _nomeArquivo = transmissao.NomeArquivo;
            _caminhoArquivo = transmissao.CaminhoArquivo;
        }
        public string Conectar()
        {
            try
            {
                string texto = string.Format("ftp://{0}/", _endereco);
                Console.WriteLine(texto);
                _uri = new Uri(texto);

                _requisicao = (FtpWebRequest)WebRequest.Create(_uri);

                if (!string.IsNullOrEmpty(_uri.UserInfo))
                {
                    NetworkCredential credenciais = new NetworkCredential(_usuario, _senha);
                    _requisicao.Credentials = credenciais;
                }
                    _requisicao.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                    FtpWebResponse resposta = (FtpWebResponse)_requisicao.GetResponse();

                    StreamReader stream = new StreamReader(resposta.GetResponseStream(), Encoding.ASCII);

                    _mensagemLog = stream.ReadToEnd();

                    resposta.Close();

                    return _mensagemLog;

            }
            catch (Exception erro)
            {
                return erro.Message;
            }

        }

        public void Transferir()
        {
            try
            {
                string texto = string.Format("ftp://{0}/{1}", _endereco, _nomeArquivo);
                Console.WriteLine(texto);
                _uri = new Uri(texto);

                _requisicao = (FtpWebRequest)WebRequest.Create(_uri);

                if (!string.IsNullOrEmpty(_uri.UserInfo))
                {
                    NetworkCredential credenciais = new NetworkCredential(_usuario, _senha);
                    _requisicao.Credentials = credenciais;
                }
                if (!string.IsNullOrEmpty(_caminhoArquivo))
                {

                    _requisicao.Method = WebRequestMethods.Ftp.UploadFile;
                    Stream ftpTransmissao = _requisicao.GetRequestStream();
                    FileStream fileStream = File.OpenRead(_caminhoArquivo);

                    byte[] buffer = new byte[1024];
                    int byteRead = 0;

                    do
                    {
                        byteRead = fileStream.Read(buffer, 0, 1024);
                        ftpTransmissao.Write(buffer, 0, byteRead);
                        MessageBox.Show($"Arquivo {_nomeArquivo} enviado com sucesso!");
                    }
                    while (byteRead != 0);
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro: " + erro.Message);
            }
        }
    }
}
