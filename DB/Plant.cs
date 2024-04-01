using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8.DB
{
    public class Plant
    {
        [Key]
        public int Id { get; set; }
        public string PlantKind { get; set; }
        public string UmoviZrost { get; set; }
        public string PeriodCvit { get; set; }
        public string WaterNeeds { get; set; }
        public string DobrivaNeeds { get; set; }
    }
}
