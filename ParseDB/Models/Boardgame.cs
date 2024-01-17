
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ParseDB.Models
{
    class Boardgame : INotifyPropertyChanged
    {
        private int gameId;
        private string name;
        private string image;
        private string description;

        private int rank;
        private int yearPublished;
        private int minPlayers;
        private int maxPlayers;
        private int suggestedPlayers;
        private int minAge;
        private int minPlaytime;
        private int maxPlaytime;
        private int usersRated;

        private int ownedNum;
        private double bayesAverage;

        public event PropertyChangedEventHandler PropertyChanged;

        [Key]
        public int GameId
        {
            get => gameId;
            set { gameId = value; OnPropertyChanged("GameId"); }
        }

        public string Name
        {
            get => name;
            set {
                if (value == "")
                {
                    name = "Неназваный";
                }
                else
                {
                    name = value;
                }
                
                OnPropertyChanged("Name"); }
        }

        public string Image
        {
            get => image;
            set { image = value; OnPropertyChanged("Image"); }
        }

        public string Description
        {
            get => description;
            set { description = value; OnPropertyChanged("Description"); }
        }

        public int Rank
        {
            get => rank;
            set { rank = value; OnPropertyChanged("Rank"); }
        }

        public int YearPublished
        {
            get => yearPublished;
            set { yearPublished = value; OnPropertyChanged("YearPublished"); }
        }

        public int MinPlayers
        {
            get => minPlayers;
            set { minPlayers = value; OnPropertyChanged("MinPlayers"); }
        }

        public int MaxPlayers
        {
            get => maxPlayers;
            set { maxPlayers = value; OnPropertyChanged("MaxPlayers"); }
        }

        public int SuggestedPlayers
        {
            get => suggestedPlayers;
            set { suggestedPlayers = value; OnPropertyChanged("SuggestedPlayers"); }
        }

        public int MinAge
        {
            get => minAge;
            set { minAge = value; OnPropertyChanged("MinAge"); }
        }

        public int MinPlaytime
        {
            get => minPlaytime;
            set { minPlaytime = value; OnPropertyChanged("MinPlaytime"); }
        }

        public int MaxPlaytime
        {
            get => maxPlaytime;
            set { maxPlaytime = value; OnPropertyChanged("MaxPlaytime"); }
        }

        public int UsersRated
        {
            get => usersRated;
            set { usersRated = value; OnPropertyChanged("UsersRated"); }
        }

        public int OwnedNum
        {
            get => ownedNum;
            set { ownedNum = value; OnPropertyChanged("OwnedNum"); }
        }

        public double BayesAverage
        {
            get => bayesAverage;
            set { bayesAverage = value; OnPropertyChanged("BayesAverage"); }
        }

        public virtual ObservableCollection<BoardgameType> Types { get; set; }
        public virtual ObservableCollection<Category> Categories { get; set; }
        public virtual ObservableCollection<Mechanic> Mechanics { get; set; }
        public virtual ObservableCollection<Artist> Artists { get; set; }
        public virtual ObservableCollection<Designer> Designers { get; set; }
        public virtual ObservableCollection<Family> Families { get; set; }
        public virtual ObservableCollection<Publisher> Publishers { get; set; }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Boardgame()
        {
            Types = new ObservableCollection<BoardgameType>();
            Categories = new ObservableCollection<Category>();
            Mechanics = new ObservableCollection<Mechanic>();
            Artists = new ObservableCollection<Artist>();
            Designers = new ObservableCollection<Designer>();
            Families = new ObservableCollection<Family>();
            Publishers = new ObservableCollection<Publisher>();
        }
    }
}
