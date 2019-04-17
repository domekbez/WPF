using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace laby
{
   
    public partial class Nowy : Window
    {
        private Employee pracownik;
        private MainWindow okno;

        public Nowy(MainWindow w)
        {
            okno = w;
            pracownik = new Employee();
            this.DataContext = pracownik;
            InitializeComponent();
        }

        private void Dodawanie(object sender, RoutedEventArgs e)
        {
            if (male.IsChecked == true)
                pracownik.Sex = "Male";
            else
                pracownik.Sex = "Female";
            okno.pracownicy.Add(pracownik);
            pracownik = new Employee();
            this.DataContext = pracownik;
            okno.zmiana = true;
            ICollectionView view = CollectionViewSource.GetDefaultView(okno.pracownicy);
            view.Refresh();
        }
    }
}
