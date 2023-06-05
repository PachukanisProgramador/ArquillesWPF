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

        private Uri _uri;
        private FtpWebRequest _requisicao;

        public FtpTransfer(HomeViewModel.Transmissao transmissao)
        {
            _endereco = transmissao.Servidor;
            _usuario = transmissao.Usuario;
            _senha = transmissao.Senha;
            _nomeArquivo = transmissao.NomeArquivo;
            _caminhoArquivo = transmissao.CaminhoArquivo;
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
                    Console.WriteLine($"Iniciando requisição.\nArquivo: {_nomeArquivo}.\nCaminho: {_caminhoArquivo}\nServidor: {_endereco}\nUsuario: {_usuario}\nSenha: {_senha}");

                    NetworkCredential credenciais = new NetworkCredential(_usuario, _senha);
                    _requisicao.Credentials = credenciais;

                    _requisicao.Method = WebRequestMethods.Ftp.UploadFile;
                    Stream ftpTransmissao = _requisicao.GetRequestStream();
                    FileStream fileStream = File.OpenRead(_caminhoArquivo);

                    byte[] buffer = new byte[1024];
                    int byteRead = 0;

                    do
                    {
                        byteRead = fileStream.Read(buffer, 0, 1024);
                        ftpTransmissao.Write(buffer, 0, byteRead);
                    }
                    while (byteRead != 0);
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine(erro.Message);
            }
        }
    }
}
