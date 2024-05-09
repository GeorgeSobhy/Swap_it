
using Repository.Pattern.EF;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey("ServiceType")]
        public int ServiceTypeId { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        [MaxLength(2000)]
        public string? Description { get; set; }
        [ForeignKey("User")]
        public int ProviderId { get; set; }
        public virtual User Provider { get; set; }
        public double Price { get; set; }
        public double ExecutionTime { get; set; } 

        public bool Active { get; set; } = false;

        public DateTime CreationDate { get; set; }
    }
}