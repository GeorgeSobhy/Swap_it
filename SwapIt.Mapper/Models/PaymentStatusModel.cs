
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SwapIt.Mapper.Models
{


    public class PaymentStatusModel
    {


        public int Id { get; set; }
        [MaxLength(100)]
        public required string Name { get; set; }
    }
}