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

        public ObservableCollection<List<string>> Resultado { get; set; }
        public RelayCommand ConexaoFtp { get; set; }

        public string Endereco { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }

        public string MensagemLog { get { return _mensagemLog; } set { _mensagemLog = value; } }

        public FtpTransfer()
        {
            string endereco = "";

            Uri uri = new Uri("ftp://" + endereco + "//");
            FtpWebRequest requisicao = (FtpWebRequest)WebRequest.Create(uri);
            NetworkCredential credenciais = new NetworkCredential(
                Environment.UserDomainName,
                Usuario,
                Senha
                );

            requisicao.Credentials = credenciais;
            requisicao.EnableSsl = false;
            requisicao.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            requisicao.KeepAlive = true;
            requisicao.UsePassive = true;

            FtpWebResponse resposta = (FtpWebResponse)requisicao.GetResponse();

            StreamReader stream = new StreamReader(resposta.GetResponseStream(), Encoding.ASCII);

            _mensagemLog = stream.ReadToEnd();

            resposta.Close();
        }
    }
}
