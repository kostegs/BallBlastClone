using System;

[Serializable]
public class SaveLoadSystemData
{
    public float FireRate { get; private set; }
    public int Damage { get; private set; }
    public int ProjectileAmount { get; private set; }
    public float ProjectileDistance { get; private set; }
    public int LevelNumber { get; private set; } = 1;
    public int CountOfCoins { get; private set; }
    public int RaiseDamagePrice { get; private set; }
    public int RaiseAmountPointer { get; private set; }
    public int RaiseSpeedPointer { get; private set; }

    public void FillDataFromDataStorage()
    {
        FireRate = DataStorage.FireRate;
        Damage = DataStorage.Damage;
        ProjectileAmount = DataStorage.ProjectileAmount;
        ProjectileDistance = DataStorage.ProjectileDistance;
        LevelNumber = DataStorage.LevelNumber;
        CountOfCoins = DataStorage.CountOfCoins;
        RaiseDamagePrice = DataStorage.RaiseDamagePrice;
        RaiseAmountPointer = DataStorage.RaiseAmountPointer;
        RaiseSpeedPointer = DataStorage.RaiseSpeedPointer;
    }
}
