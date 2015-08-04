using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Squirrel;

//using Squirrel;

namespace HelloWorld {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        BackgroundWorker _bw = new BackgroundWorker();

        public MainWindow() {
            InitializeComponent();
            _bw.DoWork += bw_DoWork;
            _bw.RunWorkerAsync();
        }

        static async void bw_DoWork(object sender, DoWorkEventArgs e) {
            do {
                try {

                    using (var mgr = new UpdateManager("file://C:/released")) {
                        await mgr.UpdateApp();
                    }
                } catch (Exception) {

                }
                Console.WriteLine("I am running");
                Thread.Sleep(2000);
            } while (true);
        }
        //           
        private void Button_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Hello World");

        }
    }
}
