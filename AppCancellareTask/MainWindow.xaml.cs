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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppCancellareTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        CancellationTokenSource ct = new CancellationTokenSource();

        //Utilizzo il metodoDoWork per tutti e tre i casi
        private void DoWork(int max, int delay, Label lbl, CancellationTokenSource ct)
        {
            for (int i = 0; i <= max; i++)
            {
                Dispatcher.Invoke(() => UpdateUI(i, lbl));
                Thread.Sleep(delay);
                //per fermare TAsk
                if (ct.Token.IsCancellationRequested)
                {
                    //Gracefully
                    break;

                    // No Gracefully stop
                    // ct.Token.ThrowIfCancellationRequested();
                    // break;
                }

            }
        }

        private void UpdateUI(int i, Label lbl)
        {
            lbl.Content = i.ToString();
        }
        private void btnNoPArametri_Click(object sender, RoutedEventArgs e)
        {
            //per fermare task
            if (ct == null)
                ct = new CancellationTokenSource();
            // codice per avviare task
            Task.Factory.StartNew(() => DoWork(100, 100, lblNoParametro, ct));



        }

        private void btnConPArametro_Click(object sender, RoutedEventArgs e)
        {
            //per fermare task
            if (ct == null)
                ct = new CancellationTokenSource();
            // codice per avviare task
            int max = Convert.ToInt32(txtMax1.Text);
            Task.Factory.StartNew(() => DoWork(max, 100, lblConParametro, ct));
        }

        private void btnConParametri_Click(object sender, RoutedEventArgs e)
        {
            //per fermare task
            if (ct == null)
                ct = new CancellationTokenSource();
            // codice per avviare task
            int max2 = Convert.ToInt32(txtMax2.Text);
            int delay = Convert.ToInt32(txtDelay.Text);
            Task.Factory.StartNew(() => DoWork(max2, delay, lblConParametri, ct));
        }

        //iniizamo a considerare la possibilità di fermare un task

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (ct != null)
            {
                ct.Cancel();
                ct = null;
            }


        }
    }
}
