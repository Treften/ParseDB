using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseDB.Models
{
    class Publisher
    {
        [Key]
        public int PublisherId { get; set; }

        public string Name { get; set; }
        public virtual ICollection<Boardgame> Boardgames { get; set; }
        public Publisher()
        {
            Boardgames = new List<Boardgame>();

        }
    }
}
