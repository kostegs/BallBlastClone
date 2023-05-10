public class StonesFreezer : BonusObject
{
    public override void ApplyBonus(BonusManager bonusManager) => bonusManager.ApplyStoneFreeze();
}
