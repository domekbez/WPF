using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace laby
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public List<Employee> pracownicy = null;

        protected override System.Collections.IEnumerator LogicalChildren
        {
            get { yield return lv; }
        }
        public void znulluj(Object sender, EventArgs e)
        {
            nowypracownik = null;
        }

        private int zaznaczony = 0;
        private string obecnalok=null;
        private string pierwszalinia;
        public bool zmiana = false;
        private Nowy nowypracownik=null;
        private bool czyComboBoxotwarty=false;
        public MainWindow()
        {


            pracownicy = null;
            InitializeComponent();

            

        }
        private void Otworz()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV|*.csv";
            string[] pom = null;
            if (ofd.ShowDialog() == true)
            {
                pracownicy = new List<Employee>();
                obecnalok = ofd.FileName;
                pom = File.ReadAllLines(ofd.FileName);
                
                pierwszalinia = pom[0];
                for (int i = 1; i < pom.Length; i++)
                {
                    string[] linijka = pom[i].Split(';');

                    string[] z = linijka[3].Split('.');
                    int a = int.Parse(z[0]);
                    int b = int.Parse(z[1]);
                    int c = int.Parse(z[2]);
                    int sal = int.Parse(linijka[5]);
                    int cur = int.Parse(linijka[6]);
                    int rol = int.Parse(linijka[7]);

                    DateTime data = new DateTime(c, b, a);
                    Employee em = new Employee(linijka[0], linijka[1], linijka[2], data, linijka[4], sal, (Currency)cur, (Role)rol);
                    pracownicy.Add(em);
                }
                
                this.DataContext = pracownicy;
                zaznaczony = lv.SelectedIndex;
                
                ICollectionView view = CollectionViewSource.GetDefaultView(pracownicy);
                view.Refresh();
                zmiana = false;


            }
        }
        private void Zapisz()
        {
            if (pracownicy != null)
            {

                List<string> lista = new List<string>();
                lista.Add(pierwszalinia);
                foreach (var em in pracownicy)
                {

                    lista.Add(em.ZrobWiersz());
                }
                File.WriteAllLines(obecnalok, lista.ToArray());
                zmiana = false;
                

            }



        }

        private void Openn(object sender, RoutedEventArgs e)
        {
            
            
                if (zmiana == true )
                {
                    switch (MessageBox.Show("Czy chcesz zapisać zmiany?", "Zapisz zmiany", MessageBoxButton.YesNoCancel))
                    {
                        case MessageBoxResult.Cancel:
                            return;
                        case MessageBoxResult.Yes:
                            Zapisz();
                            break;
                    }
                }
            
            Otworz();
        }

        private void DoGory(object sender, RoutedEventArgs e)
        {
            if (pracownicy != null && zaznaczony > 0)
            {
                zaznaczony--;
                Employee pom = pracownicy[zaznaczony];
                pracownicy[zaznaczony] = pracownicy[zaznaczony + 1];
                pracownicy[zaznaczony + 1] = pom;
                ICollectionView view = CollectionViewSource.GetDefaultView(pracownicy);
                view.Refresh();
                zmiana = true;
            }
            
        }

        private void NaDol(object sender, RoutedEventArgs e)
        {
            if (pracownicy != null && zaznaczony < pracownicy.Count - 1 && zaznaczony!=-1)
            {
                zaznaczony++;
                Employee pom = pracownicy[zaznaczony];
                pracownicy[zaznaczony] = pracownicy[zaznaczony - 1];
                pracownicy[zaznaczony - 1] = pom;
                ICollectionView view = CollectionViewSource.GetDefaultView(pracownicy);
                view.Refresh();
                zmiana = true;
            }
   
        }

        
        

        private void lv_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (lv.SelectedIndex != -1)
            {
                
                zaznaczony = lv.SelectedIndex;

                ICollectionView view = CollectionViewSource.GetDefaultView(pracownicy);
                view.Refresh();
            }

        }

        private void DodajPracownika(object sender, RoutedEventArgs e)
        {
            if (pracownicy != null)
            {
                if (nowypracownik != null)
                {
                    nowypracownik.WindowState = WindowState.Normal;
                    return;
                }
                nowypracownik = new Nowy(this);
                nowypracownik.Closed += znulluj;
                nowypracownik.Show();
                ICollectionView view = CollectionViewSource.GetDefaultView(pracownicy);
                view.Refresh();
                zaznaczony = -1;

            }
           
        }

        private void UsunPracownika(object sender, RoutedEventArgs e)
        {

            if (lv.SelectedIndex != -1)
            {
                Employee prac = (Employee)lv.Items[lv.SelectedIndex];
                pracownicy.Remove(prac);
                zmiana = true;
                zaznaczony = -1;

                ICollectionView view = CollectionViewSource.GetDefaultView(pracownicy);
                view.Refresh();
            }
        }

        private void Savee(object sender, RoutedEventArgs e)
        {
            Zapisz();

        }

        private void SaveeFile(object sender, RoutedEventArgs e)
        {
            if (pracownicy != null )
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV|*.csv";
                if (saveFileDialog.ShowDialog() != true)
                {
                    return;
                }
                obecnalok = saveFileDialog.FileName;

                List<string> lista = new List<string>();
                lista.Add(pierwszalinia);
                foreach (var em in pracownicy)
                {

                    lista.Add(em.ZrobWiersz());
                }
                File.WriteAllLines(obecnalok, lista.ToArray());
                zmiana = false;

            }

        }

        private void Closee(object sender, RoutedEventArgs e)
        {
            
            if (zmiana== true)
            {
                switch (MessageBox.Show("Czy chcesz zapisać zmiany?", "Zapisz zmiany", MessageBoxButton.YesNoCancel))
                {
                    case MessageBoxResult.Cancel:
                        return;
                    case MessageBoxResult.Yes:
                        Zapisz();
                        break;
                }
            }
            if(nowypracownik!=null)
                nowypracownik.Close();
            this.Close();
        }
        private void zarabianko_TextChanged(object sender, TextChangedEventArgs e)
        {
            zmiana = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(czyComboBoxotwarty==true)
                zmiana = true;
        }

        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            czyComboBoxotwarty = true;
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            czyComboBoxotwarty = false;
        }
    }
}
