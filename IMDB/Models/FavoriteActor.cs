using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IMDB.Models
{
    public class FavoriteActor
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name = "User")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [Key]
        [Column(Order = 1)]
        [Display(Name = "Actor")]
        public int ActorId { get; set; }

        [ForeignKey("ActorId")]
        public virtual Actor Actor { get; set; }
    }
}