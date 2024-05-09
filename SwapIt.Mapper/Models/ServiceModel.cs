 
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SwapIt.Mapper.Models
{

 
    public class ServiceModel
    {


        public int Id { get; set; }
        [MaxLength(100)]
        public required string Name { get; set; }

        public int ServiceTypeId { get; set; }

        public string? Description { get; set; }
        public int ProviderId { get; set; }
        public double Price { get; set; }
        public double ExecutionTime { get; set; }

        public bool Active { get; set; }

        public DateTime CreationDate { get; set; }
    }
}