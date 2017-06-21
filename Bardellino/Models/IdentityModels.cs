using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel;

namespace Bardellino.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class Servizi
    {
        [Key]
        public int Servizo_Id { get; set; }
        [Display(Name = "Servizio")]
        public string Servizio { get; set; }
        [Display(Name ="Descrizione")]
        public string Descrizione { set; get; }
        [DefaultValue("True")]
        public bool Pubblica { get; set; }
    }
    
    public class Promo
    {
        [Key]
        public int Promo_Id { get; set; }
        [Display(Name = "Inizio promo")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataI { get; set; }
        [Display(Name = "Fine promo")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataF { get; set; }
        [Display(Name ="Nome promo")]
        public string Nome { get; set; }
        [Display(Name = "Descrizione")]
        public string Descrizione { set; get; }
        [Display(Name ="Attiva")]
        public bool Attiva { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Bardellino.Models.Servizi> Servizis { get; set; }
        public DbSet<Bardellino.Models.Promo> Promos { get; set; }
        public DbSet<Bardellino.Models.Slide> Slides { get; set; }
    }
}