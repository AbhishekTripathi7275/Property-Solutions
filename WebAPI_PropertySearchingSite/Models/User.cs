using System.ComponentModel.DataAnnotations;

namespace WebAPI_PropertySearchingSite.Models
{
    public class User : BaseEntity
    {      
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
