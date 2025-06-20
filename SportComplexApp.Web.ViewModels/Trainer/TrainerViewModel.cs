using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Web.ViewModels.Trainer
{
    public class TrainerViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public string? Bio { get; set; }
        public string? ImageUrl { get; set; }
    }
}
