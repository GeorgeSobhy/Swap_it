
using Repository.Pattern.EF;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;


namespace SwapIt.Data.Entities
{


    public partial class User : Entity
    {

        public object EntityKey
        {
            get
            {
                return Id;
            }
        }

        [Key]
        public int Id { get; set; }


        [ForeignKey("Role")]
        public required int RoleId { get; set; }
        [ForeignKey("City")]
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        [MaxLength(100)]
        public required string  Name { get; set; }
        [MaxLength(100)]
        public required string  Email { get; set; }

        [MaxLength(100)]
        public string? Comment { get; set; }

        public required string Password { get; set; }

        public string? Address { get; set; }
        public string? ImageUrl { get; set; }

        public bool Active { get; set; } 

        public virtual required Role Role { get; set; }

       // public virtual City? City { get; set; }
        public DateTime CreationDate { get; set; }
         
    }
}