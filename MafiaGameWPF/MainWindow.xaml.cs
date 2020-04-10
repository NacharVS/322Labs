using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MongoDB.Driver;
using MongoDB.Bson;

namespace MafiaGameWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Player> registeredPlayers = new ObservableCollection<Player>();
        public ERoles selectedRole;
        public MongoClient client = new MongoClient("mongodb://localhost");
        public IMongoDatabase db;
        public IMongoCollection<Team> teams;
        public MainWindow()
        {
            InitializeComponent();
            RegisteredUsersListBox.ItemsSource = registeredPlayers;
            registeredPlayers.CollectionChanged += RegisteredPlayers_CollectionChanged;
            db = client.GetDatabase("MafiaDb");
            teams = db.GetCollection<Team>("Teams");
            TeamsComboBox.ItemsSource = teams.Find(new BsonDocument()).ToList();
        }

        private void RegisteredPlayers_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    MafiaComboBox.ItemsSource = registeredPlayers.Where(p => p.Role == ERoles.Mafia);
                    CitizensComboBox.ItemsSource = registeredPlayers.Where(p => p.Role == ERoles.Citizen);
                    BossTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Boss)?.Nickname ?? "<unsetted>";
                    SherifTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Sherif)?.Nickname ?? "<unsetted>";
                    DoctorTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Doctor)?.Nickname ?? "<unsetted>";
                    ManiacTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Maniac)?.Nickname ?? "<unsetted>";
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    break;
            }
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RegistrationTextBox.Text))
            {
                MessageBox.Show("Error: Nickname must be specified.");
                return;
            }
            Player newPlayer = new Player()
            {
                Nickname = RegistrationTextBox.Text,
                Role = ERoles.Citizen
            };
            registeredPlayers.Add(newPlayer);
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            if (RegisteredUsersListBox.SelectedItem == null)
            {
                MessageBox.Show("Error: you need to select a player from registered users to delete them.");
                return;
            }
            registeredPlayers.Remove(RegisteredUsersListBox.SelectedItem as Player);
        }

        private void SetRoleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (RegisteredUsersListBox.SelectedItem == null)
            {
                MessageBox.Show("Error: you need to select a player from registered users to delete them.");
                return;
            }
            if ((bool)MafiaRadio.IsChecked)
            {
                selectedRole = ERoles.Mafia;
            }
            if ((bool)BossRadio.IsChecked)
            {
                selectedRole = ERoles.Boss;
            }
            if ((bool)DoctorRadio.IsChecked)
            {
                selectedRole = ERoles.Doctor;
            }
            if ((bool)ManiacRadio.IsChecked)
            {
                selectedRole = ERoles.Maniac;
            }
            if ((bool)SherifRadio.IsChecked)
            {
                selectedRole = ERoles.Sherif;
            }
            if ((bool)CitizenRadio.IsChecked)
            {
                selectedRole = ERoles.Citizen;
            }
            if (registeredPlayers.FirstOrDefault(p => p.Role == selectedRole && (selectedRole == ERoles.Doctor || selectedRole == ERoles.Boss || selectedRole == ERoles.Sherif || selectedRole == ERoles.Maniac)) != null)
            {
                MessageBox.Show("Error: Player with that role is already exists");
                return;
            }
            registeredPlayers.FirstOrDefault(p => p.Nickname == (RegisteredUsersListBox.SelectedItem as Player).Nickname).Role = selectedRole;
            MafiaComboBox.ItemsSource = registeredPlayers.Where(p => p.Role == ERoles.Mafia);
            CitizensComboBox.ItemsSource = registeredPlayers.Where(p => p.Role == ERoles.Citizen);
            BossTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Boss)?.Nickname ?? "<unsetted>";
            SherifTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Sherif)?.Nickname ?? "<unsetted>";
            DoctorTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Doctor)?.Nickname ?? "<unsetted>";
            ManiacTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Maniac)?.Nickname ?? "<unsetted>";
        }
        private void ReroleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (RegisteredUsersListBox.SelectedItem == null)
            {
                MessageBox.Show("Error: you need to select a player from registered users to delete them.");
                return;
            }
            if ((bool)MafiaRadio.IsChecked)
            {
                selectedRole = ERoles.Mafia;
            }
            if ((bool)BossRadio.IsChecked)
            {
                selectedRole = ERoles.Boss;
            }
            if ((bool)DoctorRadio.IsChecked)
            {
                selectedRole = ERoles.Doctor;
            }
            if ((bool)ManiacRadio.IsChecked)
            {
                selectedRole = ERoles.Maniac;
            }
            if ((bool)SherifRadio.IsChecked)
            {
                selectedRole = ERoles.Sherif;
            }
            if ((bool)CitizenRadio.IsChecked)
            {
                selectedRole = ERoles.Citizen;
            }
            Player player = registeredPlayers.FirstOrDefault(p => p.Role == selectedRole && (selectedRole == ERoles.Doctor || selectedRole == ERoles.Boss || selectedRole == ERoles.Sherif || selectedRole == ERoles.Maniac));
            if (player != null)
            {
                player.Role = ERoles.Citizen;
            }
            registeredPlayers.FirstOrDefault(p => p.Nickname == (RegisteredUsersListBox.SelectedItem as Player).Nickname).Role = selectedRole;
            MafiaComboBox.ItemsSource = registeredPlayers.Where(p => p.Role == ERoles.Mafia);
            CitizensComboBox.ItemsSource = registeredPlayers.Where(p => p.Role == ERoles.Citizen);
            BossTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Boss)?.Nickname ?? "<unsetted>";
            SherifTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Sherif)?.Nickname ?? "<unsetted>";
            DoctorTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Doctor)?.Nickname ?? "<unsetted>";
            ManiacTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Maniac)?.Nickname ?? "<unsetted>";
        }

        private void ClearRoleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (RegisteredUsersListBox.SelectedItem == null)
            {
                MessageBox.Show("Error: you need to select a player from registered users to delete them.");
                return;
            }
            if ((bool)MafiaRadio.IsChecked)
            {
                selectedRole = ERoles.Mafia;
            }
            if ((bool)BossRadio.IsChecked)
            {
                selectedRole = ERoles.Boss;
            }
            if ((bool)DoctorRadio.IsChecked)
            {
                selectedRole = ERoles.Doctor;
            }
            if ((bool)ManiacRadio.IsChecked)
            {
                selectedRole = ERoles.Maniac;
            }
            if ((bool)SherifRadio.IsChecked)
            {
                selectedRole = ERoles.Sherif;
            }
            if ((bool)CitizenRadio.IsChecked)
            {
                selectedRole = ERoles.Citizen;
            }
            Player player = registeredPlayers.FirstOrDefault(p => p.Role == selectedRole && (selectedRole == ERoles.Doctor || selectedRole == ERoles.Boss || selectedRole == ERoles.Sherif || selectedRole == ERoles.Maniac));
            if (player != null)
            {
                player.Role = ERoles.Citizen;
            }
            MafiaComboBox.ItemsSource = registeredPlayers.Where(p => p.Role == ERoles.Mafia);
            CitizensComboBox.ItemsSource = registeredPlayers.Where(p => p.Role == ERoles.Citizen);
            BossTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Boss)?.Nickname ?? "<unsetted>";
            SherifTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Sherif)?.Nickname ?? "<unsetted>";
            DoctorTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Doctor)?.Nickname ?? "<unsetted>";
            ManiacTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Maniac)?.Nickname ?? "<unsetted>";
        }

        private void SaveTeamBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TeamNameTextBox.Text))
            {
                MessageBox.Show("Error: you need to specify team name");
                return;
            }
            bool isUpdating = false;
            Team team;
            string oldTeamName = "";
            if ((TeamsComboBox.SelectedItem as Team) != null)
            {
                team = TeamsComboBox.SelectedItem as Team;
                oldTeamName = team.Name;
                team.Name = TeamNameTextBox.Text;
                isUpdating = true;
            }
            else
            {
                team = new Team()
                {
                    Name = TeamNameTextBox.Text
                };
            }
            foreach (var item in registeredPlayers)
            {
                if (team.Players.FirstOrDefault(p => p.Nickname == item.Nickname) == null)
                {
                    team.Players.Add(item);
                }
            }
            if (isUpdating)
            {
                teams.UpdateOne(new BsonDocument("Name", oldTeamName), new BsonDocument { { "$set", team.ToBsonDocument() } });
            }
            else
            {
                teams.InsertOne(team);
            }
        }

        private void TeamsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(TeamsComboBox.SelectedItem is Team team)) return;
            registeredPlayers.Clear();
            TeamNameTextBox.Text = team.Name;
            foreach (Player player in team.Players)
            {
                registeredPlayers.Add(player);
            }
            MafiaComboBox.ItemsSource = registeredPlayers.Where(p => p.Role == ERoles.Mafia);
            CitizensComboBox.ItemsSource = registeredPlayers.Where(p => p.Role == ERoles.Citizen);
            BossTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Boss)?.Nickname ?? "<unsetted>";
            SherifTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Sherif)?.Nickname ?? "<unsetted>";
            DoctorTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Doctor)?.Nickname ?? "<unsetted>";
            ManiacTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Maniac)?.Nickname ?? "<unsetted>";
        }

        private void RandomRoleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (RegisteredUsersListBox.SelectedItem == null)
            {
                MessageBox.Show("Error: you need to select a player from registered users to delete them.");
                return;
            }

            Random rand = new Random();
            int[] arr = (int[])Enum.GetValues(typeof(ERoles));
            selectedRole = Enum.Parse<ERoles>(rand.Next(arr.Min(), arr.Max()).ToString());
            
            registeredPlayers.FirstOrDefault(p => p.Nickname == (RegisteredUsersListBox.SelectedItem as Player).Nickname).Role = selectedRole;
            MafiaComboBox.ItemsSource = registeredPlayers.Where(p => p.Role == ERoles.Mafia);
            CitizensComboBox.ItemsSource = registeredPlayers.Where(p => p.Role == ERoles.Citizen);
            BossTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Boss)?.Nickname ?? "<unsetted>";
            SherifTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Sherif)?.Nickname ?? "<unsetted>";
            DoctorTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Doctor)?.Nickname ?? "<unsetted>";
            ManiacTextBlock.Text = registeredPlayers.FirstOrDefault(p => p.Role == ERoles.Maniac)?.Nickname ?? "<unsetted>";
        }
    }
}
