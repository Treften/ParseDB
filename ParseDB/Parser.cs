using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using HtmlAgilityPack;
using System.Xml;
using Newtonsoft.Json;
using ParseDB.Models;
using System.Data.Entity;
using System.Windows.Controls;

namespace ParseDB
{
   

    namespace Parse
    {
        class Parser
        {
            string RequestThing(string id)
            {
                string reqStrTemplate = "https://boardgamegeek.com/xmlapi2/thing?id=" + id + "&stats=1";
                WebRequest req = WebRequest.Create(reqStrTemplate);
                WebResponse resp = req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream, Encoding.UTF8);
                return sr.ReadToEnd();
            }
            public async void Parse(string url)
            {
                using (BoardgameContext db = new BoardgameContext())
                {
                    IConfiguration config = Configuration.Default.WithDefaultLoader();
                    IBrowsingContext context = BrowsingContext.New(config);
                    IDocument doc = await context.OpenAsync(url);
                    var els = doc.QuerySelectorAll("a.primary");
                    ClearDatabase<BoardgameContext>();
                    for (int i = 0; i < els.Length; i++)
                    {
                        string id = GetIdFromURL(els[i].GetAttribute("href"));
                    XmlDocument xDoc = new XmlDocument();
                    int suggestedplayers=0;
                    int maxvotes = 0;
                    int buff = 0;
                    string Out = RequestThing(id);
                    xDoc.LoadXml(Out);
                    XmlElement? xRoot = xDoc.DocumentElement;
                    Boardgame boardgame = new Boardgame();
                        if (xRoot != null)
                        {

                            foreach (XmlElement xnode in xRoot)
                            {
                                XmlNode? attr = xnode.Attributes.GetNamedItem("type");
                                boardgame.GameId = Convert.ToInt32(xnode.Attributes.GetNamedItem("id").Value);
                                foreach (XmlNode childnode in xnode.ChildNodes)
                                {

                                    if (childnode.Name == "name" && childnode.Attributes.GetNamedItem("type").Value == "primary")
                                    {
                                        boardgame.Name = childnode.Attributes.GetNamedItem("value").Value;
                                    }
                                    if (childnode.Name == "image")
                                    {
                                        boardgame.Image = childnode.InnerText;
                                    }
                                    if (childnode.Name == "description")
                                    {
                                        boardgame.Description = childnode.InnerText;
                                    }
                                    if (childnode.Name == "minplayers")
                                    {
                                        boardgame.MinPlayers = Convert.ToInt32(childnode.Attributes.GetNamedItem("value").Value);
                                    }
                                    if (childnode.Name == "maxplayers")
                                    {
                                        boardgame.MaxPlayers = Convert.ToInt32(childnode.Attributes.GetNamedItem("value").Value);
                                    }
                                    if (childnode.Name == "yearpublished")
                                    {
                                        boardgame.YearPublished = Convert.ToInt32(childnode.Attributes.GetNamedItem("value").Value);
                                    }
                                    if (childnode.Name == "minplaytime")
                                    {
                                        boardgame.MinPlaytime = Convert.ToInt32(childnode.Attributes.GetNamedItem("value").Value);
                                    }
                                    if (childnode.Name == "maxplaytime")
                                    {
                                        boardgame.MaxPlaytime = Convert.ToInt32(childnode.Attributes.GetNamedItem("value").Value);
                                    }
                                    if (childnode.Name == "minage")
                                    {
                                        boardgame.MinAge = Convert.ToInt32(childnode.Attributes.GetNamedItem("value").Value);
                                    }
                                    if (childnode.Name == "poll" && childnode.Attributes.GetNamedItem("name").Value == "suggested_numplayers")
                                    {
                                        maxvotes = 0;
                                        suggestedplayers = 0;
                                        foreach (XmlNode pollchild in childnode.ChildNodes)
                                        {
                                            if (pollchild.Name == "results" && !pollchild.Attributes.GetNamedItem("numplayers").Value.Contains("more than"))
                                            {
                                                foreach (XmlNode pollchildchild in pollchild.ChildNodes)
                                                {
                                                    if (pollchildchild.Name == "result" && pollchildchild.Attributes.GetNamedItem("value").Value == "Best")
                                                    {
                                                        buff = Convert.ToInt32(pollchildchild.Attributes.GetNamedItem("numvotes").Value);
                                                        if (buff > maxvotes)
                                                        {
                                                            maxvotes = buff;
                                                            suggestedplayers = Convert.ToInt32(pollchild.Attributes.GetNamedItem("numplayers").Value);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (childnode.Name == "statistics")
                                    {
                                        foreach (XmlNode pollchild in childnode.ChildNodes)
                                        {
                                            foreach (XmlNode pollchildchild in pollchild.ChildNodes)
                                            {
                                                if (pollchildchild.Name == "usersrated")
                                                {
                                                    boardgame.UsersRated = Convert.ToInt32(pollchildchild.Attributes.GetNamedItem("value").Value);
                                                }
                                                if (pollchildchild.Name == "owned")
                                                {
                                                    boardgame.OwnedNum = Convert.ToInt32(pollchildchild.Attributes.GetNamedItem("value").Value);
                                                }
                                                if (pollchildchild.Name == "bayesaverage")
                                                {
                                                    boardgame.BayesAverage = Convert.ToDouble(pollchildchild.Attributes.GetNamedItem("value").Value.Replace(".", ","));
                                                }
                                                if (pollchildchild.Name == "ranks")
                                                {
                                                    foreach (XmlNode rank in pollchildchild.ChildNodes)
                                                    {
                                                        if (rank.Attributes.GetNamedItem("type").Value == "family")
                                                        {

                                                            BoardgameType t = (new BoardgameType { Name = rank.Attributes.GetNamedItem("name").Value, TypeId = Convert.ToInt32(rank.Attributes.GetNamedItem("id").Value) });
                                                            var ts = db.Types.Where(x => x.Name == t.Name).FirstOrDefault();
                                                            if (db.Types.Where(x => x.Name == t.Name).FirstOrDefault() == null)
                                                            {
                                                                db.Types.Add(t);
                                                                boardgame.Types.Add(t);
                                                            }
                                                            else
                                                            {
                                                                boardgame.Types.Add(ts);
                                                            }
                                                          
                                                        }

                                                        else
                                                        {
                                                            boardgame.Rank = Convert.ToInt32(rank.Attributes.GetNamedItem("value").Value);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (childnode.Name == "link" && childnode.Attributes.GetNamedItem("type").Value == "boardgamecategory")
                                    {

                                        Category c = (new Category
                                        {
                                            Name = childnode.Attributes.GetNamedItem("value").Value,
                                            CategoryId = Convert.ToInt32(childnode.Attributes.GetNamedItem("id").Value)
                                        });
                                        var ts = db.Categories.Where(x => x.Name == c.Name).FirstOrDefault();
                                        if (db.Categories.Where(x => x.Name == c.Name).FirstOrDefault() == null)
                                        {
                                            db.Categories.Add(c);
                                            boardgame.Categories.Add(c);
                                        }
                                        else
                                        {
                                            boardgame.Categories.Add(ts);
                                        }
                                     


                                    }
                                    if (childnode.Name == "link" && childnode.Attributes.GetNamedItem("type").Value == "boardgamemechanic")
                                    {

                                        Mechanic m = (new Mechanic { Name = childnode.Attributes.GetNamedItem("value").Value, MechanicId = Convert.ToInt32(childnode.Attributes.GetNamedItem("id").Value) });
                                        var ts = db.Mechanics.Where(x => x.Name == m.Name).FirstOrDefault();
                                        if (db.Mechanics.Where(x => x.Name == m.Name).FirstOrDefault() == null)
                                        {
                                            db.Mechanics.Add(m);
                                            boardgame.Mechanics.Add(m);
                                        }
                                        else
                                        {
                                            boardgame.Mechanics.Add(ts);
                                        }
                                      


                                    }
                                    if (childnode.Name == "link" && childnode.Attributes.GetNamedItem("type").Value == "boardgamefamily")
                                    {

                                        Family f = (new Family { Name = childnode.Attributes.GetNamedItem("value").Value, FamilyId = Convert.ToInt32(childnode.Attributes.GetNamedItem("id").Value) });
                                        var ts = db.Families.Where(x => x.Name == f.Name).FirstOrDefault();
                                        if (db.Families.Where(x => x.Name == f.Name).FirstOrDefault() == null)
                                        {
                                            db.Families.Add(f);
                                            boardgame.Families.Add(f);
                                        }
                                        else
                                        {
                                            boardgame.Families.Add(ts);
                                        }
                                 

                                    }


                                    if (childnode.Name == "link" && childnode.Attributes.GetNamedItem("type").Value == "boardgamedesigner")
                                    {

                                        Designer d = (new Designer { Name = childnode.Attributes.GetNamedItem("value").Value, DesignerId = Convert.ToInt32(childnode.Attributes.GetNamedItem("id").Value) });
                                        var ts = db.Designers.Where(x => x.Name == d.Name).FirstOrDefault();
                                        if (db.Designers.Where(x => x.Name == d.Name).FirstOrDefault() == null)
                                        {
                                            db.Designers.Add(d);
                                            boardgame.Designers.Add(d);
                                        }
                                        else
                                        {
                                            boardgame.Designers.Add(ts);
                                        }


                                    }
                                    if (childnode.Name == "link" && childnode.Attributes.GetNamedItem("type").Value == "boardgameartist")
                                    {

                                        Artist a = (new Artist { Name = childnode.Attributes.GetNamedItem("value").Value, ArtistId = Convert.ToInt32(childnode.Attributes.GetNamedItem("id").Value) });
                                        var ts = db.Artists.Where(x => x.Name == a.Name).FirstOrDefault();
                                        if (db.Artists.Where(x => x.Name == a.Name).FirstOrDefault() == null)
                                        {
                                            db.Artists.Add(a);
                                            boardgame.Artists.Add(a);
                                        }
                                        else
                                        {
                                            boardgame.Artists.Add(ts);
                                        }


                                    }
                                    if (childnode.Name == "link" && childnode.Attributes.GetNamedItem("type").Value == "boardgamepublisher")
                                    {

                                        Publisher p = (new Publisher { Name = childnode.Attributes.GetNamedItem("value").Value, PublisherId = Convert.ToInt32(childnode.Attributes.GetNamedItem("id").Value) });
                                        var ts = db.Publishers.Where(x => x.Name == p.Name).FirstOrDefault();
                                        if (db.Publishers.Where(x => x.Name == p.Name).FirstOrDefault() == null)
                                        {
                                            db.Publishers.Add(p);
                                            boardgame.Publishers.Add(p);
                                        }
                                        else
                                        {
                                            boardgame.Publishers.Add(ts);
                                        }


                                    }

                                }
                                boardgame.SuggestedPlayers = suggestedplayers;
                                db.Boardgames.Add(boardgame);
                                db.SaveChanges();
                                Thread.Sleep(820);
                            }
                        }
                    }
                     
                    }
                
            }
 
            public static string GetIdFromURL(string input)
            {
                string[] s = input.Split('/');

                return s[2];
            }
            public static void ClearDatabase<T>() where T : DbContext, new()
            {
                using (var context = new T())
                {
                    var tableNames = context.Database.SqlQuery<string>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT LIKE '%Migration%'").ToList();
                    foreach (var tableName in tableNames)
                    {
                        context.Database.ExecuteSqlCommand(string.Format("DELETE FROM {0}", tableName));
                    }

                    context.SaveChanges();
                }
            }

        }

}
}
