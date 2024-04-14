using System.ComponentModel.DataAnnotations;

namespace WebAPI_PropertySearchingSite.Models
{
    public class FurnishingType : BaseEntity
    {       
        [Required]
        public string Name { get; set; }
    }
}