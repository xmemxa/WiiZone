using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using WiiZoneNowy.Data;
using WiiZoneNowy.Models;

namespace WiiZoneNowy.Validators;

public sealed class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext ctx)
    {
        if (value is not string email) return ValidationResult.Success;

        email = email.Trim().ToLowerInvariant();

        var provider = ctx.GetRequiredService<IDbContextProvider>();
        var db = provider.GetDbContext();
        
        var currentClient = ctx.ObjectInstance as Client;
        if (currentClient == null) return ValidationResult.Success;

        bool exists = db.Clients.AsNoTracking()
            .Any(c => c.Email.ToLower() == email
            && c.ClientId != currentClient.ClientId);

        return exists
            ? new ValidationResult("That e-mail address is already registered.")
            : ValidationResult.Success;
    }
}
