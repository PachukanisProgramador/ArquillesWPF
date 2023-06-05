using ArquillesWPF.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ArquillesWPF.MVVM.View
{
    /// <summary>
    /// Lógica interna para ConfirmationBoxView.xaml
    /// </summary>
    public partial class ConfirmationBoxView : Window
    {
        public ConfirmationBoxView()
        {
            InitializeComponent();
        }

        private void BotaoSim_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BotaoNao_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
