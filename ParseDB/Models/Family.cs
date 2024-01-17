using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseDB.Models
{
    class Family
    {
        [Key]
        public int FamilyId { get; set; }

        public string Name { get; set; }
        public virtual ICollection<Boardgame> Boardgames { get; set; }
        public Family()
        {
            Boardgames = new List<Boardgame>();

        }
    }
}
