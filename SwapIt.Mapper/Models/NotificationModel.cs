
using SwapIt.Data.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwapIt.Mapper.Models
{


    public class NotificationModel
    {

        public int Id { get; set; }
        [MaxLength(1000)]
        public string? Content { get; set; }
        public int? ServiceRequestId { get; set; }
        public ServiceRequest? ServiceRequest { get; set; }
        public required int UserId { get; set; }
        public User? User { get; set; }
        public DateTime CreationDate { get; set; }
    }
}