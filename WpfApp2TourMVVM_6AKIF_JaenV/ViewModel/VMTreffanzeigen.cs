using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp2TourMVVM_6AKIF_JaenV.Model;  //Access to DB

namespace WpfApp2TourMVVM_6AKIF_JaenV.ViewModel
{
    class VMTreffanzeigen : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        Tour_DBEntities dB = new Tour_DBEntities();

        public IEnumerable<Tour> AlleTour
        {
            get
            {
                return dB.Tours.OrderBy(x => x.To_Bezeichnung).ToList();
            }
        }

    }
}
