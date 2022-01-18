using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{

    #region Singleton class: UIManager

    public static UIManager Instance;



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion


    [SerializeField] GameObject WinPanel;


    [SerializeField] GameObject GameOverPanel;

    [SerializeField] Text Score;

    [SerializeField] GameObject PausePanel;

    [SerializeField] GameObject PauseBtn;

    [SerializeField] Image fadePanel;

    [SerializeField] Image progressFillImage;

    [SerializeField] int sceneOffset;
    [SerializeField] Text nextLevelText;
    [SerializeField] Text currentLevelText;

    [SerializeField] GameObject NativePanel;



    void Start()
    {
        WinPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);
        PauseBtn.SetActive(true);
        NativePanel.SetActive(false);

        FadeAtStart();

        Game.won = false;
        Game.lost = false;

        progressFillImage.fillAmount = 0f;
        SetLevelProgressText();

    }
    private void Update()
    {
        Score.text = PlayerPrefs.GetInt("Score").ToString();
    }

    public void UpdateLevelProgress()
    {
        float val = 1f - ((float)Level.Instance.objectsInScene / Level.Instance.totalObjects);
        //transition fill (0.4 seconds)
        progressFillImage.DOFillAmount(val, 0.4f);
    }

    void SetLevelProgressText()
    {
        int level = SceneManager.GetActiveScene().buildIndex + sceneOffset;
        currentLevelText.text = level.ToString();
        nextLevelText.text = (level + 1).ToString();
    }

    public void ActivateWinPanel ()
    {
        WinPanel.SetActive(true);
        FadeAtStart();
        Interstitial.Instance.ShowInterstitial();





    }

    public void ActivateGameOverPanel()
    {
        GameOverPanel.SetActive(true);
        Game.lost = true;
        FadeAtStart();
    }

    public void DeactivateGameOverPanel()
    {
        GameOverPanel.SetActive(false);
    }


        public void ActivatePausePanel()
    {
       
        PausePanel.SetActive(true);
        PauseBtn.SetActive(false);
        Game.isGameover = true;
        FadeAtStart();

    }

    public void DeactivatePausePanel()
    {
        PausePanel.SetActive(false);
        PauseBtn.SetActive(true);
        Game.isGameover = false;

    }
    public void ActivateNativePanel()
    {

        NativePanel.SetActive(true);

    }

    public void FadeAtStart()
    {
        fadePanel.DOFade(0f, 1.3f).From(1f);
    }


}



