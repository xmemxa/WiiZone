namespace WiiZoneNowy.Data;

public interface IDbContextProvider
{
    AppDbContext GetDbContext();
    void SetConnection();
}