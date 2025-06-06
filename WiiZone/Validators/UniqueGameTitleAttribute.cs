using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using WiiZoneNowy.Data;
using WiiZoneNowy.Models;

namespace WiiZoneNowy.Validators;

public sealed class UniqueGameTitleAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext ctx)
    {
        if (value is not string title || string.IsNullOrWhiteSpace(title))
            return ValidationResult.Success;

        var provider = ctx.GetRequiredService<IDbContextProvider>();
        var db = provider.GetDbContext();
        
        var currentGame = ctx.ObjectInstance as Game;
        if (currentGame == null) return ValidationResult.Success;
        
        bool exists = db.Games
            .AsNoTracking()
            .Any(g => g.Title.ToLower() == title.Trim().ToLower()
            && g.GameId != currentGame.GameId);

        return exists
            ? new ValidationResult("A game with this title already exists.")
            : ValidationResult.Success;
    }
}