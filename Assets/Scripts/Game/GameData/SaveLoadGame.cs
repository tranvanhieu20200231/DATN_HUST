using UnityEngine;

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

        PlayerPrefsUtility.SaveInt("MaxHealCount", PlayerInteractiveRecovery.maxHealCount);
        PlayerPrefsUtility.SaveFloat("AmountRecovered", PlayerInteractiveRecovery.amountRecovered);

        PlayerPrefsUtility.SaveInt("CurrentCoin", CoinUI.currentCoin);

        PlayerPrefsUtility.SaveInt("ShopWeapon_1", ShopManager.indexWeapon_1);
        PlayerPrefsUtility.SaveInt("ShopWeapon_2", ShopManager.indexWeapon_2);
        PlayerPrefsUtility.SaveInt("ShopWeapon_3", ShopManager.indexWeapon_3);
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

        PlayerInteractiveRecovery.maxHealCount = PlayerPrefsUtility.LoadInt("MaxHealCount", 1);
        PlayerInteractiveRecovery.amountRecovered = PlayerPrefsUtility.LoadFloat("AmountRecovered", 50f);

        CoinUI.currentCoin = PlayerPrefsUtility.LoadInt("CurrentCoin", 0);

        ShopManager.indexWeapon_1 = PlayerPrefsUtility.LoadInt("ShopWeapon_1", 0);
        ShopManager.indexWeapon_2 = PlayerPrefsUtility.LoadInt("ShopWeapon_2", 1);
        ShopManager.indexWeapon_3 = PlayerPrefsUtility.LoadInt("ShopWeapon_3", 2);
    }

    public static void DeleteData()
    {
        PlayerPrefsUtility.DeleteKey("Attack");
        PlayerPrefsUtility.DeleteKey("Health");

        PlayerPrefsUtility.DeleteKey("Primary");
        PlayerPrefsUtility.DeleteKey("Secondary");

        PlayerPrefsUtility.DeleteKey("CurrentLevelIndex");

        PlayerPrefsUtility.DeleteKey("PowerRed");
        PlayerPrefsUtility.DeleteKey("PowerGreen");
        PlayerPrefsUtility.DeleteKey("PowerYellow");

        PlayerPrefsUtility.DeleteKey("MaxHealCount");
        PlayerPrefsUtility.DeleteKey("CurrentHealCount");
        PlayerPrefsUtility.DeleteKey("AmountRecovered");

        PlayerPrefsUtility.DeleteKey("TelepointStart");

        PlayerPrefsUtility.DeleteKey("CurrentCoin");

        PlayerPrefsUtility.DeleteKey("ShopWeapon_1");
        PlayerPrefsUtility.DeleteKey("ShopWeapon_2");
        PlayerPrefsUtility.DeleteKey("ShopWeapon_3");

        PlayerPrefs.Save();
    }

    public static void DeleteDataDie()
    {
        PlayerPrefsUtility.DeleteKey("Attack");
        PlayerPrefsUtility.DeleteKey("Health");

        PlayerPrefsUtility.DeleteKey("Primary");
        PlayerPrefsUtility.DeleteKey("Secondary");

        PlayerPrefsUtility.DeleteKey("CurrentLevelIndex");

        PlayerPrefsUtility.DeleteKey("PowerRed");
        PlayerPrefsUtility.DeleteKey("PowerGreen");
        PlayerPrefsUtility.DeleteKey("PowerYellow");

        PlayerPrefsUtility.DeleteKey("MaxHealCount");
        PlayerPrefsUtility.DeleteKey("CurrentHealCount");
        PlayerPrefsUtility.DeleteKey("AmountRecovered");

        PlayerPrefsUtility.DeleteKey("CurrentCoin");

        PlayerPrefsUtility.DeleteKey("ShopWeapon_1");
        PlayerPrefsUtility.DeleteKey("ShopWeapon_2");
        PlayerPrefsUtility.DeleteKey("ShopWeapon_3");

        PlayerPrefs.Save();
    }
}
