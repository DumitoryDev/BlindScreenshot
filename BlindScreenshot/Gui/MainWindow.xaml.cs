using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Gui.Utils;
using Gui.View;

namespace Gui
{
    public partial class MainWindow : MetroWindow
    {
        private DllManager _dllManager;
        WindowState _prevState;
        public MainWindow()
        {
            try
            {
                this.Init();
               
            }
            catch (Exception e)
            {
                MessageBox.Show("Error start program! " + e.Message, "Error");
                this.Close();
            }

        }
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                Hide();
            else
                _prevState = WindowState;
        }
        private void TaskbarIcon_TrayLeftMouseDown(object sender, RoutedEventArgs e)
        {
            Show();
            WindowState = _prevState;
        }
        private void Init()
        {
            this.InitializeComponent();
            this.InitComboBox();
            this.DataContext = this;
            this._dllManager = new DllManager();
        }
        
        private async void BtnInject_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Inject();
            }
            catch (Exception exception)
            {
                await this.ShowMessageAsync("Error", exception.Message);
            }

        }

        private async void BtnUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.InitComboBox();
            }
            catch (Exception exception)
            {
                await this.ShowMessageAsync("Error", exception.Message);
            }

        }

        private async void Inject()
        {
            if (!(this.ComboBox.SelectedItem is ProcessView processView))
                return;

            var res = _dllManager.Inject(processView.Process);

            if (res)
            {
                await this.ShowMessageAsync("Info", "Success");
            }
            else
            {
                var errorMessage = "Error " + _dllManager.GetError();
                if (_dllManager.GetError() == 0)
                {
                    errorMessage = "Unknown error";
                }
                await this.ShowMessageAsync("Info", errorMessage);
            }
        }

        private void InitComboBox()
        {
            var processes = Process.GetProcesses();
            Array.Sort(processes, (process, process1) => string.Compare(process1.ProcessName, process.ProcessName, StringComparison.Ordinal));
            var data = processes.Select(process =>
            {
                try
                {
                    if (process.MainModule == null)
                        return null;
                    var icon = System.Drawing.Icon.ExtractAssociatedIcon(process.MainModule.FileName);
                    return new ProcessView(process, icon.ToImageSource());
                }
                catch
                {
                    return new ProcessView(process, null);
                }

            });

            this.ComboBox.ItemsSource = data;

        }

    }
}
