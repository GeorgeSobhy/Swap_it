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
    public partial class Notification : Entity
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
        public string? Content { get; set; }

        [ForeignKey("ServiceRequest")]
        public int? ServiceRequestId { get; set; }
        public virtual ServiceRequest? ServiceRequest { get; set; }

        [ForeignKey("User")]
        public required int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
