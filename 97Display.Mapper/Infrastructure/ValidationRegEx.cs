
namespace SwapIt.Mapper.Models
{
    public sealed class ValidationRegEx
    {
        public const string Password = @"^.*(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).*$";
        public const string Name = @"^[a-zA-Z0-9''-'\s]{5,50}$";
        public const string Phone = @"^[01]?[- .]?(\([0-9]\d{2}\)|[0-9]\d{2})[- .]?\d{3}[- .]?\d{4}$";
        public const string Exclusion = @"^[^<^>^,^.^!^@^#^$^%^^^&^*^(^)^_^+^-^=^{^}^\[^\]^:^;^'^?^/^""]*$";
        public const string EMail = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";
        public const string Website = @"^((http:\/\/www\.)|(www\.)|(http:\/\/))[a-zA-Z0-9._-]+\.[a-zA-Z.]{2,5}$";
        public const string Url = @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$";
        public const string DecimalVal = @"^[0-9]+(\.[0-9]{1,3})?$";
        public const string IntegerVal = @"^\d+$";
    }
}