using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using WpfApp2TourMVVM_6AKIF_JaenV.Model;

namespace WpfApp2TourMVVM_6AKIF_JaenV.ViewModel
{
    class VMStatTour : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        Tour_DBEntities dbglobal;

        public VMStatTour()
        {
            dbglobal = new Tour_DBEntities();

        }

        public IEnumerable<BuchungTourStat> Statistik
        {
            get
            {
                return (from t in dbglobal.Tours
                        orderby t.To_Bezeichnung
                        select new BuchungTourStat
                        {
                            Tour_Id = t.To_Tour_Id,
                            Bezeichnung = t.To_Bezeichnung,
                            Buchungszahl = t.Buchungs.Count,
                            Breite = t.Buchungs.Count() * 20
                        }
                ).ToList();
            }
        }

        public class BuchungTourStat
        {
            public int Tour_Id { get; set; }
            public String Bezeichnung { get; set; }
            public int Buchungszahl { get; set; }
            public int Breite { get; set; }
        }
    }
}
