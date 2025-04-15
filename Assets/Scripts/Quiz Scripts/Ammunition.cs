[System.Serializable]

public class Ammunition
{
    public int currAmmo;
    public int maxAmmo;
    public System.Action<int> onChanged;

    public void Reduce(int value)
    {
        currAmmo -= value;
        if (currAmmo < 0) currAmmo = 0;
        onChanged?.Invoke(currAmmo);
    }
    public void Gain(int value)
    {
        currAmmo += value;
        if (currAmmo > maxAmmo) currAmmo = maxAmmo;
        onChanged?.Invoke(currAmmo);
    }
}