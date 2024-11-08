public class WeaponGeneratorPrimary : WeaponGenerator
{
    public static bool isPrimaryWeapon;

    private void Update()
    {
        isPrimaryWeapon = currentPrimaryWeaponName != "Null";
    }
}
