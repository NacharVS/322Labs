using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MafiaGameWPF
{
    public class Team : INotifyPropertyChanged
    {
        private string name;
        public ObjectId Id { get; set; }
        public string Name 
        { 
            get => name;
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }
        public ObservableCollection<Player> Players { get; set; } = new ObservableCollection<Player>();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
