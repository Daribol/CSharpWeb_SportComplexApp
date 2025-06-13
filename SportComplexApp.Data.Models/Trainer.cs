using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Data.Models
{
    public class Trainer
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Specialization { get; set; } = null!;

        public string? Bio { get; set; }

        public string? ImageUrl { get; set; }

        public virtual ICollection<TrainerSession> TrainerSessions { get; set; } = new HashSet<TrainerSession>();
        public virtual ICollection<SportTrainer> SportTrainers { get; set; } = new HashSet<SportTrainer>();
    }
}
