using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> soundObjects;
    private Dictionary<string, AudioSource> audioSourceDictionary;

    private void Awake()
    {
        audioSourceDictionary = new Dictionary<string, AudioSource>();

        foreach (var obj in soundObjects)
        {
            if (obj != null)
            {
                AudioSource audioSource = obj.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSourceDictionary[obj.name] = audioSource;
                }
            }
        }
    }

    public void PlaySound(string objectName)
    {
        if (audioSourceDictionary.TryGetValue(objectName, out AudioSource audioSource))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}