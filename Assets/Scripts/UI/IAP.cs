using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class IAP : MonoBehaviour {

    public Text timer;

    public SmoothMotion sm1;
    public SmoothMotion sm2;

    // Use this for initialization
    void Start () {
        transform.SetParent(Util.canvas.transform);
        transform.localScale = new Vector3(1f, 1f, 1f);
        GetComponent<RectTransform>().anchoredPosition = new Vector3(1500f, 0, 0);

        sm1.begin();
    }

    void Update() {
        timer.text = Util.encodeTimeColon(Util.wm.adWatchTimeCoins);
    }

    public void close() {
        sm2.startPos = GetComponent<RectTransform>().anchoredPosition;
        sm2.begin();
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
                WorldManager.updateCoinCount();
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
