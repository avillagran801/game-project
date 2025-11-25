using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider effectsSlider;
    public Slider musicSlider;
    public Slider vibrationSlider;

    void Start()
    {
        effectsSlider.value = SoundManager.Instance.soundSettings.effectsVolume;
        musicSlider.value = SoundManager.Instance.soundSettings.musicVolume;
        vibrationSlider.value = SoundManager.Instance.soundSettings.vibrationIntensity;
    }
    public void OnEffectsSliderValueChanged(float value)
    {
        SoundManager.Instance.ChangeEffectsVolume(value);
    }

    public void OnMusicSliderValueChanged(float value)
    {
        SoundManager.Instance.ChangeMusicVolume(value);
    }

    public void OnVibrationSliderValueChanged(float value)
    {
        SoundManager.Instance.ChangeVibrationIntensity(value);
    }

    public void GoHome()
    {
        SoundManager.Instance.PlaySelectEffect();
        SoundManager.Instance.SaveSoundSettings();
        // Scene 0: Main Menu
        SceneManager.LoadSceneAsync(0);
    }
}
