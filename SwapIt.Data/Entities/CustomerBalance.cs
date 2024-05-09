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
        [ForeignKey("User")]
        public required int UserId { get; set; }
        public virtual User User { get; set; }
        public required DateTime CreationDate { get; set; }

    }
}
