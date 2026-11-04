using UnityEngine;
using System.Collections.Generic;

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
    XP,
    Boss,
    Explode,
    BossAbility

}
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
    public SoundEntry[] soundEntries; 

    private Dictionary<SoundType, AudioClip> soundDictionary;

    void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); return; }

        soundDictionary = new Dictionary<SoundType, AudioClip>();
        foreach (SoundEntry entry in soundEntries)
        {
            if (entry.clip != null && !soundDictionary.ContainsKey(entry.type))
            {
                soundDictionary.Add(entry.type, entry.clip);
            }
        }
    }


    public void PlaySFX(SoundType type)
    {
        Debug.Log("method called");

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