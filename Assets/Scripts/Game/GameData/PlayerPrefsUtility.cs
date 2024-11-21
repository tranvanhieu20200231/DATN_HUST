﻿using UnityEngine;

public static class PlayerPrefsUtility
{
    public static void SaveInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    public static int LoadInt(string key, int defaultValue = 0)
    {
        return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) : defaultValue;
    }

    public static void SaveFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    public static float LoadFloat(string key, float defaultValue = 0f)
    {
        return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetFloat(key) : defaultValue;
    }

    public static void SaveString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public static string LoadString(string key, string defaultValue = "")
    {
        return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetString(key) : defaultValue;
    }

    public static void DeleteKey(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.DeleteKey(key);
        }
    }

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
