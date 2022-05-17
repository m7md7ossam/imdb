using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IMDB.Models
{
    public class MovieDirector
    {
        [Key]
        [Column(Order = 0)]
        public int DirectorId { get; set; }

        [ForeignKey("DirectorId")]
        public virtual Director Director { get; set; }

        [Key]
        [Column(Order = 1)]
        public int MovieId { get; set; }
        
        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
    }
}