using System.Diagnostics;

public class ItemCollector : IItemTaker
{
    private readonly PlayerHealth _playerStats;

    public ItemCollector(PlayerHealth playerStats)
    {
        _playerStats = playerStats;
    }

    public void Take(Coin coin)
    {
        coin.Remove();
    }

    public void Take(FirstAid firstAid)
    {
        _playerStats.RestoreHealth(firstAid.HealAmount);
        firstAid.Remove();
    }
}
