
using Repository.Pattern.EF;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;


namespace SwapIt.Data.Entities
{


    public partial class Service : Entity
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

        public int ServiceTypeId { get; set; }

        public string? Description { get; set; }
        public int ProviderId { get; set; }
        public double Price { get; set; }
        public double ExecutionTime { get; set; } 

        public bool Active { get; set; }

        public DateTime CreationDate { get; set; }
    }
}