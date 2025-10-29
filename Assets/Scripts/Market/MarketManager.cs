using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarketManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;

    void Start()
    {
        score = DataManager.Instance.userData.totalScore;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    void BuyPack()
    {

    }

    public void GoHome()
    {
        // Scene 0: Main Menu
        SceneManager.LoadSceneAsync(0);
    }
}
