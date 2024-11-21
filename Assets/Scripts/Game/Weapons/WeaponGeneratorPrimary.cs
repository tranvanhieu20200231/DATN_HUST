public class WeaponGeneratorPrimary : WeaponGenerator
{
    public static bool isPrimaryWeapon;

    private void Start()
    {
        GenerateWeaponByName(currentPrimaryWeaponName, true, true);
    }

    private void Update()
    {
        isPrimaryWeapon = currentPrimaryWeaponName != "Null";
    }
}
