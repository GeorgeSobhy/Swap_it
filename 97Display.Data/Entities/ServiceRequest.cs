using Repository.Pattern.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwapIt.Data.Entities
{
    public partial class ServiceRequest : Entity
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
        [MaxLength(1000)]
        public string? Notes { get; set; }
        [MaxLength(2000)]
        public string? Feedback { get; set; }
        public float? Rate { get; set; }
        public DateTime CreationDate { get; set; }

        [ForeignKey("ServiceStatus")]
        public required int ServiceStatusI {  get; set; }
        public virtual ServiceStatus Status { get; set; }

        [ForeignKey("Service")]
        public required int ServiceId { get; set; }
        public virtual Service Service { get; set; }

        [ForeignKey("User")]
        public required int CustomerID { get; set; }
        public virtual User Customer { get; set; }
        [ForeignKey("PaymentStatus")]
        public int? PaymentStatusId { get; set;}
        public virtual PaymentStatus? PaymentStatus { get; set; }

    }
}
