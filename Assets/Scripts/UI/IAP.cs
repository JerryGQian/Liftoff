﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System;

public class IAP : MonoBehaviour{

    public Text timer;

    public SmoothMotion sm1;
    public SmoothMotion sm2;

    public static int IAP1 = 5000;
    public static int IAP2 = 20000;
    public static int IAP3 = 80000;

    public GameObject restorePurchases;



    // Use this for initialization
    void Start () {
        transform.SetParent(Util.canvas.transform);
        transform.localScale = new Vector3(1f, 1f, 1f);
        GetComponent<RectTransform>().anchoredPosition = new Vector3(1500f, 0, 0);

        sm1.begin();
        Util.audioManager.playMenuSwoosh();


//#if UNITY_ANDROID
        Destroy(restorePurchases);
//#endif
    }

    void Update() {
        if (Advertisement.IsReady()) {
            if (Util.wm.adWatchTimeCoins > 0) {
                timer.text = Util.encodeTimeColon(Util.wm.adWatchTimeCoins);
            }
            else {
                timer.text = "READY";
            }
        }
        else {
            timer.text = "LOADING";
        }
    }

    public void close() {
        sm2.startPos = GetComponent<RectTransform>().anchoredPosition;
        sm2.begin();
        Util.audioManager.playMenuSwoosh();
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
                Util.wm.spawnCoinPile();
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

    public void buy1() {
        Util.wm.GetComponent<Purchaser>().Buy1();
    }
    public void buy2() {
        Util.wm.GetComponent<Purchaser>().Buy2();
    }
    public void buy3() {
        Util.wm.GetComponent<Purchaser>().Buy3();
    }
}

