using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ParseDB.Models;
namespace ParseDB
{
    class BoardgameContext: DbContext
    {
        public BoardgameContext() : base("Boardgames")
        {
            this.Database.CommandTimeout = 180;
        }
        public DbSet<Boardgame> Boardgames { get; set; }
        public DbSet<BoardgameType> Types { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Designer> Designers { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Family> Families { get; set; }
    }
}
