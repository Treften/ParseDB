using AngleSharp;
using AngleSharp.Dom;
using Newtonsoft.Json;
using ParseDB.Models;
using ParseDB.Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
using System.Xml;
using System.Data.Entity;
namespace ParseDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new ViewModel();
            DataContext = vm;
        
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var parser = new Parser();
            string url = "https://boardgamegeek.com/browse/boardgame";
             parser.Parse(url);
            using (BoardgameContext db = new BoardgameContext())
            {
               vm.Boardgames = new ObservableCollection<Boardgame>(db.Boardgames.Include(x => x.Types).Include(x => x.Categories).Include(x => x.Mechanics).Include(x => x.Families).Include(x => x.Artists).Include(x => x.Publishers).Include(x => x.Designers).OrderBy(x => x.Rank).ToList());
              
            }
          
        }
    }
}
