using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider effectsSlider;
    public Slider musicSlider;

    void Start()
    {
        effectsSlider.value = SoundManager.Instance.soundSettings.effectsVolume;
        musicSlider.value = SoundManager.Instance.soundSettings.musicVolume;
    }
    public void OnEffectsSliderValueChanged(float value)
    {
        SoundManager.Instance.ChangeEffectsVolume(value);
        SoundManager.Instance.PlaySelectEffect();
    }

    public void OnMusicSliderValueChanged(float value)
    {
        SoundManager.Instance.ChangeMusicVolume(value);
    }

    public void GoHome()
    {
        SoundManager.Instance.PlaySelectEffect();
        SoundManager.Instance.SaveSoundSettings();
        // Scene 0: Main Menu
        SceneManager.LoadSceneAsync(0);
    }
}
