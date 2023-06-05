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

namespace ArquillesWPF.Core
{
    internal class FtpTransfer
    {
        private string _mensagemLog;
        private string _endereco;
        private string _usuario;
        private string _senha;
        private Uri _uri;
        private FtpWebRequest _requisicao;


        public string Endereco { get { return _endereco; } set { value = _endereco; } }
        public string Usuario { get { return _usuario; } set { value = _usuario; } }
        public string Senha { get { return _senha; } set { value = _senha; } }

        public string MensagemLog { get { return _mensagemLog; } set { _mensagemLog = value; } }

        public FtpTransfer(string endereco, string usuario, string senha)
        {
            _endereco = endereco; _usuario = usuario; _senha = senha;
            Uri uri = new Uri("ftp://" + "192.168.100.10" + "/");
            _uri = uri;
            _requisicao = (FtpWebRequest)WebRequest.Create(uri);
        }
        public string Conectar()
        {
            try
            {
                _requisicao = (FtpWebRequest)WebRequest.Create(_uri);

                if (string.IsNullOrEmpty(_uri.UserInfo))
                {
                    _requisicao.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                    FtpWebResponse resposta = (FtpWebResponse)_requisicao.GetResponse();

                    StreamReader stream = new StreamReader(resposta.GetResponseStream(), Encoding.ASCII);

                    _mensagemLog = stream.ReadToEnd();

                    resposta.Close();

                    return _mensagemLog;
                }
                else
                {
                    NetworkCredential credenciais = new NetworkCredential(
                    _usuario,
                    _senha);

                    _requisicao.Credentials = credenciais;
                    _requisicao.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                    FtpWebResponse resposta = (FtpWebResponse)_requisicao.GetResponse();

                    StreamReader stream = new StreamReader(resposta.GetResponseStream(), Encoding.ASCII);

                    _mensagemLog = stream.ReadToEnd();

                    resposta.Close();
                    _requisicao.Method = null;

                    return _mensagemLog;
                }

            }
            catch (Exception erro)
            {
                return erro.Message;
            }

        }

        public void Transferir(string enderecoArquivo)
        {
            _requisicao = (FtpWebRequest)WebRequest.Create(_uri);

            if (string.IsNullOrEmpty(_uri.UserInfo))
            {
                NetworkCredential credenciais = new NetworkCredential(
                    _usuario,
                    _senha);

                _requisicao.Credentials = credenciais;
            }
            if (!string.IsNullOrEmpty(enderecoArquivo))
            {
                _requisicao.Method = WebRequestMethods.Ftp.UploadFile;
                Stream ftpTransmissao = _requisicao.GetRequestStream();
                FileStream fileStream = File.OpenRead(enderecoArquivo);
                BackgroundWorker processo = new BackgroundWorker();
                byte[] buffer = new byte[1024];
                double tamanhoTransmissao = (double)fileStream.Length;
                int byteRead = 0;
                double leitor = 0;

                do
                {
                    byteRead = fileStream.Read(buffer, 0, 1024);
                    ftpTransmissao.Write(buffer, 0, byteRead);
                    leitor += (double)byteRead;
                    double porcentagemCarregamento = leitor / tamanhoTransmissao * 100;
                    processo.ReportProgress((int)porcentagemCarregamento);
                }
                while (byteRead != 0);
            }
        }
    }
}
