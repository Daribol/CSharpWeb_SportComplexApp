using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Data.Models
{
    public class Facility
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Sport> Sports { get; set; } = new HashSet<Sport>();
    }
}
