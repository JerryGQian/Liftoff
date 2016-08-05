using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum MenuType { main, play, replay, none, other }

public class MenuManager : MonoBehaviour {
    static float frameRate = 50f;
    static float frameDelay;

    public MenuType menuType;

    public GameObject title;
    public GameObject score;
    Text scoreText;
    public GameObject coins;
    public GameObject best;
    public GameObject wind;

    public GameObject playButton;
    public GameObject settingsButton;
    public GameObject leaderButton;
    public GameObject backPanel;
    public GameObject rocketSelectorPanel;




    bool firstShowing = true;  //if this is the first time score is shown.
    void Awake() {
        Util.menuManager = this;
    }
    // Use this for initialization
    void Start() {
        frameDelay = 1f / frameRate;
        scoreText = score.GetComponent<Text>();

        float ratio = Screen.height / 1920f;

        best.GetComponent<RectTransform>().anchoredPosition = new Vector3(Screen.width / -ratio * 0.5f * 0.9f, best.GetComponent<RectTransform>().anchoredPosition.y);
        wind.GetComponent<RectTransform>().anchoredPosition = new Vector3(Screen.width / -ratio * 0.5f * 0.9f, wind.GetComponent<RectTransform>().anchoredPosition.y);

        coins.GetComponent<RectTransform>().anchoredPosition = new Vector3(Screen.width / ratio * 0.5f * 0.85f, coins.GetComponent<RectTransform>().anchoredPosition.y);
        coins.GetComponent<SmoothMotion>().endPos = new Vector3(coins.GetComponent<RectTransform>().anchoredPosition.x, 1920f * 0.46f);
        coins.GetComponent<SmoothMotion>().startPos = coins.GetComponent<RectTransform>().anchoredPosition;

        score.GetComponent<SmoothMotion>().endPos = new Vector3(Screen.width / -ratio * 0.5f * 0.9f, score.GetComponent<RectTransform>().anchoredPosition.y);

        showMainMenu();
    }

    public void updateScore(int i) {
        scoreText.text = "" + i;
    }

    /// <summary>
    /// Main menu
    /// </summary>

    public void showMainMenu() {
        hideAll();

        menuType = MenuType.main;

        playButton.SetActive(true);
        settingsButton.SetActive(true);
        leaderButton.SetActive(true);
        backPanel.SetActive(true);
        rocketSelectorPanel.SetActive(true);
        playButton.GetComponent<SmoothMotion>().reset();
        settingsButton.GetComponent<SmoothMotion>().reset();
        leaderButton.GetComponent<SmoothMotion>().reset();
        backPanel.GetComponent<SmoothMotion>().reset();
        rocketSelectorPanel.GetComponent<SmoothMotion>().reset();

        title.SetActive(true);
        title.GetComponent<SmoothMotion>().reset();
    }

    public void hideMainMenu() {
        menuType = MenuType.none;
        playButton.GetComponent<SmoothMotion>().begin();
        settingsButton.GetComponent<SmoothMotion>().begin();
        leaderButton.GetComponent<SmoothMotion>().begin();
        backPanel.GetComponent<SmoothMotion>().begin();
        rocketSelectorPanel.GetComponent<SmoothMotion>().begin();
        title.GetComponent<SmoothMotion>().begin();
        
    }

    /// <summary>
    /// PlayScreen
    /// </summary>

    public void showPlayScreen() {
        hideAll();

        menuType = MenuType.play;
        score.SetActive(true);
        if (firstShowing) {
            score.GetComponent<SmoothMotion>().begin();
            coins.GetComponent<SmoothMotion>().begin();
            firstShowing = false;
        }
        Util.scrollManager.hidden(true);
        Util.wm.plusIcon.SetActive(false);

    }
    public void hidePlayScreen() {
        score.SetActive(false);
        Util.scrollManager.hidden(false);

    }

    /// <summary>
    /// ReplayMenu
    /// </summary>

    public void showReplayMenu() {
        hideAll();

        menuType = MenuType.replay;

        score.SetActive(true);
        playButton.SetActive(true);
        settingsButton.SetActive(true);
        leaderButton.SetActive(true);
        backPanel.SetActive(true);
        rocketSelectorPanel.SetActive(true);
        playButton.GetComponent<SmoothMotion>().reset();
        settingsButton.GetComponent<SmoothMotion>().reset();
        leaderButton.GetComponent<SmoothMotion>().reset();
        backPanel.GetComponent<SmoothMotion>().reset();
        rocketSelectorPanel.GetComponent<SmoothMotion>().reset();

        Util.wm.plusIcon.SetActive(true);
    }

    public void hideReplayMenu() {
        menuType = MenuType.none;
        playButton.GetComponent<SmoothMotion>().begin();
        settingsButton.GetComponent<SmoothMotion>().begin();
        leaderButton.GetComponent<SmoothMotion>().begin();
        backPanel.GetComponent<SmoothMotion>().begin();
        rocketSelectorPanel.GetComponent<SmoothMotion>().begin();
        title.GetComponent<SmoothMotion>().begin();
    }

    public void hideAll() {
        if (menuType == MenuType.main) {
            hideMainMenu();
        }
        hidePlayScreen();
        hideReplayMenu();
    }

}
