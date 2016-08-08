using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class IAP : MonoBehaviour {

    public Text timer;

	// Use this for initialization
	void Start () {
        transform.SetParent(Util.canvas.transform);
        transform.localScale = new Vector3(1f, 1f, 1f);
        GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }

    void Update() {
        timer.text = Util.encodeTimeColon(Util.wm.adWatchTimeCoins);
    }

    public void close() {
        Destroy(gameObject);
    }

    public void watchAd() {
        if (Util.wm.adWatchTimeCoins <= 0) {
            if (Advertisement.IsReady()) {
                ShowOptions options = new ShowOptions();
                options.resultCallback = HandleShowResultCoins;
                Debug.Log("Showing Video");
                Advertisement.Show(Util.wm.zoneID, options);
            }
        }
        else {
            //em.list.transform.FindChild("AdForMoney").transform.FindChild("TimerText").GetComponent<Animator>().SetTrigger("Flash");
        }
    }

    public void HandleShowResultCoins(ShowResult result) {
        switch (result) {
            case ShowResult.Finished:
                //if (Util.adMoneyCooldown <= 0) {
                Debug.Log("Video completed. Rewarded $" + Util.coinReward);
                int num = Util.coinReward;
                Util.wm.coins += num;
                Util.wm.adWatchTimeCoins = Util.adCoinsCooldown;
                Util.saveManager.save();
                //}
                break;
            case ShowResult.Skipped:
                Debug.LogWarning("Video was skipped.");
                break;
            case ShowResult.Failed:
                Debug.LogError("Video failed to show.");
                break;
        }
    }
}
