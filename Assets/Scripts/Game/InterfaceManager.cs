using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class InterfaceManager : MonoBehaviour
{
    public GameObject GameManager;
    public GameObject GameplayObjects;
    public GameObject InformationBar;
    public GameObject PausePanel;
    public GameObject SettingsPanel;
    public GameObject GameOverPanel;

    public TextMeshProUGUI gameOverScoreText;
    public AudioClip selectSound;
    private static InterfaceManager _instance;
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    public void OpenPause()
    {
        SoundManager.Instance.PlayEffect(selectSound);

        GameManager.GetComponent<GameManager>().OnPause();

        GameplayObjects.SetActive(false);
        InformationBar.SetActive(false);
        GameOverPanel.SetActive(false);
        SettingsPanel.SetActive(false);

        PausePanel.SetActive(true);
    }

    public void ContinueGame()
    {
        SoundManager.Instance.PlayEffect(selectSound);

        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);
        SettingsPanel.SetActive(false);

        GameplayObjects.SetActive(true);
        InformationBar.SetActive(true);

        GameManager.GetComponent<GameManager>().OnContinue();
    }

    public void RestartGame()
    {
        SoundManager.Instance.PlayEffect(selectSound);

        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);
        SettingsPanel.SetActive(false);

        GameplayObjects.SetActive(true);
        InformationBar.SetActive(true);

        GameManager.GetComponent<GameManager>().OnRestart();
    }

    public void GoHome()
    {
        SoundManager.Instance.PlayEffect(selectSound);
        // Scene 0: Main Menu
        SceneManager.LoadSceneAsync(0);
    }

    public void OpenGameOver()
    {
        GameplayObjects.SetActive(false);
        InformationBar.SetActive(false);
        PausePanel.SetActive(false);
        SettingsPanel.SetActive(false);

        gameOverScoreText.text = "You got " + GameManager.GetComponent<GameManager>().scoreText.text + " maqui points";

        GameOverPanel.SetActive(true);
    }

    public void OpenSettings()
    {
        SoundManager.Instance.PlayEffect(selectSound);

        GameManager.GetComponent<GameManager>().OnPause();

        GameplayObjects.SetActive(false);
        InformationBar.SetActive(false);
        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);

        SettingsPanel.SetActive(true);
    }

    public void OnEffectsSliderValueChanged(float value)
    {
        SoundManager.Instance.ChangeEffectsVolume(value);
        SoundManager.Instance.PlayEffect(selectSound);
    }

    public void OnMusicSliderValueChanged(float value)
    {
        SoundManager.Instance.ChangeMusicVolume(value);
    }

}
