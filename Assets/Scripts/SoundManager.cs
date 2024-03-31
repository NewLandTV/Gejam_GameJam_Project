using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    // Sounds
    public Sound[] sfxs;

    // AudioSource
    public AudioSource[] sfxPlayer;

    // Singleton
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Sfx play
    public void PlaySFX(string p_soundName)
    {
        for (int i = 0; i < sfxs.Length; i++)
        {
            if (sfxs[i].name.Equals(p_soundName))
            {
                for (int j = 0; j < sfxPlayer.Length; j++)
                {
                    if (!sfxPlayer[j].isPlaying)
                    {
                        sfxPlayer[j].clip = sfxs[i].clip;
                        sfxPlayer[j].Play();
                        return;
                    }
                }
            }
        }
    }

    // Sfx pause
    public void PauseSFX(string p_soundName)
    {
        for (int i = 0; i < sfxs.Length; i++)
        {
            if (sfxs[i].name.Equals(p_soundName))
            {
                for (int j = 0; j < sfxPlayer.Length; j++)
                {
                    if (sfxPlayer[j].clip == sfxs[i].clip)
                    {
                        sfxPlayer[j].Pause();
                    }
                }
            }
        }
    }
}
