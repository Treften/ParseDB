using ParseDB.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Drawing.Chart;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System.Reflection;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using Document = Microsoft.Office.Interop.Word.Document;
using Shape = Microsoft.Office.Interop.Word.Shape;

namespace ParseDB
{
    class ViewModel
    {
        private Boardgame selectedGame;
        private Artist selectedArtist;
        private BoardgameType selectedType;
        private Models.Category selectedCategory;
        private Designer selectedDesigner;
        private Family selectedFamily;
        private Mechanic selectedMechanic;
        private Publisher selectedPublisher;
        private bool usersChart;
        private bool ownedChart;
        private bool averageChart;
        private bool typesChart;
        private bool categoriesChart;
        private bool mechanicsChart;
        private bool familiesChart;
        private bool artistsChart;
        private bool designersChart;
        private bool publishersChart;
        private string searchText;
        #region Properties
        public bool UsersChart
        {
            get { return usersChart; }
            set
            {
                usersChart = value;
                OnPropertyChanged("UsersChart");
            }
        }
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged("SearchText");
            }
        }
        public bool OwnedChart
        {
            get { return ownedChart; }
            set
            {
                ownedChart = value;
                OnPropertyChanged("OwnedChart");
            }
        }
        public bool AverageChart
        {
            get { return averageChart; }
            set
            {
                averageChart = value;
                OnPropertyChanged("AverageChart");
            }
        }
        public bool TypesChart
        {
            get { return typesChart; }
            set
            {
                typesChart = value;
                OnPropertyChanged("TypesChart");
            }
        }
        public bool CategoriesChart
        {
            get { return categoriesChart; }
            set
            {
                categoriesChart = value;
                OnPropertyChanged("CategoriesChart");
            }
        }
        public bool MechanicsChart
        {
            get { return mechanicsChart; }
            set
            {
                mechanicsChart = value;
                OnPropertyChanged("MechanicsChart");
            }
        }
        public bool FamiliesChart
        {
            get { return familiesChart; }
            set
            {
                familiesChart = value;
                OnPropertyChanged("FamiliesChart");
            }
        }
        public bool ArtistsChart
        {
            get { return artistsChart; }
            set
            {
                artistsChart = value;
                OnPropertyChanged("ArtistsChart");
            }
        }
        public bool DesignersChart
        {
            get { return designersChart; }
            set
            {
                designersChart = value;
                OnPropertyChanged("DesignersChart");
            }
        }
        public bool PublishersChart
        {
            get { return publishersChart; }
            set
            {
                publishersChart = value;
                OnPropertyChanged("PublishersChart");
            }
        }
        #endregion 
        public ObservableCollection<Boardgame> Boardgames { get; set; }
      
        public ObservableCollection<Boardgame> Backup { get; set; }
        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                    (deleteCommand = new RelayCommand(obj =>
                    {
                       if (SelectedGame!=null)
                        {
                        using (BoardgameContext db = new BoardgameContext())
                        {
                            Boardgame del = db.Boardgames.Find(SelectedGame.GameId);
                            db.Boardgames.Remove(del);
                            db.SaveChanges();
                        }
                        Boardgames.Remove(SelectedGame);
                        }
                    }));
            }
        }
        private RelayCommand deleteType;
        public RelayCommand DeleteType
        {
            get
            {
                return deleteType ??
                    (deleteType = new RelayCommand(obj =>
                    {
                        if (SelectedType != null)
                        {
                            using (BoardgameContext db = new BoardgameContext())
                            {

                                BoardgameType t = db.Types.Find(SelectedType.TypeId);
                                db.Boardgames.Find(SelectedGame.GameId).Types.Remove(t);
                                db.SaveChanges();
                            }
                            SelectedGame.Types.Remove(SelectedType);
                        }
                    }));
            }
        }
        private RelayCommand deleteCategory;
        public RelayCommand DeleteCategory
        {
            get
            {
                return deleteCategory ??
                    (deleteCategory = new RelayCommand(obj =>
                    {
                        if (SelectedCategory != null)
                        {
                            using (BoardgameContext db = new BoardgameContext())
                            {

                                Models.Category t = db.Categories.Find(SelectedCategory.CategoryId);
                                db.Boardgames.Find(SelectedGame.GameId).Categories.Remove(t);
                                db.SaveChanges();
                            }
                            SelectedGame.Categories.Remove(SelectedCategory);
                        }
                    }));
            }
        }
        private RelayCommand deleteMechanic;
        public RelayCommand DeleteMechanic
        {
            get
            {
                return deleteMechanic ??
                    (deleteMechanic = new RelayCommand(obj =>
                    {
                        if (SelectedMechanic != null)
                        {
                            using (BoardgameContext db = new BoardgameContext())
                            {

                                Mechanic t = db.Mechanics.Find(SelectedMechanic.MechanicId);
                                db.Boardgames.Find(SelectedGame.GameId).Mechanics.Remove(t);
                                db.SaveChanges();
                            }
                            SelectedGame.Mechanics.Remove(SelectedMechanic);
                        }
                    }));
            }
        }
        private RelayCommand deleteFamily;
        public RelayCommand DeleteFamily
        {
            get
            {
                return deleteFamily ??
                    (deleteFamily = new RelayCommand(obj =>
                    {
                        if (SelectedFamily != null)
                        {
                            using (BoardgameContext db = new BoardgameContext())
                            {

                                Family t = db.Families.Find(SelectedFamily.FamilyId);
                                db.Boardgames.Find(SelectedGame.GameId).Families.Remove(t);
                                db.SaveChanges();
                            }
                            SelectedGame.Families.Remove(SelectedFamily);
                        }
                    }));
            }
        }
        private RelayCommand deleteArtist;
        public RelayCommand DeleteArtist
        {
            get
            {
                return deleteArtist ??
                    (deleteArtist = new RelayCommand(obj =>
                    {
                        if (SelectedArtist != null)
                        {
                            using (BoardgameContext db = new BoardgameContext())
                            {

                                Artist t = db.Artists.Find(SelectedArtist.ArtistId);
                                db.Boardgames.Find(SelectedGame.GameId).Artists.Remove(t);
                                db.SaveChanges();
                            }
                            SelectedGame.Artists.Remove(SelectedArtist);
                        }
                    }));
            }
        }
        private RelayCommand deleteDesigner;
        public RelayCommand DeleteDesigner
        {
            get
            {
                return deleteDesigner ??
                    (deleteDesigner = new RelayCommand(obj =>
                    {
                        if (SelectedDesigner != null)
                        {
                            using (BoardgameContext db = new BoardgameContext())
                            {

                                Designer t = db.Designers.Find(SelectedDesigner.DesignerId);
                                db.Boardgames.Find(SelectedGame.GameId).Designers.Remove(t);
                                db.SaveChanges();
                            }
                            SelectedGame.Designers.Remove(SelectedDesigner);
                        }
                    }));
            }
        }
        private RelayCommand deletePublisher;
        public RelayCommand DeletePublisher
        {
            get
            {
                return deletePublisher ??
                    (deletePublisher = new RelayCommand(obj =>
                    {
                        if (SelectedPublisher != null)
                        {
                            using (BoardgameContext db = new BoardgameContext())
                            {

                                Publisher t = db.Publishers.Find(SelectedPublisher.PublisherId);
                                db.Boardgames.Find(SelectedGame.GameId).Publishers.Remove(t);
                                db.SaveChanges();
                            }
                            SelectedGame.Publishers.Remove(SelectedPublisher);
                        }
                    }));
            }
        }
        private RelayCommand addType;
        public RelayCommand AddType
        {
            get
            {
                return addType ??
                    (addType = new RelayCommand(obj =>
                    {
                        if (NameToAdd1 != null&&SelectedGame!=null)
                        {
                            using (BoardgameContext db = new BoardgameContext())
                            {
                                BoardgameType t = new BoardgameType { Name = NameToAdd1 };
                                SelectedGame.Types.Add(t) ;
                                db.Boardgames.Find(SelectedGame.GameId).Types.Add(t);
                                db.SaveChanges();
                            }
                         
                        }
                    }));
            }
        }
        private RelayCommand addCategory;
        public RelayCommand AddCategory
        {
            get
            {
                return addCategory ??
                    (addCategory = new RelayCommand(obj =>
                    {
                        if (NameToAdd1 != null && SelectedGame != null)
                        {
                            using (BoardgameContext db = new BoardgameContext())
                            {
                                Models.Category t = new Models.Category { Name = NameToAdd1 };
                                SelectedGame.Categories.Add(t);
                                db.Boardgames.Find(SelectedGame.GameId).Categories.Add(t);
                                db.SaveChanges();
                            }

                        }
                    }));
            }
        }
        private RelayCommand addMechanic;
        public RelayCommand AddMechanic
        {
            get
            {
                return addMechanic ??
                    (addMechanic = new RelayCommand(obj =>
                    {
                        if (NameToAdd1 != null && SelectedGame != null)
                        {
                            using (BoardgameContext db = new BoardgameContext())
                            {
                                Mechanic t = new Mechanic { Name = NameToAdd1 };
                                SelectedGame.Mechanics.Add(t);
                                db.Boardgames.Find(SelectedGame.GameId).Mechanics.Add(t);
                                db.SaveChanges();
                            }

                        }
                    }));
            }
        }
        private RelayCommand addFamily;
        public RelayCommand AddFamily
        {
            get
            {
                return addFamily ??
                    (addFamily = new RelayCommand(obj =>
                    {
                        if (NameToAdd1 != null && SelectedGame != null)
                        {
                            using (BoardgameContext db = new BoardgameContext())
                            {
                                Family t = new Family { Name = NameToAdd1 };
                                SelectedGame.Families.Add(t);
                                db.Boardgames.Find(SelectedGame.GameId).Families.Add(t);
                                db.SaveChanges();
                            }

                        }
                    }));
            }
        }

        private RelayCommand addArtist;
        public RelayCommand AddArtist
        {
            get
            {
                return addArtist ??
                    (addArtist = new RelayCommand(obj =>
                    {
                        if (NameToAdd2 != null && SelectedGame != null)
                        {
                            using (BoardgameContext db = new BoardgameContext())
                            {
                                Artist t = new Artist { Name = NameToAdd2 };
                                SelectedGame.Artists.Add(t);
                                db.Boardgames.Find(SelectedGame.GameId).Artists.Add(t);
                                db.SaveChanges();
                            }

                        }
                    }));
            }
        }

        private RelayCommand addDesigner;
        public RelayCommand AddDesigner
        {
            get
            {
                return addDesigner ??
                    (addDesigner = new RelayCommand(obj =>
                    {
                        if (NameToAdd2 != null && SelectedGame != null)
                        {
                            using (BoardgameContext db = new BoardgameContext())
                            {
                                Designer t = new Designer { Name = NameToAdd2 };
                                SelectedGame.Designers.Add(t);
                                db.Boardgames.Find(SelectedGame.GameId).Designers.Add(t);
                                db.SaveChanges();
                            }

                        }
                    }));
            }
        }

        private RelayCommand addPublisher;
        public RelayCommand AddPublisher
        {
            get
            {
                return addPublisher ??
                    (addPublisher = new RelayCommand(obj =>
                    {
                        if (NameToAdd2 != null && SelectedGame != null)
                        {
                            using (BoardgameContext db = new BoardgameContext())
                            {
                                Publisher t = new Publisher { Name = NameToAdd2 };
                                SelectedGame.Publishers.Add(t);
                                db.Boardgames.Find(SelectedGame.GameId).Publishers.Add(t);
                                db.SaveChanges();
                            }

                        }
                    }));
            }
        }

        string EndOfRow(ExcelWorksheet w, string c)
        {
            int i = 2;

            while (w.Cells[c + i.ToString()].Value != null)
            {
                    i++;
            }
            return (i-1).ToString();
        }
        private RelayCommand makeCharts;
        public RelayCommand MakeCharts
        {
            get
            {
                return makeCharts ??
                    (makeCharts = new RelayCommand(obj =>
                    {
                        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                        using (var p = new ExcelPackage())
                        {
                            var ws = p.Workbook.Worksheets.Add("MySheet");
                            ws.Cells["A1"].Value = "Name";
                            ws.Cells["B1"].Value = "UsersRated";
                            ws.Cells["C1"].Value = "OwnedNum";
                            ws.Cells["D1"].Value = "BayesAverage";

                            for (int i = 0; i < Boardgames.Count; i++)
                            {
                                ws.Cells["A" + (i + 2).ToString()].Value = (Boardgames[i].Name);
                                ws.Cells["B" + (i + 2).ToString()].Value = (Boardgames[i].UsersRated);
                                ws.Cells["C" + (i + 2).ToString()].Value = (Boardgames[i].OwnedNum);
                                ws.Cells["D" + (i + 2).ToString()].Value = (Boardgames[i].BayesAverage);
                            }
                            using (BoardgameContext db = new BoardgameContext())
                            {
                                BoardgameType[] types = db.Types.Include(x=>x.Boardgames).ToArray();
                                Models.Category[] cats = db.Categories.Include(x => x.Boardgames).ToArray();
                                Mechanic[] mechs = db.Mechanics.Include(x => x.Boardgames).ToArray();
                                Family[] fams = db.Families.Include(x => x.Boardgames).ToArray();
                                Artist[] arts = db.Artists.Include(x => x.Boardgames).ToArray();
                                Designer[] des = db.Designers.Include(x => x.Boardgames).ToArray();
                                Publisher[] pubs = db.Publishers.Include(x => x.Boardgames).ToArray();
                                for (int i = 0; i <types.Length ; i++)
                                {
                                    ws.Cells["E" + (i + 2).ToString()].Value = types[i].Name;
                                    ws.Cells["F" + (i + 2).ToString()].Value = types[i].Boardgames.Count();
                                }
                                for (int i = 0; i < cats.Length; i++)
                                {
                                    ws.Cells["G" + (i + 2).ToString()].Value = cats[i].Name;
                                    ws.Cells["H" + (i + 2).ToString()].Value = cats[i].Boardgames.Count();
                                }
                                for (int i = 0; i < mechs.Length; i++)
                                {
                                    ws.Cells["I" + (i + 2).ToString()].Value = mechs[i].Name;
                                    ws.Cells["J" + (i + 2).ToString()].Value = mechs[i].Boardgames.Count();
                                }
                                for (int i = 0; i < fams.Length; i++)
                                {
                                    ws.Cells["K" + (i + 2).ToString()].Value = fams[i].Name;
                                    ws.Cells["L" + (i + 2).ToString()].Value = fams[i].Boardgames.Count();
                                }
                                for (int i = 0; i < arts.Length; i++)
                                {
                                    ws.Cells["M" + (i + 2).ToString()].Value = arts[i].Name;
                                    ws.Cells["N" + (i + 2).ToString()].Value = arts[i].Boardgames.Count();
                                }
                                for (int i = 0; i < des.Length; i++)
                                {
                                    ws.Cells["O" + (i + 2).ToString()].Value = des[i].Name;
                                    ws.Cells["P" + (i + 2).ToString()].Value = des[i].Boardgames.Count();
                                }
                                for (int i = 0; i < pubs.Length; i++)
                                {
                                    ws.Cells["Q" + (i + 2).ToString()].Value = pubs[i].Name;
                                    ws.Cells["R" + (i + 2).ToString()].Value = pubs[i].Boardgames.Count();
                                }

                            }
                            if (UsersChart)
                            {
                                var barChartUsers = (ExcelBarChart)ws.Drawings.AddChart("Users", eChartType.ColumnClustered);
                                barChartUsers.SetPosition(1, 0, 20, 0);
                                barChartUsers.SetSize(3600, 600);
                                barChartUsers.Series.Add("B2:B"+EndOfRow(ws,"B"), "A2:A" + EndOfRow(ws, "A"));

                                barChartUsers.Title.Text = "Пользователей оценило:";

                                barChartUsers.DataLabel.ShowCategory = false;
                                barChartUsers.DataLabel.ShowPercent = false;
                                barChartUsers.DataLabel.ShowLeaderLines = true;
                                barChartUsers.Legend.Remove();
                                barChartUsers.DataLabel.ShowValue = false;
                            }
                            if (OwnedChart)
                            {
                                var barChartOwned = (ExcelBarChart)ws.Drawings.AddChart("Owned", eChartType.ColumnClustered);
                                barChartOwned.SetPosition(32, 0, 20, 0);
                                barChartOwned.SetSize(3600, 600);
                                barChartOwned.Series.Add("C2:C" + EndOfRow(ws, "C"), "A2:A" + EndOfRow(ws, "A"));

                                barChartOwned.Title.Text = "Пользователей имеет:";

                                barChartOwned.DataLabel.ShowCategory = false;
                                barChartOwned.DataLabel.ShowPercent = false;
                                barChartOwned.DataLabel.ShowLeaderLines = true;
                                barChartOwned.Legend.Remove();
                                barChartOwned.DataLabel.ShowValue = false;
                            }
                            if (AverageChart)
                            {
                                var barChartAverage = (ExcelBarChart)ws.Drawings.AddChart("Average", eChartType.ColumnClustered);
                                barChartAverage.SetPosition(62, 0, 20, 0);
                                barChartAverage.SetSize(3600, 600);
                                barChartAverage.Series.Add("D2:D"+EndOfRow(ws, "D"), "A2:A"+EndOfRow(ws, "A"));

                                barChartAverage.Title.Text = "Средний рейтинг:";

                                barChartAverage.DataLabel.ShowCategory = false;
                                barChartAverage.DataLabel.ShowPercent = false;
                                barChartAverage.DataLabel.ShowLeaderLines = true;
                                barChartAverage.Legend.Remove();
                                barChartAverage.DataLabel.ShowValue = false;
                            }
                            if (TypesChart)
                            {
                                var barChartAverage = (ExcelBarChart)ws.Drawings.AddChart("Types", eChartType.ColumnClustered);
                                barChartAverage.SetPosition(92, 0, 20, 0);
                                barChartAverage.SetSize(1200, 600);
                                barChartAverage.Series.Add("F2:F" + EndOfRow(ws, "F"), "E2:E" + EndOfRow(ws, "E"));

                                barChartAverage.Title.Text = "Типы::";

                                barChartAverage.DataLabel.ShowCategory = false;
                                barChartAverage.DataLabel.ShowPercent = false;
                                barChartAverage.DataLabel.ShowLeaderLines = true;
                                barChartAverage.Legend.Remove();
                                barChartAverage.DataLabel.ShowValue = false;
                            }
                            if (CategoriesChart)
                            {
                                var barChartAverage = (ExcelBarChart)ws.Drawings.AddChart("Categories", eChartType.ColumnClustered);
                                barChartAverage.SetPosition(122, 0, 20, 0);
                                barChartAverage.SetSize(2400, 600);
                                barChartAverage.Series.Add("H2:H" + EndOfRow(ws, "H"), "G2:G" + EndOfRow(ws, "G"));

                                barChartAverage.Title.Text = "Категории:";

                                barChartAverage.DataLabel.ShowCategory = false;
                                barChartAverage.DataLabel.ShowPercent = false;
                                barChartAverage.DataLabel.ShowLeaderLines = true;
                                barChartAverage.Legend.Remove();
                                barChartAverage.DataLabel.ShowValue = false;
                            }
                            if (FamiliesChart)
                            {
                                var barChartAverage = (ExcelBarChart)ws.Drawings.AddChart("Families", eChartType.ColumnClustered);
                                barChartAverage.SetPosition(152, 0, 20, 0);
                                barChartAverage.SetSize(3600, 600);
                                barChartAverage.Series.Add("J2:J" + EndOfRow(ws, "J"), "I2:I" + EndOfRow(ws, "I"));

                                barChartAverage.Title.Text = "Семьи:";

                                barChartAverage.DataLabel.ShowCategory = false;
                                barChartAverage.DataLabel.ShowPercent = false;
                                barChartAverage.DataLabel.ShowLeaderLines = true;
                                barChartAverage.Legend.Remove();
                                barChartAverage.DataLabel.ShowValue = false;
                            }
                            if (ArtistsChart)
                            {
                                var barChartAverage = (ExcelBarChart)ws.Drawings.AddChart("Artists", eChartType.ColumnClustered);
                                barChartAverage.SetPosition(182, 0, 20, 0);
                                barChartAverage.SetSize(3600, 600);
                                barChartAverage.Series.Add("N2:N" + EndOfRow(ws, "N"), "M2:M" + EndOfRow(ws, "M"));

                                barChartAverage.Title.Text = "Художники::";

                                barChartAverage.DataLabel.ShowCategory = false;
                                barChartAverage.DataLabel.ShowPercent = false;
                                barChartAverage.DataLabel.ShowLeaderLines = true;
                                barChartAverage.Legend.Remove();
                                barChartAverage.DataLabel.ShowValue = false;
                            }
                            if (DesignersChart)
                            {
                                var barChartAverage = (ExcelBarChart)ws.Drawings.AddChart("Designers", eChartType.ColumnClustered);
                                barChartAverage.SetPosition(212, 0, 20, 0);
                                barChartAverage.SetSize(3600, 600);
                                barChartAverage.Series.Add("P2:P" + EndOfRow(ws, "P"), "O2:O" + EndOfRow(ws, "O"));

                                barChartAverage.Title.Text = "Дизайнеры:";

                                barChartAverage.DataLabel.ShowCategory = false;
                                barChartAverage.DataLabel.ShowPercent = false;
                                barChartAverage.DataLabel.ShowLeaderLines = true;
                                barChartAverage.Legend.Remove();
                                barChartAverage.DataLabel.ShowValue = false;
                            }
                            if (PublishersChart)
                            {
                                var barChartAverage = (ExcelBarChart)ws.Drawings.AddChart("Publishers", eChartType.ColumnClustered);
                                barChartAverage.SetPosition(242, 0, 20, 0);
                                barChartAverage.SetSize(3600, 600);
                                barChartAverage.Series.Add("R2:R" + EndOfRow(ws, "R"), "Q2:Q" + EndOfRow(ws, "Q"));

                                barChartAverage.Title.Text = "Издатели:";

                                barChartAverage.DataLabel.ShowCategory = false;
                                barChartAverage.DataLabel.ShowPercent = false;
                                barChartAverage.DataLabel.ShowLeaderLines = true;
                                barChartAverage.Legend.Remove();
                                barChartAverage.DataLabel.ShowValue = false;
                            }
                            string dir= @"c:\workbooks1";
                            Directory.CreateDirectory(dir);
                                p.SaveAs(new FileInfo(dir+@"\Charts.xlsx"));
                          
                            
                              
                            
                        }

                    }));
            }
        }
        private RelayCommand updateGame;
        public RelayCommand UpdateGame
        {
            get
            {
                return updateGame ??
                    (updateGame = new RelayCommand(obj =>
                    {
                        if (SelectedGame != null)
                        {
                            using (BoardgameContext db = new BoardgameContext())
                            {
                                db.Boardgames.Attach(SelectedGame);
                                db.Entry(SelectedGame).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                               //  Boardgames = new ObservableCollection<Boardgame>(db.Boardgames.Include(x => x.Types).Include(x => x.Categories).Include(x => x.Mechanics).Include(x => x.Families).Include(x => x.Artists).Include(x => x.Publishers).Include(x => x.Designers).ToList());
                                
                            }
                        }
                    }));
            }
        }
        private RelayCommand saveReport;
        public RelayCommand SaveReport
        {
            get
            {
                return saveReport ??
                    (saveReport = new RelayCommand(obj =>
                    {
                        if (SelectedGame != null)
                        {
                            using (BoardgameContext db = new BoardgameContext())
                            {

                                FillWordTemplate(@"c:\workbooks\test",SelectedGame);
                            }
                        }
                    }));
            }
        }

        private RelayCommand search;
        public RelayCommand Search
        {
            get
            {
                return search ??
                    (search = new RelayCommand(obj =>
                    {
                        Boardgames.Clear();
                       List<Boardgame> ItemsFound = Backup.Where(x=>x.Name.ToLower().Contains(SearchText.ToLower())).ToList();
                        foreach (var item in ItemsFound)
                        {
                            Boardgames.Add(item);
                        }
                    }));
            }
        }

        public Boardgame SelectedGame
        {
            get { return selectedGame; }
            set
            {
                selectedGame = value;
                OnPropertyChanged("SelectedGame");
            }
        }
        string nameToAdd1;
        public string NameToAdd1
        {
            get { return nameToAdd1; }
            set
            {
               
                if (value == "")
                {
                    nameToAdd1 = "Неназваный";
                }
                else
                {
                    nameToAdd1 = value;
                }
                OnPropertyChanged("NameToAdd1");
            }
        }
        string nameToAdd2;
        public string NameToAdd2
        {
            get { return nameToAdd2; }
            set
            {
                if (value == "")
                {
                    nameToAdd2 = "Неназваный";
                }
                else
                {
                    nameToAdd2 = value;
                }
                OnPropertyChanged("NameToAdd2");
            }
        }
        public BoardgameType SelectedType
        {
            get { return selectedType; }
            set
            {
                selectedType = value;
                OnPropertyChanged("SelectedType");
            }
        }
        public Artist SelectedArtist
        {
            get { return selectedArtist; }
            set
            {
                selectedArtist = value;
                OnPropertyChanged("SelectedArtist");
            }
        }
        public Models.Category SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }
        public Designer SelectedDesigner
        {
            get { return selectedDesigner; }
            set
            {
                selectedDesigner = value;
                OnPropertyChanged("SelectedDesigner");
            }
        }
        public Family SelectedFamily
        {
            get { return selectedFamily; }
            set
            {
                selectedFamily = value;
                OnPropertyChanged("SelectedFamily");
            }
        }
        public Mechanic SelectedMechanic
        {
            get { return selectedMechanic; }
            set
            {
                selectedMechanic = value;
                OnPropertyChanged("SelectedMechanic");
            }
        }
        public Publisher SelectedPublisher
        {
            get { return selectedPublisher; }
            set
            {
                selectedPublisher = value;
                OnPropertyChanged("SelectedPublisher");
            }
        }

        public void FillWordTemplate(string templatePath,  Boardgame game)
        {
            string dir = @"c:\workbooks";
            using (WordprocessingDocument olddoc = WordprocessingDocument.Open(templatePath + ".dotx", true))
            {

                using (WordprocessingDocument document = (WordprocessingDocument)olddoc.Clone(templatePath + "report.doc"))
                {
                    IEnumerable<Text> fields = document.MainDocumentPart.Document.Descendants<Text>();
                    string s = "";
                    string sep = ", ";
                    foreach (Text field in fields)
                    {
                        switch (field.Text)
                        {
                            case "Название":
                                field.Text = game.Name;
                                break;
                            case "Описание":
                                field.Text = "Описание: " + game.Description;
                                break;
                            case "Ранг":
                                field.Text = "Ранг: " + game.Rank.ToString();
                                break;
                            case "Год выхода":
                                field.Text = "Год выхода: " + game.YearPublished.ToString();
                                break;
                            case "Минимум игроков":
                                field.Text = "Минимум игроков: " + game.MinPlayers.ToString();
                                break;
                            case "Максимум игроков":
                                field.Text = "Максимум игроков: " + game.MaxPlayers.ToString();
                                break;
                            case "Рек игроков":
                                field.Text = "Рек игроков: " + game.SuggestedPlayers.ToString();
                                break;
                            case "Мин возраст":
                                field.Text = "Мин возраст: " + game.MinAge.ToString();
                                break;
                            case "Мин время":
                                field.Text = "Мин время: " + game.MinPlaytime.ToString();
                                break;
                            case "Макс время":
                                field.Text = "Макс время: " + game.MaxPlaytime.ToString();
                                break;
                            case "Оценок":
                                field.Text = "Оценок: " + game.UsersRated.ToString();
                                break;
                            case "Имеют":
                                field.Text = "Имеют: " + game.OwnedNum.ToString();
                                break;
                            case "Оценка":
                                field.Text = "Оценка: " + game.BayesAverage.ToString();
                                break;
                            case "Типы":
                                s = "Типы: ";
                                foreach (BoardgameType type in game.Types)
                                {
                                    s = s + type.Name + Environment.NewLine;
                                }
                                field.Text = s;
                                break;
                            case "Категории":
                                s = "Категории: ";
                                foreach (Models.Category type in game.Categories)
                                {
                                    s = s + type.Name + sep;
                                }
                                field.Text = s;
                                break;
                            case "Механики":
                                s = "Механики: ";
                                foreach (Mechanic type in game.Mechanics)
                                {
                                    s = s + type.Name + sep;
                                }
                                field.Text = s;
                                break;
                            case "Семьи":
                                s = "Семьи: ";
                                foreach (Family type in game.Families)
                                {
                                    s = s + type.Name + sep;
                                }
                                field.Text = s;
                                break;
                            case "Художники":
                                s = "Художники: ";
                                foreach (Artist type in game.Artists)
                                {
                                    s = s + type.Name + sep;
                                }
                                field.Text = s;
                                break;
                            case "Дизайнеры":
                                s = "Дизайнеры: ";
                                foreach (Designer type in game.Designers)
                                {
                                    s = s + type.Name + sep;
                                }
                                field.Text = s;
                                break;
                            case "Издатели":
                                s = "Издатели: ";
                                foreach (Publisher type in game.Publishers)
                                {
                                    s = s + type.Name + sep;
                                }
                                field.Text = s;
                                break;
                        }

                    }

                    document.Save();


                }
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                var workbooks = excelApp.Workbooks;
                var workbook = workbooks.Add();
                var sheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets.Add();
                var charts = (Microsoft.Office.Interop.Excel.ChartObjects)sheet.ChartObjects(Type.Missing);
                for (int i = 0; i < Boardgames.Count; i++)
                {
                    sheet.Cells[(i+2),1] = (Boardgames[i].Name);
                    sheet.Cells[ (i + 2),2] = (Boardgames[i].UsersRated);

                }
                workbook.SaveAs("c:\\workbooks\\test505.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                var chartObject = charts.Add(60, 10, 3600, 600);
                var chart = chartObject.Chart;

                chart.SetSourceData(sheet.get_Range("B2", "B101")) ;
                chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlColumnStacked;
       
                var word = new Microsoft.Office.Interop.Word.Application();
                word.Visible = true;
                Document doc = word.Documents.Open(templatePath + "report.doc");

                string tempImagePath = System.IO.Path.GetTempPath() + "chart.png";
                chart.Export(tempImagePath, "PNG", false);

                object missing = Missing.Value;
                object start = doc.Content.End - 1;
                Microsoft.Office.Interop.Word.Range range = doc.Range(ref start, ref missing);
                range.InsertParagraphAfter();
                InlineShape picture = doc.InlineShapes.AddPicture(tempImagePath, false, true, range);
                picture.Width = (float)chartObject.Width;
                picture.Height = (float)chartObject.Height;
                doc.SaveAs(templatePath + "report.doc");
                System.IO.File.Delete(tempImagePath);

                word.Quit();
                workbook.Close(false);
                excelApp.Quit();

                ReleaseComObject(chart);
                ReleaseComObject(workbooks);
                ReleaseComObject(workbook);
                ReleaseComObject(excelApp);
                ReleaseComObject(doc);
                ReleaseComObject(word);
            }
        }
        private static void ReleaseComObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                Console.WriteLine("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        public ViewModel()
        {
            using(BoardgameContext db = new BoardgameContext())
            {
                Boardgames = new ObservableCollection<Boardgame>(db.Boardgames.Include(x=>x.Types).Include(x=>x.Categories).Include(x=>x.Mechanics).Include(x=>x.Families).Include(x=>x.Artists).Include(x=>x.Publishers).Include(x=>x.Designers).OrderBy(x=>x.Rank).ToList());
                Backup = new ObservableCollection<Boardgame>(Boardgames);
            }
           
            {
            };
        }
     
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

