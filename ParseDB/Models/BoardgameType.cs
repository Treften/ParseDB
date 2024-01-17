using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseDB.Models
{
    class BoardgameType
    {
        [Key]
        public int TypeId { get; set; }

        public string Name { get; set; }
        public virtual ICollection<Boardgame> Boardgames { get; set; }
        public BoardgameType()
        {
            Boardgames = new List<Boardgame>();

        }
    }
}
