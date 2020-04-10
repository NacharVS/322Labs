using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using MongoDB.Bson;

namespace MafiaGameWPF
{
    public class Player : INotifyPropertyChanged
    {
        private string nickname;
        private ERoles role;
        private Team team;

        public ObjectId Id { get; set; }
        public string Nickname
        {
            get => nickname;
            set
            {
                nickname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Nickname)));
            }
        }
        public ERoles Role 
        { 
            get => role; 
            set
            {
                role = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Role)));
            } 
        }
        public Team Team
        {
            get => team;
            set
            {
                team = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Team)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
;