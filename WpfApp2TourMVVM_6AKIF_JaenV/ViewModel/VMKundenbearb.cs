using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Input;
using WpfApp2TourMVVM_6AKIF_JaenV.Model;

namespace WpfApp2TourMVVM_6AKIF_JaenV.ViewModel
{
    class VMKundenbearb : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        Tour_DBEntities db = new Tour_DBEntities();

        //LisBox mit den Kundennamen
        public IEnumerable<Kunde> AlleKunden
        {
            get
            {
                var erg = (from k in db.Kundes
                           orderby k.K_Nachname
                           select k
                ).ToList();
                
                return erg;
            }

        }

         //StackPanel (SelectedKunde)  ist das SelecteedItem vom Listbox)
        private Kunde selectedKunde;
     

        public Kunde SelectedKunde
        {
            get { return selectedKunde; }
            set
            {
                selectedKunde = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedKunde"));
            }
        }

        //Buttons im StackPannel  (Instanzen der Klasse DelegatedCommand.cs)

        //private ICommand newCommand;
        //private ICommand deleteCommand;
        private ICommand saveCommand;


        //Command für New-Button
        public ICommand NewCommand
        {
            get
            {
                return new DelegateCommand(NewExecute, NewCanExecute);
            }
        }

        //Button New gedruckt
        public void NewExecute(object param)
        {
            if (SelectedKunde != null)
            {
                using (Tour_DBEntities db = new Tour_DBEntities())
                {
                    db.SaveChanges();
                    PropertyChanged(this, new PropertyChangedEventArgs("AlleKunden"));
                }
            }
        }


        public bool CanExecute(object param)
        {
            if (SelectedKunde != null)
                return true;
            return false;

        }

        public bool NewCanExecute(object param)
        {
            if (SelectedKunde != null)
                return true;
            return false;

        }


        //Command für Delete
        public ICommand DeleteCommand { get { return new DelegateCommand(DeleteExecute, CanExecute); } }

        //Das Button wurde gedrückt
        public void DeleteExecute(object param)
        {
            if (SelectedKunde != null)
            {
                using (Tour_DBEntities db = new Tour_DBEntities())
                {
                    db.Entry(SelectedKunde).State = EntityState.Deleted;
                    db.SaveChanges();
                    PropertyChanged(this, new PropertyChangedEventArgs("AlleKunden"));
                }
            }
        }

        //Command für Save-Button
        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                    saveCommand = new DelegateCommand(SaveExecute, CanExecute);
                return saveCommand;
            }
        }

        public void SaveExecute(object param)
        {
            if (SelectedKunde != null)
            {
                using (Tour_DBEntities db = new Tour_DBEntities())
                {
                    db.SaveChanges();
                    PropertyChanged(this, new PropertyChangedEventArgs("AlleKunden"));
                }
            }
        }

    }
}
