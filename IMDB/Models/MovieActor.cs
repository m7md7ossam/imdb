using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IMDB.Models
{
    public class MovieActor
    {
        [Key]
        [Column(Order = 0)]
        public int ActorId { get; set; }

        [ForeignKey("ActorId")]
        public virtual Actor Actor { get; set; }


        [Key]
        [Column(Order = 1)]
        public int MovieId { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
    }
}