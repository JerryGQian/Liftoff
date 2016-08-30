using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class SecondChance : MonoBehaviour {
    float time;
    float life = 3.5f;
    bool adwatch = false;
    bool ended = false;

    public GameObject bar;

    public SmoothMotion s1;
    public SmoothMotion s2;
	// Use this for initialization
	void Start () {
        time = life;
        ended = false;
        transform.SetParent(Util.canvas.transform);
        transform.localScale = new Vector3(1f, 1f, 1f);
        GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 1000f, 0);

        s1.begin();
        Util.audioManager.playMenuSwoosh();
    }
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
        bar.transform.localScale = new Vector3(time / life, 1f, 1f);
        if (time < 0 && !s2.began && !adwatch) {
            close();
        }
	}

    public void watchAd() {
        if (Advertisement.IsReady() && !ended) {
            adwatch = true;
            ShowOptions options = new ShowOptions();
            options.resultCallback = HandleShowResultCoins;
            Debug.Log("Showing Video");
            CancelInvoke("close");
            Advertisement.Show(Util.wm.zoneID, options);
        }
    }

    public void HandleShowResultCoins(ShowResult result) {
        switch (result) {
            case ShowResult.Finished:
                Debug.Log("Video completed. Restarting!");
                Util.gm.distance -= GameManager.scoreSpeed * GameManager.invincibleTime;
                Util.wm.adWatchTimeLife = Util.adLifeCooldown;
                Util.wm.gamesSinceAdWatch = 0;
                if (s2 != null) close();
                Util.gm.restart();
                
                break;
            case ShowResult.Skipped:
                Debug.LogWarning("Video was skipped.");
                if (s2 != null) close();
                break;
            case ShowResult.Failed:
                Debug.LogError("Video failed to show.");
                if (s2 != null) close();
                break;
        }
    }

    public void close() {
        ended = true;
        Util.wm.totalDistance += Util.gm.distance;
        Util.saveManager.save();
        s2.startPos = new Vector3(0, 0, 0);
        s2.begin();
        Util.audioManager.playMenuSwoosh();
    }
}
