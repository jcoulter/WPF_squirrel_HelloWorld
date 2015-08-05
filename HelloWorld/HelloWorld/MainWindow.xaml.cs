using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
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
        public Boolean Restart = false;

        public MainWindow() {
            InitializeComponent();
            _bw.DoWork += bw_DoWork;
            _bw.RunWorkerAsync(this);

        }

        async void bw_DoWork(object sender, DoWorkEventArgs e) {
            do {
                try {
                    using (var mgr = new UpdateManager(@"C:\dev\helloworld\HelloWorld\Releases", "HelloWorldSquirrel")) {
                        var updateInfo = await mgr.CheckForUpdate(false, progress => { });
                        if (updateInfo == null || updateInfo.ReleasesToApply.Count == 0)
                            return;

                        await mgr.UpdateApp();
                        Restart = true;
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex.StackTrace);
                }
                Console.WriteLine("I am running");
                Thread.Sleep(2000);
            } while (true);
        }

        public void Restart_App() {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
        //           
        private void Button_Click(object sender, RoutedEventArgs e) {
            if (Restart) {
                Restart_App();
            }
            MessageBox.Show("I run 2! - " + Restart);
        }
    }
}
