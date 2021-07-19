using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp2TourMVVM_6AKIF_JaenV.Model;

namespace WpfApp2TourMVVM_6AKIF_JaenV.ViewModel
{
    class VMFremdenfbearb : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //ListBox1
        public IEnumerable<Sprache> AlleSprachen // ItemsSource="{Binding Path=AlleSprachen}"
        {
            get
            {
                Tour_DBEntities dB = new Tour_DBEntities();
                
                var erg = (from s in dB.Spraches
                           orderby s.S_Language //DisplayMemberPath="S_Language"
                           select s).ToList();
                dB.Dispose();
                return erg;
                
            }
        }

        //neu Instanz anlegen und Methode
        private int selectedSprache;    //SelectedValue="{Binding Path=SelectedSprache...} und DatenTyp von SelectedValuePath= PK "S_Sprach_Id"

        public int SelectedSprache
        {
            get
            {
                return selectedSprache;
            }
            set
            {
                selectedSprache = value;                                                 // für observable braucht man:
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedSprache"));  //SelectedValue Listbox1 
                PropertyChanged(this, new PropertyChangedEventArgs("IDundFremdenfuehrer"));  //ItemsSource ListBox2
            }

        }

        //ListBox 2
        public IEnumerable<Fremdenfuehrer> IDundFremdenfuehrer
        {
            get
            {
                Tour_DBEntities db = new Tour_DBEntities();
                
                    var erg = (from f in db.Fremdenfuehrers
                               where f.F_S_Sprach_Id == SelectedSprache
                               orderby f.F_Fremdenfuehrer_Id + " " + f.F_Vorname + " " + f.F_Nachname
                               select f).ToList();
                    db.Dispose();

                return erg; 
                
            }
        }

        //StackPanel
        private Fremdenfuehrer selectedFremdenfuehrerID;

        public Fremdenfuehrer SelectedFremdenfuehrerID
        {
            get
            {
                return selectedFremdenfuehrerID;
            }
            set
            {
                selectedFremdenfuehrerID = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedFremdenfuehrerID"));
            }
        }



        //Buttons

        //Instanz der Klasse ViewModel/Delegatecommand.cs
        private ICommand saveCommand;
        private ICommand deleteCommand;
        private ICommand newCommand;


        //Command für jedes Button:
        //SaveButton
        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                    saveCommand = new DelegateCommand(SaveExecute, CanExecute);
                return SaveCommand;
            }
        }

        //DeleteButton
        public ICommand DeleteCommand
        {
            get
            {
                return new DelegateCommand(DeleteExecute, CanExecute);
            }
        }

        //NewButton
        public ICommand NewCommand
        {
            get
            {
                return new DelegateCommand(NewExecute, NewCanExecute);
            }
        }

        public bool CanExecute(object param)
        {
            if (selectedFremdenfuehrerID !=null)
                return true;
            return false;
        }

        public bool NewCanExecute(object param)
        {
            if (selectedFremdenfuehrerID != null)
                return true;
            return false;
        }

        //DeleteButton
        public void DeleteExecute(object param)
        {
            if (selectedFremdenfuehrerID != null)
            {
                using (Tour_DBEntities db = new Tour_DBEntities())
                {
                    db.Entry(selectedFremdenfuehrerID).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    PropertyChanged(this, new PropertyChangedEventArgs("IDundFremdenfuehrer"));
                }
            }
        }


        //NewButton
        public void NewExecute(object param)
        {
            if (selectedSprache != 0)
            {
                using (Tour_DBEntities db = new Tour_DBEntities())
                {
                    db.SaveChanges();
                    PropertyChanged(this, new PropertyChangedEventArgs("selectedSprache"));
                }
            }
        }

        //Save
        public void SaveExecute(object param)
        {
            if (selectedFremdenfuehrerID != null)
            {
                using (Tour_DBEntities db = new Tour_DBEntities())
                    db.SaveChanges();
                PropertyChanged(this, new PropertyChangedEventArgs("selectedSprache"));
            }
        }
    }
}
