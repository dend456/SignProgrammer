using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SignProgrammer.Model;
using SignProgrammer.View;
using System;
using System.Windows.Input;

namespace SignProgrammer.ViewModel
{

    public class MainWindowVM : ViewModelBase
    {
        public Sign CurrentSign { get; set; } = new PLSign();

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

        public string SelectedPage { get; set; } = "A";
        public ICommand EffectCommand { get; private set; }
        public ICommand SendMessageCommand { get; private set; }
        public ICommand NewGraphicCommand { get; private set; }

        public MainWindow Window { get; set; }

        public MainWindowVM()
        {
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
                GraphicEditor window = new GraphicEditor();
                GraphicEditorVM vm = window.DataContext as GraphicEditorVM;
                vm.CurrentSign = CurrentSign;
                window.Show();
            });
        }
    }
}
