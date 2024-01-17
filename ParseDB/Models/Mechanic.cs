using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseDB.Models
{
    class Mechanic
    {
        [Key]
        public int MechanicId { get; set; }

        public string Name { get; set; }
        public virtual ICollection<Boardgame> Boardgames { get; set; }
        public Mechanic()
        {
            Boardgames = new List<Boardgame>();

        }
    }
}
