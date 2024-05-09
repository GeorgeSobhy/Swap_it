
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SwapIt.Mapper.Models
{


    public class ServiceImageModel
    {


        public int Id { get; set; }
        [MaxLength(100)] 
        public required int ServiceId { get; set; }
        public ServiceModel? Service { get; set; }
        public DateTime CreationDate { get; set; }
        public required string ImageUrl { get; set; }
    }
}