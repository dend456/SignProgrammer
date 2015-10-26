using SignProgrammer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignProgrammer.ViewModel
{

    public class MainWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Sign CurrentSign { get; set; } = new PLSign();

        private string msg = "";
        public string MessageText 
        { 
            get { return msg; } 
            set 
            { 
                msg = value;
                NotifyPropertyChanged("");
                Console.WriteLine(value); 
            } 
        }

        public string SelectedPage { get; set; } = "A";
        public int CurrentSpeed { get; set; } = 25;


    }
}
