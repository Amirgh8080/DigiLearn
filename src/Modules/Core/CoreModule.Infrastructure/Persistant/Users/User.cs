﻿using Common.Domain;

namespace CoreModule.Infrastructure.Persistant.Users;

class User:BaseEntity
{
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    public string Family { get; set; }
    public string Email { get; set; }
    public string Avatar { get; set; }
}
