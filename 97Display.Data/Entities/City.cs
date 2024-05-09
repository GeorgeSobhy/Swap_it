using Repository.Pattern.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwapIt.Data.Entities
{
    public partial class City : Entity
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
        public required string Name { get; set; }


    }
}
