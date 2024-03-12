﻿using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Domain.Entities;
public sealed class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => string.Join(" ", FirstName, LastName);
    public int? EmailConfirmCode { get; set; }
}
