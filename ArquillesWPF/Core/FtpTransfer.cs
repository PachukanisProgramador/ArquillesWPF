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

namespace ArquillesWPF.Core
{
    internal class FtpTransfer
    {
        private string _mensagemLog;
        private string _endereco;
        private string _usuario;
        private string _senha;


        public string Endereco { get { return _endereco; } set { value = _endereco; } }
        public string Usuario { get { return _usuario; } set { value = _usuario; } }
        public string Senha { get { return _senha; } set { value = _senha; } }

        public string MensagemLog { get { return _mensagemLog; } set { _mensagemLog = value; } }

        public FtpTransfer(string endereco, string usuario, string senha)
        {
            _endereco = endereco; _usuario = usuario; _senha = senha;
        }
        public string Conectar()
        {
            try
            {
                Uri uri = new Uri("ftp://" + "192.168.100.10" + "//");
                FtpWebRequest requisicao = (FtpWebRequest)WebRequest.Create(uri);

                if (string.IsNullOrEmpty(uri.UserInfo))
                {
                    requisicao.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                    FtpWebResponse resposta = (FtpWebResponse)requisicao.GetResponse();

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

                    requisicao.Credentials = credenciais;
                    requisicao.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                    FtpWebResponse resposta = (FtpWebResponse)requisicao.GetResponse();

                    StreamReader stream = new StreamReader(resposta.GetResponseStream(), Encoding.ASCII);

                    _mensagemLog = stream.ReadToEnd();

                    resposta.Close();

                    return _mensagemLog;
                }

            }
            catch (Exception erro)
            {
                return erro.Message;
            }

        }
    }
}
