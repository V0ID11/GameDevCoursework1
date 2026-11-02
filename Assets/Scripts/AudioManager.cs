using UnityEngine;
using System.Collections.Generic;

// 1. Define all possible sounds in an Enum outside the class so any script can see it
public enum SoundType 
{ 
    Arrow, 
    BodyHit,
    Bounce,
    Dash,
    Heal,
    Kill, 
    LevelUp,
    Parry,
    Slow,
    SwordAttack,
    SwordHit,
    UpgradeGet,
    XP

}

// 2. Create a struct to link the Enum to an AudioClip in the Inspector
[System.Serializable]
public struct SoundEntry
{
    public SoundType type;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; 
    public AudioSource sfxSource;

    [Header("Audio Library")]
    // This is what you see and edit in the Unity Inspector
    public SoundEntry[] soundEntries; 

    // This is what the code uses for instant, highly efficient lookups
    private Dictionary<SoundType, AudioClip> soundDictionary;

    void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); return; }

        // 3. Convert the Inspector array into the Dictionary at startup
        soundDictionary = new Dictionary<SoundType, AudioClip>();
        foreach (SoundEntry entry in soundEntries)
        {
            // Only add it if there is actually an audio clip assigned
            if (entry.clip != null && !soundDictionary.ContainsKey(entry.type))
            {
                soundDictionary.Add(entry.type, entry.clip);
            }
        }
    }

    // 4. One single method to handle every sound in the game
    public void PlaySFX(SoundType type)
    {
        Debug.Log("method called");
        // TryGetValue safely checks if the sound exists to prevent NullReferenceExceptions
        if (soundDictionary.TryGetValue(type, out AudioClip clip))
        {
            Debug.Log("sound played");
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Audio clip for {type} is missing from the AudioManager!");
        }
    }
}