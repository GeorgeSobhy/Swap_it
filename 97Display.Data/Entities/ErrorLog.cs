
using Repository.Pattern.EF;
using System.ComponentModel.DataAnnotations;


namespace SwapIt.Data.Entities
{


    public partial class ErrorLog : Entity
    {

        public object EntityKey
        {
            get
            {
                return Id;
            }
        }

        [Key]
        public long Id { get; set; }

        [MaxLength(100)]
        public string? MethodName { get; set; }
        [MaxLength(100)]
        public string? ClassName { get; set; }

        public string? ErrorMsg { get; set; }


        public string? ErrorCode { get; set; }


        public string? StackTrace { get; set; }


        public DateTime DateAdded { get; set; }
    }
}