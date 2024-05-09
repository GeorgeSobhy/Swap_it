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
    public partial class ServiceBookmark : Entity
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

        [ForeignKey("Service")]
        public required int ServiceId { get; set; }
        public virtual Service Service { get; set; }

        [ForeignKey("User")]
        public required int CustomerID { get; set; }
        public virtual User Customer { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
