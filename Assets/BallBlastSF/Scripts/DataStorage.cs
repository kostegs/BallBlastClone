
using System.Collections.Generic;
using UnityEditor;

public static class DataStorage 
{
    public static float FireRate { get; private set; }
    public static int Damage { get; private set; }
    public static int ProjectileAmount { get; private set; }
    public static float ProjectileDistance { get; private set; }
    public static int LevelNumber { get; private set; } = 1;
    public static bool SettingsDataInitialized { get; private set; }
    public static int CountOfCoins { get; private set; }
    public static int RaiseDamagePrice { get; private set; }
    public static int RaiseAmountPointer { get; private set; }
    public static int RaiseSpeedPointer { get; private set; }

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

    public static void FillDataFromCoinsManager(CoinsManager coinsManager)
    {
        CountOfCoins = coinsManager.CountOfCoins;
    }

    public static void FillDataFromCharacteristicsImprover(CharacteristicsImprover improver)
    {
        RaiseDamagePrice = improver.RaiseDamagePrice;
        RaiseAmountPointer = improver.RaiseAmountPointer;
        RaiseSpeedPointer = improver.RaiseSpeedPointer;
    }

    public static void LoadDataFromSave(SaveLoadSystemData data)
    {
        if (data == null)
            return;       

        FireRate = data.FireRate;
        Damage = data.Damage;
        ProjectileAmount = data.ProjectileAmount;
        ProjectileDistance = data.ProjectileDistance;
        LevelNumber = data.LevelNumber;        
        CountOfCoins = data.CountOfCoins;
        RaiseDamagePrice = data.RaiseDamagePrice;
        RaiseAmountPointer = data.RaiseAmountPointer;
        RaiseSpeedPointer = data.RaiseSpeedPointer;

        SettingsDataInitialized = true;
    }
}
