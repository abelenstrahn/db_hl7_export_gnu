using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace EstomedApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, MainThread.MainThreadCb
    {
        private Thread scanThread = null;
        private Thread hl7Thread = null;

        public MainWindow()
        {
            InitializeComponent();
            estomedDefaultInput();
        }

        private void estomedDefaultInput()
        {
            portList.Text = "10020";
            dbInput.Text = "e2demo";
            userInput.Text = "root";
            passwordInput.Text = "";
            outputInput.Text = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory).ToString() + "\\list.hl7";
        }

        private void selectFile(String filePath)
        {
            string argument = @"/select, " + filePath;
            if (!File.Exists(filePath))
            {
                return;
            }
            System.Diagnostics.Process.Start("explorer.exe", argument);
        }

        private void runButton_Click(object sender, RoutedEventArgs e)
        {
            if (hl7Thread != null)
            {
                hl7Thread.Abort();
                hl7Thread = null;
                runButton.Content = "Generuj";
                status.Content = "Zatrzymano";
                progress.Value = 100;
                return;
            }
            if (scanThread != null)
            {
                scanThread.Abort();
                scanThread = null;
                runButton.Content = "Generuj";
                status.Content = "Zatrzymano";
                progress.Value = 100;
                return;
            }
            string sPort = "";
            if (portList.SelectedIndex < 0)
            {
                sPort = portList.Text;
            } else
            {
                sPort = (string)portList.SelectedItem;
            }
            if(runButton.Content.ToString() == "Pokaż plik")
            {
                selectFile(outputInput.Text);
                runButton.Content = "Generuj";
                return;
            }
            if (sPort.Length > 0 && serverInput.Text.Length > 0)
            {
                int port = 0;
                try
                {
                    port = int.Parse(sPort);
                    if (port > 0 )
                    {
                        runButton.Content = "Zatrzymaj";
                        MainThread threadClass = new MainThread(this, serverInput.Text, port, dbInput.Text, userInput.Text, passwordInput.Text);
                        hl7Thread = new Thread(new ThreadStart(threadClass.processEstomed));
                        hl7Thread.Start();
                    }
                    else
                    {
                        MessageBox.Show("Wybierz program");
                    }
                }
                catch (FormatException err)
                {
                    MessageBox.Show("Nieprawidłowy numer portu");
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Nieprawidłowy numer portu");
                } 
            } else
            {
                MessageBox.Show("Wypełnij wszystkie pola");
            }
        }

        private void portList_TextChanged(object sender, RoutedEventArgs e)
        {
            portList.SelectedIndex = -1;
        }

        private void settingsExtended_Click(object sender, RoutedEventArgs e)
        {
            NetUtil threadClass = new NetUtil(this, serverInput.Text, 22, 8080);
            scanThread = new Thread(new ThreadStart(threadClass.ScanPorts));
            scanThread.Start();
            runButton.Content = "Zatrzymaj";
        }

        public void onConnectStart()
        {
            Dispatcher.Invoke((Action)delegate ()
            {
                status.Content = "Łączenie z bazą danych";
                progress.Value = 5;
            });
        }

        public void onConnectError(String err)
        {
            Dispatcher.Invoke((Action)delegate ()
            {
                status.Content = "Błąd połączenia z bazą danych";
                runButton.Content = "Generuj";
                progress.Value = 100;
                MessageBox.Show(err);
            });
        }


        public void onConnectOk()
        {
            Dispatcher.Invoke((Action)delegate ()
            {
                status.Content = "Połączono z bazą danych";
                progress.Value = 10;
            });
        }

        public void onHL7Progress(double pr)
        {
            Dispatcher.Invoke((Action)delegate ()
            {
                status.Content = "Generowanie pliku";
                progress.Value = pr;
            });
        }

        public void onHL7Done(string tmpFile)
        {
            hl7Thread = null;
            Dispatcher.Invoke((Action)delegate ()
            {
                status.Content = "Zakończono";
                progress.Value = 100;
                runButton.Content = "Pokaż plik";
                 File.Copy(tmpFile, outputInput.Text, true);
            });
        }

        public void onScanProgress(double pr)
        {
            Dispatcher.Invoke((Action)delegate () {
                progress.Value = pr;
                status.Content = "Skanowanie portów";
            });
        }

        public void onScanResult(List<int> usedPort)
        {
            usedPort.Sort();
            scanThread = null;
            Dispatcher.Invoke((Action)delegate ()
            {
                portList.Items.Clear();
                progress.Value = 100;
                status.Content = "Skanowanie portów zakończone";
                runButton.Content = "Generuj";
                portList.Text = "Wybierz z listy";
                foreach (var port in usedPort)
                {
                    portList.Items.Add(port.ToString());
                }
            });
        }

        public void onError(string msg)
        {
            status.Content = "Zakończono z błędem: " + msg;
            progress.Value = 100;
            runButton.Content = "Generuj";
            hl7Thread = null;
        }

        private void changeFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = "list";
            savefile.Filter = "HL7 files (*.hl7)|*.hl7";
            bool? save = savefile.ShowDialog();
            if (save.HasValue && (bool)save)
            {
                outputInput.Text = savefile.FileName;
            }
        }
    }
}
