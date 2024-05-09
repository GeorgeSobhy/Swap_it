
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SwapIt.Mapper.Models
{


    public class ServiceBookmarkModel
    {


        public int Id { get; set; }
        public required int ServiceId { get; set; }
        public ServiceModel? Service { get; set; }
        public required int CustomerID { get; set; }
        public UserModel? Customer { get; set; }
        public DateTime CreationDate { get; set; }
    }
}