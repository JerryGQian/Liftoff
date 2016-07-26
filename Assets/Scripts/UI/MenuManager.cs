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

    public GameObject playButton;

    void Awake() {
        Util.menuManager = this;
    }
    // Use this for initialization
    void Start() {
        frameDelay = 1f / frameRate;
        scoreText = score.GetComponent<Text>();

        showMainMenu();
    }

    public void updateScore(int i) {
        scoreText.text = "" + i;
    }

    public void showMainMenu() {
        hideAll();

        menuType = MenuType.main;
        playButton.SetActive(true);
        title.SetActive(true);
    }

    public void hideMainMenu() {
        menuType = MenuType.none;
        playButton.SetActive(false);
        title.SetActive(false);
    }

    public void showPlayScreen() {
        hideAll();

        menuType = MenuType.play;
        score.SetActive(true);
    }
    public void hidePlayScreen() {
        score.SetActive(false);
    }

    public void showReplayMenu() {
        hideAll();

        menuType = MenuType.replay;
        playButton.SetActive(true);
        score.SetActive(true);
    }

    public void hideReplayMenu() {
        menuType = MenuType.none;
        playButton.SetActive(false);
    }

    public void hideAll() {
        if (menuType == MenuType.main) {
            hideMainMenu();
        }
        hidePlayScreen();
        hideReplayMenu();
    }

}
