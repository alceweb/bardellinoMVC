using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Bardellino.Models
{
    public class InfoViewModels
    {
        [Display(Name ="Nome")]
        [Required]
        public string Nome { get; set; }
        [Display(Name ="Cognome")]
        [Required]
        public string Cognome { get; set; }
        [Required]
        [Display(Name = "Posta elettronica")]
        public string Email { get; set; }
        [Display(Name ="Tel./Cell.")]
        public string Tel { get; set; }
        [Display(Name ="Messaggio")]
        public string Messaggio { get; set; }
    }
}