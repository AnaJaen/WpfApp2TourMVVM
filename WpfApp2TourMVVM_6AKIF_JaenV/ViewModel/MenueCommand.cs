using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp2TourMVVM_6AKIF_JaenV.ViewModel
{
    class MenueCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MainWindow mw = (MainWindow)Application.Current.MainWindow;

            if (parameter?.ToString() != "WindowAbout")
                mw.contentcontrol.Children.Clear();

            switch (parameter.ToString())
            {
                case "Kundenbearb":
                    mw.contentcontrol.Children.Add(new Kundenbearb());    //kundenbearb() ist der CommandParameter von jedem MenuIten Header in  MainWindow
                    break;

                case "Buchungsbearb":
                    //mw.contentcontrol.Children.Add(new Buchungsbearb());
                    break;

                case "Tourbearb":
                    //mw.contentcontrol.Children.Add(new Tourbearb());
                    break;

                case "Treffanzeigen":
                    mw.contentcontrol.Children.Add(new Treffanzeigen());
                    break;

                case "Treffbearb":
                    //mw.contentcontrol.Children.Add(new Treffbearb());
                    break;

                case "Fremdenfbearb":
                    mw.contentcontrol.Children.Add(new Fremdenfbearb());
                    break;

                case "StatTour":
                    mw.contentcontrol.Children.Add(new StatTourxaml());
                    break;

                case "AboutUns":
                    mw.contentcontrol.Children.Add(new AboutUns());
                    break;




                case "WindowAbout":
                    //AboutUns wa = new WindowAbout();
                    //wa.ShowDialog();
                    break;
                default:
                    MessageBox.Show(parameter.ToString() + " ist kein sinnvoller Menüeintrag!!");
                    break;

            }
        }
    }
}
