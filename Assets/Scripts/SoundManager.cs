using System;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SoundSettings
{
    public float effectsVolume = 1f;
    public float musicVolume = 1f;
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("SoundManager");
                _instance = go.AddComponent<SoundManager>();
                DontDestroyOnLoad(go);

                _instance.Initialize();
            }

            return _instance;
        }
    }

    public SoundSettings soundSettings = new SoundSettings();
    private string settingsPath;
    private AudioSource effectsSource;
    private AudioSource musicSource;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        Initialize();
    }

    private void Initialize()
    {
        settingsPath = Path.Combine(Application.persistentDataPath, "soundSettings.json");

        effectsSource = gameObject.AddComponent<AudioSource>();
        effectsSource.playOnAwake = false;

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.playOnAwake = false;

        LoadSoundSettings();
    }

    public void LoadSoundSettings()
    {
        // Try to load the last user settings on its json file
        try
        {
            if (File.Exists(settingsPath))
            {
                string json = File.ReadAllText(settingsPath);
                soundSettings = JsonUtility.FromJson<SoundSettings>(json);

                effectsSource.volume = soundSettings.effectsVolume;
                musicSource.volume = soundSettings.musicVolume;
                Debug.Log("Sound settings loaded from file");
            }
            else
            {
                // If theres no json file, create a new one
                Debug.Log("No sound settings file found, creating new settings");
                soundSettings = new SoundSettings();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error loading user settings: {ex.Message}");
            soundSettings = new SoundSettings();
        }
    }

    public void SaveSoundSettings()
    {
        // Try to write the last sound settings on its json file
        try
        {
            string json = JsonUtility.ToJson(soundSettings, true);
            File.WriteAllText(settingsPath, json);
            Debug.Log("Sound settings saved to " + settingsPath);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error saving sound settings: {ex.Message}");
        }
    }

    public void PlayEffect(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }

    public void ChangeEffectsVolume(float volume)
    {
        soundSettings.effectsVolume = volume;
        effectsSource.volume = volume;
        SaveSoundSettings();
    }

    public void ChangeMusicVolume(float volume)
    {
        soundSettings.musicVolume = volume;
        musicSource.volume = volume;
        SaveSoundSettings();
    }

}
