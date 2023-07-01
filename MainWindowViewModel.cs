using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace WpfApp2btnDate
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ICommand TextBoxAddCommand { get; set; }
        private DateTime Txtdata1 { get; set; }
        public DateTime TxtData1
        {
            get { return Txtdata1; }
            set
            {
                Txtdata1 = value;
                AddLbl();
            }
        }
        public BaseViewModel ActiveView { get; set; }
        public string Lbldata { get; set; }
        public ObservableCollection<democlass> ListString { get; set; } = new ObservableCollection<democlass>();
        // public ObservableCollection<democlassString> ListString1 { get; set; } = new ObservableCollection<democlassString>(); 
        public MainWindowViewModel()
        {
            //Txtdata1= DateTime.Now;    

            TextBoxAddCommand = new RelayCommand(TextBoxAddCommandClick);
            AddLbl();

        }

        public void AddLbl()
        {
            
            Lbldata = TxtData1.ToString("dd-MM-yyyy");
            for (int i = 0; i < ListString.Count; i++)
            {
                string s = Convert.ToDateTime( ListString[i].txtdata).ToString("dd-MM-yyyy"); ;

                if (Lbldata == "")
                {
                    Lbldata += s;
                }
                else
                {
                    if(s!="")
                    {
                        Lbldata = Lbldata + ", " + s;
                    }
                    else
                    {
                        Lbldata += s;

                    }
                }
            }
            OnPropertyChanged(nameof(Lbldata));

        }
        public void TextBoxMinusCommandClick(object obj)
        {
            int index = ListString.IndexOf((democlass)obj);

            ListString.RemoveAt(index);
            OnPropertyChanged(nameof(ListString));
            AddLbl();
        }

        public int count { get; set; } = 0;
        private void TextBoxAddCommandClick(object obj)
        {
            DateTime TempDate = TxtData1;

            if (ListString.Count > 0)
            {
                for (int i = 0; i < ListString.Count; i++) 
                { 
                
                }
                DateTime s = ListString.Max(x => x.txtdata);
                ListString.Add(new democlass(this) { txtNO = count = count + 1, txtdata = s.AddMonths(1) }); ;
            }
            else
            { 
                ListString.Add(new democlass(this) { txtNO = count = count + 1, txtdata = DateTime.Now }); ;
            }
            OnPropertyChanged(nameof(ListString));
            AddLbl();

        }
    }
    public class democlass : BaseViewModel
    {
        private MainWindowViewModel mainWindowViewModel;

        public democlass(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            TextBoxMinusCommand = new RelayCommand(mainWindowViewModel.TextBoxMinusCommandClick);

        }

        private DateTime txtData { get; set; }
        public DateTime txtdata
        {
            get { return txtData; }
            set
            {
                txtData = value;
                if (mainWindowViewModel != null)
                {
                    mainWindowViewModel.AddLbl();
                }
            }
        }
        public int txtNO { get; set; }
    }
    //public class democlassString
    //{
    //    public string txtdata { get; set; }
    //}
}
