using System;
using System.IO;
using System.Windows;

namespace ArquillesWPF.Core
{
    public class Extensions
    {
        public static readonly DependencyProperty Icon = 
            DependencyProperty.RegisterAttached("Icon", typeof(string), typeof(Extensions), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty Imagem =
            DependencyProperty.RegisterAttached("Imagem", typeof(Uri), typeof(Extensions), new PropertyMetadata(default(Uri)));

        public static void SetIcon(UIElement elemento, string valor)
        {
            elemento.SetValue(Icon, valor);
        }
        public static string GetIcon(UIElement elemento)
        {
            return (string)elemento.GetValue(Icon);
        }

        public static void SetImagem(UIElement elemento, Uri valor)
        {
            elemento.SetValue(Imagem, valor);
        }
        public static Uri GetImagem(UIElement elemento)
        {
            return (Uri)elemento.GetValue(Imagem);
        }
    }
}
