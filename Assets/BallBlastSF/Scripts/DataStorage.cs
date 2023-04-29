
using UnityEditor;

public static class DataStorage 
{
    public static float FireRate { get; private set; }
    public static int Damage { get; private set; }
    public static int ProjectileAmount { get; private set; }
    public static float ProjectileDistance { get; private set; }
    public static int LevelNumber { get; private set; } = 1;
    public static bool SettingsDataInitialized { get; private set; }
    
    public static void FillDataFromSettings(GamePlaySettings settings)
    {
        FireRate = settings.FireRate;
        Damage = settings.Damage;
        ProjectileAmount = settings.ProjectileAmount;
        ProjectileDistance = settings.ProjectileDistance;
        SettingsDataInitialized = true;
    }

    public static void FillDataFromGameMgr(GameMgr gameMgr)
    {
        LevelNumber = gameMgr.LevelNumber;
    }
}