
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SwapIt.Mapper.Models
{


    public class ErrorLogModel
    {


        [ScaffoldColumn(false)]
        public long Id { get; set; }

        [MaxLength(100)]
        [DisplayName("Method Name")]
        public string MethodName { get; set; }
        [MaxLength(100)]
        [DisplayName("Class Name")]
        public string ClassName { get; set; }

        [DisplayName("Error Msg")]
        [DisplayFormat(NullDisplayText = "[...]")]
        public string ErrorMsg { get; set; }

        [DisplayName("Error Code")]
        public string ErrorCode { get; set; }

        [DisplayName("Stack Trace")]
        [DisplayFormat(NullDisplayText = "[...]")]
        public string StackTrace { get; set; }


        [Required()]
        [DisplayName("Date Added")]
        public DateTime DateAdded { get; set; }

        [DisplayName("Error Count")]
        public int ErrorCount { get; set; }
    }
}