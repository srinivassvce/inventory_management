using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserApi.models
{
    [Table("Users")]
    public class TokenModel
    {
        public string Fullname { get; set; } = default!;

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;

        public string Token { get; set; } = default!;


    }
}
