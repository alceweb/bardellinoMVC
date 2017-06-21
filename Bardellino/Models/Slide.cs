using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Bardellino.Models
{
    public class Slide
    {
        [Key]
        public int Slide_Id { get; set; }
        public int Posizione { get; set; }
        public string Testo { get; set; }
        public string Immagine { get; set; }
    }
}