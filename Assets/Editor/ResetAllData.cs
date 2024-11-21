using UnityEditor;
using UnityEngine;

public class ResetAllData : MonoBehaviour
{
    [MenuItem("Tools/Reset All Data")]

    static void SliceSprites()
    {
        PlayerPrefsUtility.DeleteAll();
    }
}
