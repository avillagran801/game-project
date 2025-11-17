using System;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SoundSettings
{
    public float effectsVolume = 100;
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
        effectsSource.volume = soundSettings.effectsVolume / 100f;
    }

    public void SaveSoundSettings()
    {
        // Try to write the last user settings on its json file
        try
        {
            string json = JsonUtility.ToJson(soundSettings, true);
            File.WriteAllText(settingsPath, json);
            Debug.Log("User settings saved to " + settingsPath);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error saving user settings: {ex.Message}");
        }
    }

    public void PlayEffect(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }

}
