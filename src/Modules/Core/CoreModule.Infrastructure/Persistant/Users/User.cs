using Common.Domain;
using System.ComponentModel.DataAnnotations;

namespace CoreModule.Infrastructure.Persistant.Users;

class User:BaseEntity
{
    private User()
    {

    }
    [MaxLength(12)]
    public string PhoneNumber { get; set; }
    
    [MaxLength(50)]
    public string Name { get; set; }
    
    [MaxLength(50)]
    public string Family { get; set; }
    
    [MaxLength(110)]
    public string Email { get; set; }
    
    [MaxLength(110)]
    public string Avatar { get; set; }
}
