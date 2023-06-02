using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SharpTrooper.Core;
using SharpTrooper.Entities;

namespace Swapi_Cs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SharpTrooperCore core = new SharpTrooperCore();
        private List<People> pessoas = new List<People>();
        private List<Vehicle> vehicles = new List<Vehicle>();
        private List<People> pilots = new List<People>();   
        private List<Starship> starships = new List<Starship>();
        
        public MainWindow()
        {
            InitializeComponent();
            lista_pessoa.Items.Clear();
            for(int i = 1; i < 10; i++) { pessoas.AddRange(core.GetAllPeople(i.ToString()).results); }
            foreach (People pe in pessoas) { lista_pessoa.Items.Add(pe.name); }
        }

        private void select_person(object sender, RoutedEventArgs e)
        {
            if (lista_pessoa.SelectedItem!=null) { people_vehicles(); people_ships(); return; }
            if (lista_vehicle.SelectedItem != null) { vehicles_pilots(); return; }
            if (lista_ship.SelectedItem!=null) { ship_pilots(); return; }
            if (lista_pilot.SelectedItem != null) { pilot_vehicles(); pilot_ships(); return; }
        }

        private void people_vehicles()
        {
            lista_vehicle.Visibility = Visibility.Visible;
            l_v.Content = "vehicles: " + pessoas[lista_pessoa.SelectedIndex].name;
            l_v.Visibility = Visibility.Visible;
            vehicles.Clear();
            foreach (String v in pessoas[lista_pessoa.SelectedIndex].vehicles) { vehicles.Add(core.GetSingleByUrl<Vehicle>(v)); }
            vehicles.OrderBy(o => o.name);
            lista_vehicle.Items.Clear();
            foreach (Vehicle v in vehicles) { lista_vehicle.Items.Add(v.name); }
            return;
        }

        private void people_ships() 
        {
            lista_ship.Visibility = Visibility.Visible;
            l_s.Content = "Ships: " + pessoas[lista_pessoa.SelectedIndex].name;
            l_s.Visibility = Visibility.Visible;
            starships.Clear();
            foreach (String v in pessoas[lista_pessoa.SelectedIndex].starships) { starships.Add(core.GetSingleByUrl<Starship>(v)); }
            lista_ship.Items.Clear();
            starships.OrderBy(o => o.name);
            foreach (Vehicle v in starships) { lista_ship.Items.Add(v.name); }
            lista_pessoa.SelectedItem = null;
            return;
        }

        private void vehicles_pilots() 
        {
            lista_pilot.Visibility = Visibility.Visible;
            l_p.Content = "Pilots: " + vehicles[lista_vehicle.SelectedIndex].name; 
            l_p.Visibility = Visibility.Visible;
            pilots.Clear();
            foreach (String v in vehicles[lista_vehicle.SelectedIndex].pilots) { pilots.Add(core.GetSingleByUrl<People>(v)); }
            lista_pilot.Items.Clear();
            pilots.OrderBy(o => o.name);
            foreach (People v in pilots) { lista_pilot.Items.Add(v.name); };
            lista_vehicle.SelectedItem = null;
            return;
        }

        private void ship_pilots() 
        {
            lista_pilot.Visibility = Visibility.Visible;
            l_p.Content = "Pilots: " + starships[lista_ship.SelectedIndex].name;
            l_p.Visibility = Visibility.Visible;
            pilots.Clear();
            foreach (String v in starships[lista_ship.SelectedIndex].pilots) { pilots.Add(core.GetSingleByUrl<People>(v)); }
            lista_pilot.Items.Clear();
            pilots.OrderBy(o => o.name);
            foreach (People v in pilots) { lista_pilot.Items.Add(v.name); }
            lista_ship.SelectedItem = null;
            return;
        }

        private void pilot_vehicles()
        {
            lista_vehicle.Visibility = Visibility.Visible;
            l_v.Content = "vehicles: " + pilots[lista_pilot.SelectedIndex].name;
            vehicles.Clear();
            foreach (String v in pilots[lista_pilot.SelectedIndex].vehicles) { vehicles.Add(core.GetSingleByUrl<Vehicle>(v)); }
            lista_vehicle.Items.Clear();
            vehicles.OrderBy(o => o.name);
            foreach (Vehicle v in vehicles) { lista_vehicle.Items.Add(v.name); }
            return;
        }
        private void pilot_ships() {
            lista_ship.Visibility = Visibility.Visible;
            l_s.Content = "Ships: " + pilots[lista_pilot.SelectedIndex].name;
            l_s.Visibility = Visibility.Visible;
            starships.Clear();
            foreach (String v in pilots[lista_pilot.SelectedIndex].starships) { starships.Add(core.GetSingleByUrl<Starship>(v)); }
            lista_ship.Items.Clear();
            starships.OrderBy(o => o.name);
            foreach (Vehicle v in starships) { lista_ship.Items.Add(v.name); }
            lista_pilot.SelectedItem = null;
            return;
        }
        private void voltar_click(object sender, RoutedEventArgs e)
        {
            lista_vehicle.Items.Clear();
            lista_vehicle.Visibility = Visibility.Hidden;
            l_v.Visibility = Visibility.Hidden;
            lista_pilot.Items.Clear();
            lista_pilot.Visibility = Visibility.Hidden;
            l_p.Visibility = Visibility.Hidden;
            lista_ship.Visibility= Visibility.Hidden;
            l_s.Visibility= Visibility.Hidden;
        }
        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            if (b_sort.Content == "Ordenar")
            {
                pessoas = pessoas.OrderBy(o => o.name).ToList();
                vehicles =vehicles.OrderBy(o => o.name).ToList();
                starships = starships.OrderBy(o => o.name).ToList();
                pilots = pilots.OrderBy(o => o.name).ToList();
                b_sort.Content = "Ordenar reverso";
                reload();
            }
            else
            {
                pessoas = pessoas.OrderByDescending(o => o.name).ToList();
                vehicles = vehicles.OrderByDescending(o => o.name).ToList();
                starships = starships.OrderByDescending(o => o.name).ToList();
                pilots = pilots.OrderByDescending(o => o.name).ToList();
                b_sort.Content = "Ordenar";
                reload();
            }
        }

        private void reload()
        {
            lista_pessoa.Items.Clear();
            lista_vehicle.Items.Clear();
            lista_ship.Items.Clear();
            lista_pilot.Items.Clear();
            foreach (People pe in pessoas) { lista_pessoa.Items.Add(pe.name); }
            foreach (Vehicle pe in vehicles) { lista_vehicle.Items.Add(pe.name); }
            foreach (Starship pe in starships) { lista_ship.Items.Add(pe.name); }
            foreach (People pe in pilots) { lista_pilot.Items.Add(pe.name); }
        }
    }
}
