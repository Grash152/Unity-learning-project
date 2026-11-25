using UnityEngine;

public static class HPSystem
{
    private static int _maxHp = 50;
    private static int _currentHp = _maxHp;

    public static void SetCurrentHp(int hp)
    {
        _currentHp = hp;
    }

    public static int GetCurrentHp()
    {
        return _currentHp;
    }
    public static int GetMaxHp()
    {
        return _maxHp;
    }
}