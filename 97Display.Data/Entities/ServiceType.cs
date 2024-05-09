
using Repository.Pattern.EF;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;


namespace SwapIt.Data.Entities
{


    public partial class ServiceType : Entity
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
        [MaxLength(100)]
        public required string  Name { get; set; } 
         
    }
}