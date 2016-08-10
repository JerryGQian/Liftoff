using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class SecondChance : MonoBehaviour {
    float time;
    float life = 2.5f;

    public GameObject bar;

    public SmoothMotion s1;
    public SmoothMotion s2;
	// Use this for initialization
	void Start () {
        time = life;

        transform.SetParent(Util.canvas.transform);
        transform.localScale = new Vector3(1f, 1f, 1f);
        GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 1000f, 0);

        s1.begin();
	}
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
        bar.transform.localScale = new Vector3(time / life, 1f, 1f);
        if (time < 0 && !s2.began) {
            close();
        }
	}

    public void watchAd() {
        if (Advertisement.IsReady()) {
            ShowOptions options = new ShowOptions();
            options.resultCallback = HandleShowResultCoins;
            Debug.Log("Showing Video");
            close();
            Advertisement.Show(Util.wm.zoneID, options);
        }
    }

    public void HandleShowResultCoins(ShowResult result) {
        switch (result) {
            case ShowResult.Finished:
                Debug.Log("Video completed. Restarting!");
                Util.gm.distance -= GameManager.scoreSpeed * 2f;
                Util.wm.adWatchTimeLife = Util.adLifeCooldown;
                Util.wm.gamesSinceAdWatch = 0;
                Util.gm.restart();
                //CancelInvoke("close");
                //close();
                break;
            case ShowResult.Skipped:
                Debug.LogWarning("Video was skipped.");
                break;
            case ShowResult.Failed:
                Debug.LogError("Video failed to show.");
                break;
        }
    }

    public void close() {
        s2.startPos = new Vector3(0, 0, 0);
        s2.begin();
    }
}
