using WiiZoneNowy.Models;

namespace WiiZoneNowy.Validators;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using WiiZoneNowy.Data;
using Microsoft.Extensions.DependencyInjection;

public sealed class UniquePhoneAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext ctx)
    {
        if (value is not string phone || string.IsNullOrWhiteSpace(phone)) return ValidationResult.Success;

        phone = phone.Trim();

        var provider = ctx.GetRequiredService<IDbContextProvider>();
        var db = provider.GetDbContext();

        var currentClient = ctx.ObjectInstance as Client;
        if (currentClient == null) return ValidationResult.Success;

        bool exists = db.Clients.AsNoTracking()
            .Any(c => c.Phone == phone
            && c.ClientId != currentClient.ClientId);

        return exists
            ? new ValidationResult("That phone number is already registered.")
            : ValidationResult.Success;
    }
}
