using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseDB.Models
{
    class Artist
    {
        [Key]
        public int ArtistId { get; set; }

        public string Name { get; set; }
        public virtual ICollection<Boardgame> Boardgames { get; set; }
        public Artist()
        {
            Boardgames = new List<Boardgame>();

        }
    }
}
