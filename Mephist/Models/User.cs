using System.ComponentModel.DataAnnotations;

namespace Mephist
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string NickName { get; set; }
    }
}