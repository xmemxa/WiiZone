using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using WiiZoneNowy.Data;

namespace WiiZoneNowy.Validators;

public sealed class UniqueTagNameAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext ctx)
    {
        if (value is not string name || string.IsNullOrWhiteSpace(name))
            return ValidationResult.Success;

        var provider = ctx.GetRequiredService<IDbContextProvider>();
        var db = provider.GetDbContext();
        
        var exists = db.Tags
            .AsNoTracking()
            .Any(t => t.Name.ToLower() == name.Trim().ToLower());

        return exists
            ? new ValidationResult("This tag name already exists.")
            : ValidationResult.Success;
    }
}