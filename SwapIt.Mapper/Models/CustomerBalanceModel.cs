 
using System.ComponentModel.DataAnnotations; 

namespace SwapIt.Mapper.Models
{


    public class CustomerBalanceModel
    {
         
        public int Id { get; set; }

        [Range(0, int.MaxValue)]
        public required int Points { get; set; } = 0; 
        public required int CustomerId { get; set; }
        public virtual UserModel? Customer { get; set; }
        public required DateTime CreationDate { get; set; }
    }
}