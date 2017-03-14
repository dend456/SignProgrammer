using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SignProgrammer.Model;
using SignProgrammer.View;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace SignProgrammer.ViewModel
{

    public class MainWindowVM : ViewModelBase
    {
        Sign currentSign;
        public Sign CurrentSign
        {
            get
            {
                return currentSign;
            }
            set
            {
                currentSign = value;
                OpenComPorts = SerialSign.OpenComPorts();
                RaisePropertyChanged("");
            }
        }
        
        public List<string> OpenComPorts { get; private set; }

        private string msg = "";
        public string MessageText 
        { 
            get { return msg; } 
            set 
            { 
                msg = value;
                RaisePropertyChanged("");
            } 
        }

        private int currentSpeed = 25;
        public int CurrentSpeed 
        { 
            get
            {
                return currentSpeed;
            } 
            set
            {
                currentSpeed = value;
                CurrentSign.SetSpeed(value);
            }
        }

        private string selectedComPort = "";
        public string SelectedComPort
        {
            get
            {
                return selectedComPort;
            }
            set
            {
                selectedComPort = value;
                ((SerialSign)CurrentSign).Port = value;
            }
        }

        public string SelectedPage { get; set; } = "A";
        public ICommand EffectCommand { get; private set; }
        public ICommand SendMessageCommand { get; private set; }
        public ICommand NewGraphicCommand { get; private set; }
        public ICommand RefreshComPortsCommand { get; private set; }

        public MainWindow Window { get; set; }
        private GraphicEditor graphicsWindow;

        public MainWindowVM()
        {
            CurrentSign = new PLSign();

            EffectCommand = new RelayCommand<SignEffect>(s =>
            {
                int oldIndex = Window.MessageBoxCaretIndex;
                MessageText = MessageText.Insert(oldIndex, s.Text);
                Window.MessageBoxCaretIndex = oldIndex + s.Text.Length;
            });

            SendMessageCommand = new RelayCommand(() =>
            {
                if (!string.IsNullOrEmpty(msg))
                {
                    CurrentSign.SendMessage(msg, SelectedPage);
                }
            });

            NewGraphicCommand = new RelayCommand(() =>
            {
                graphicsWindow = new GraphicEditor();
                GraphicEditorVM vm = graphicsWindow.DataContext as GraphicEditorVM;
                vm.CurrentSign = CurrentSign;
                vm.MainWindow = this;
                graphicsWindow.Show();
            }); 
            
            RefreshComPortsCommand = new RelayCommand(() =>
            {
                OpenComPorts = SerialSign.OpenComPorts();
                RaisePropertyChanged("");
            });
        }

        public void refreshGraphics()
        {
            graphicsWindow.Close();
            NewGraphicCommand.Execute(null);
        }
    }
}
