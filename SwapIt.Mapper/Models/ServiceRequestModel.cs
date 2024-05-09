
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SwapIt.Mapper.Models
{


    public class ServiceRequestModel
    {


        public int Id { get; set; }
        [MaxLength(1000)]
        public string? Notes { get; set; }
        [MaxLength(2000)]
        public string? Feedback { get; set; }
        public float? Rate { get; set; }
        public DateTime CreationDate { get; set; } 
        public required int ServiceStatusI { get; set; }

        public required int ServiceId { get; set; }

        public required int CustomerID { get; set; }
        
        public int? PaymentStatusId { get; set; }
        public  PaymentStatusModel? PaymentStatus { get; set; }
        public virtual ServiceStatusModel? Status { get; set; }
        public virtual ServiceModel? Service { get; set; }
        public virtual UserModel? Customer { get; set; }
    }
}