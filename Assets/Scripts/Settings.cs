using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
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
        // Scene 0: Main Menu
        SceneManager.LoadSceneAsync(0);
    }
}
