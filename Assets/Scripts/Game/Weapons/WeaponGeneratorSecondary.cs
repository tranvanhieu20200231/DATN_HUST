public class WeaponGeneratorSecondary : WeaponGenerator
{
    public static bool isSecondaryWeapon;

    private void Start()
    {
        GenerateWeaponByName(currentSecondaryWeaponName, false, true);
    }

    private void Update()
    {
        isSecondaryWeapon = currentSecondaryWeaponName != "Null";
    }
}
