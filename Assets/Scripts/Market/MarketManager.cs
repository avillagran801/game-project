using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarketManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject leftIconRender;
    public GameObject centerIconRender;
    public GameObject rightIconRender;
    public TextMeshProUGUI priceText;
    public IconPack[] iconPacks;

    private int score;
    private int index = 0;

    void Awake()
    {
        score = DataManager.Instance.userData.totalScore;
        UpdateScoreText();
        SetPreviewIcon();
    }

    void SetPreviewIcon()
    {
        leftIconRender.GetComponent<PackRender>().SetDesign(iconPacks[index % iconPacks.Length].getPreviewIcon());
        centerIconRender.GetComponent<PackRender>().SetDesign(iconPacks[(index + 1) % iconPacks.Length].getPreviewIcon());
        rightIconRender.GetComponent<PackRender>().SetDesign(iconPacks[(index + 2) % iconPacks.Length].getPreviewIcon());

        priceText.text = iconPacks[(index + 1) % iconPacks.Length].price.ToString();

        Debug.Log(index);
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void leftClick()
    {
        index = (index - 1 + iconPacks.Length) % iconPacks.Length;
        SetPreviewIcon();
    }

    public void rightClick()
    {
        index = (index + 1) % iconPacks.Length;
        SetPreviewIcon();
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
