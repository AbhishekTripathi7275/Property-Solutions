using System.ComponentModel.DataAnnotations;

namespace WebAPI_PropertySearchingSite.Models
{
    public class PropertyType : BaseEntity
    {    
        [Required]
        public string Name { get; set; }
    }
}