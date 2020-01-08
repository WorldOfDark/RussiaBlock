using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class View : MonoBehaviour
{
    private RectTransform logoname;
    private RectTransform menuui;
    private RectTransform gameui;

    private GameObject gameOverUI;
    private GameObject restartButton;
    private GameObject rankUI;
    private GameObject setUI;

    private GameObject muteUI;

    private Text score;
    private Text highscore;
    private Text gameOverScore;

    private Text rankScore;
    private Text rankHighscore;
    private Text rankNumberGames;
    private void Awake()
    {
        logoname = transform.Find("Canvas/LogoName") as RectTransform;
        menuui = transform.Find("Canvas/MenuUI") as RectTransform;
        gameui = transform.Find("Canvas/GameUI") as RectTransform;

        restartButton = transform.Find("Canvas/MenuUI/ReStart").gameObject;
        gameOverUI = transform.Find("Canvas/GameOverUI").gameObject;
        rankUI = transform.Find("Canvas/RankUI").gameObject;
        setUI = transform.Find("Canvas/SettingUI").gameObject;

        muteUI = transform.Find("Canvas/SettingUI/audio/mute").gameObject;

        score = transform.Find("Canvas/GameUI/NowScore/score").GetComponent<Text>();
        highscore = transform.Find("Canvas/GameUI/BestScore/score").GetComponent<Text>();
        gameOverScore = transform.Find("Canvas/GameOverUI/score").GetComponent<Text>();

        rankScore = transform.Find("Canvas/RankUI/ScoreLabel/score").GetComponent<Text>();
        rankHighscore = transform.Find("Canvas/RankUI/HighScoreLabel/score").GetComponent<Text>();
        rankNumberGames = transform.Find("Canvas/RankUI/NumberGamesLabel/score").GetComponent<Text>();
    }

    public void ShowMenu()
    {
        logoname.gameObject.SetActive(true);
        logoname.DOAnchorPosY(0.5f,0.5f);
        menuui.gameObject.SetActive(true);
        menuui.DOAnchorPosY(-0.5f,0.5f);
    }

    public void HideMenu()
    {
        logoname.DOAnchorPosY(105.1f, 0.5f).
            OnComplete(delegate { logoname.gameObject.SetActive(false); });
        menuui.DOAnchorPosY(-100.0f, 0.5f).
            OnComplete(delegate { menuui.gameObject.SetActive(false); });
    }

    public void UpdateGameUI(int score, int highscore)
    {
        this.score.text = score.ToString();
        this.highscore.text = highscore.ToString();
    }

    public void ShowGameUI(int score = 0,int highscore = 0)
    {
        this.score.text = score.ToString();
        this.highscore.text = highscore.ToString();

        gameui.gameObject.SetActive(true);
        gameui.DOAnchorPosY(-78.2f, 0.5f );
    }

    public void HideGameUI()
    {
        gameui.DOAnchorPosY(78.65f, 0.5f).
            OnComplete(delegate { gameui.gameObject.SetActive(false); });
    }

    public void ShowRestartButton()
    {
        restartButton.SetActive(true);
    }

    public void ShowGameOverUI(int score = 0)
    {
        gameOverUI.SetActive(true);
        gameOverScore.text = score.ToString();
    }

    public void HideGameOverUI()
    {
        gameOverUI.SetActive(false);
    }

    public void HomeButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    public void ShowRankUI(int score,int highScore,int numberGames)
    {
        rankScore.text = score.ToString();
        rankHighscore.text = highScore.ToString();
        rankNumberGames.text = numberGames.ToString();
        rankUI.SetActive(true);
    }

    public void HideRankUI()
    {
        rankUI.SetActive(false);
    }

    public void SetMuteActive(bool isActive)
    {
        muteUI.SetActive(isActive);
    }

    public void SetSettingUIActive(bool isActive)
    {
        setUI.SetActive(isActive);
    }
}
