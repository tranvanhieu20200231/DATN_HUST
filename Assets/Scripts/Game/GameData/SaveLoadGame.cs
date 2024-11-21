public static class SaveLoadGame
{
    public static void SaveData()
    {
        PlayerPrefsUtility.SaveFloat("Attack", PlayerData.attack);
        PlayerPrefsUtility.SaveFloat("Health", PlayerData.health);

        PlayerPrefsUtility.SaveString("Primary", WeaponGenerator.currentPrimaryWeaponName);
        PlayerPrefsUtility.SaveString("Secondary", WeaponGenerator.currentSecondaryWeaponName);

        PlayerPrefsUtility.SaveInt("CurrentLevelIndex", PlayerInteractiveTeleport.currentLevelIndex);

        PlayerPrefsUtility.SaveInt("PowerRed", PlayerInteractiveChest.redPowerCurrent);
        PlayerPrefsUtility.SaveInt("PowerGreen", PlayerInteractiveChest.greenPowerCurrent);
        PlayerPrefsUtility.SaveInt("PowerYellow", PlayerInteractiveChest.yellowPowerCurrent);
    }

    public static void LoadGame()
    {
        PlayerData.attack = PlayerPrefsUtility.LoadFloat("Attack", 10f);
        PlayerData.health = PlayerPrefsUtility.LoadFloat("Health", 100f);

        WeaponGenerator.currentPrimaryWeaponName = PlayerPrefsUtility.LoadString("Primary", "Null");
        WeaponGenerator.currentSecondaryWeaponName = PlayerPrefsUtility.LoadString("Secondary", "Null");

        PlayerInteractiveTeleport.currentLevelIndex = PlayerPrefsUtility.LoadInt("CurrentLevelIndex", 1);

        PlayerInteractiveChest.redPowerCurrent = PlayerPrefsUtility.LoadInt("PowerRed", 0);
        PlayerInteractiveChest.greenPowerCurrent = PlayerPrefsUtility.LoadInt("PowerGreen", 0);
        PlayerInteractiveChest.yellowPowerCurrent = PlayerPrefsUtility.LoadInt("PowerYellow", 0);
    }
}
