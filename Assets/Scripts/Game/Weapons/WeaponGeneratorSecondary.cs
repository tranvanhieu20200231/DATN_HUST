public class WeaponGeneratorSecondary : WeaponGenerator
{
    public static bool isSecondaryWeapon;

    private void Update()
    {
        isSecondaryWeapon = currentSecondaryWeaponName != "Null";
    }
}
