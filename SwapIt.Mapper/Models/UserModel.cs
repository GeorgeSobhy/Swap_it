
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SwapIt.Mapper.Models
{


    public class UserModel
    {


        public int Id { get; set; }
        public int RoleId { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        [MaxLength(100)]
        public required string Name { get; set; }
        [MaxLength(100)]
        public required string Email { get; set; }

        [MaxLength(100)]
        public string? Comment { get; set; }

        public required string Password { get; set; }

        public string? Address { get; set; }
        public string? ImageUrl { get; set; }

        public bool Active { get; set; }
        public string? RoleName { get; set; }

        public DateTime CreationDate { get; set; }
    }
}