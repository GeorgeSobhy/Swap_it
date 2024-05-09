using Castle.Core.Resource;
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
    public partial class CustomerBalance : Entity
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

        [Range(0,int.MaxValue)]
        public required int Points { get; set; } = 0;
        [ForeignKey("Customer")]
        public required int CustomerId { get; set; }
        public virtual User Customer { get; set; }
        public required DateTime CreationDate { get; set; }

    }
}
