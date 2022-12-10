using Common.Domain;
using System.ComponentModel.DataAnnotations;

namespace UserModule.Data.Entities.Users;

class User : BaseEntity
{
    [MaxLength(50)]
    public string? Name { get; set; }

    [MaxLength(50)]
    public string? Family { get; set; }

    [Required]
    [MaxLength(11)]
    public string PhoneNumber { get; set; }

    [MaxLength(50)]
    public string? Email { get; set; }

    [Required]
    [MaxLength(80)]
    public string Password { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Avatar { get; set; }

    public List<UserRoles> UserRoles { get; set; }
}
