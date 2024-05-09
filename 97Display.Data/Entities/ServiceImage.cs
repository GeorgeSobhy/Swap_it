using Microsoft.EntityFrameworkCore;
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
    public partial class ServiceImage : Entity
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
        [ForeignKey("Service")]
        public required int ServiceId { get; set; }
        public virtual Service Service { get; set; }
        public DateTime CreationDate { get; set; }
        public required string ImageUrl { get; set; }

    }
}
