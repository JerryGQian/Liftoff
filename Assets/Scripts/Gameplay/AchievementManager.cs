using UnityEngine;
using System.Collections;
using System;
#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#elif UNITY_IOS
using UnityEngine.SocialPlatforms.GameCenter;
#endif

public class AchievementManager : MonoBehaviour {

    void Awake() {
        Util.achievementManager = this;
    }

	// Use this for initialization
	void Start () {
        setupGPGS();
	}

    public void setupGPGS() {
#if UNITY_ANDROID
        Debug.Log("Activating GPGS");
        PlayGamesPlatform.Activate();
        Debug.Log("GPGS Activated");
        authenticate();
#elif UNITY_IOS
        authenticate();
#endif
    }

    public void authenticate() {
        try {
            Debug.Log("Authenticating...");
            Social.localUser.Authenticate((bool success) => {
                Debug.Log("Authenticated, Posting scores.");
                // handle success or failure
                if (success) Debug.Log("Success Authenticate");
            });
        }
        catch (Exception e) {
            Debug.LogError("ERROR AUTHENTICATION: " + e.Message);
        }
    }

    public void checkAchievementsDistance() {
        if (Util.gm.distance > 110f) {
            Social.ReportProgress("CgkI-bbVjLkNEAIQBA", 100f, (bool success) => { });
        }
        if (Util.gm.distance > 350f) {
            Social.ReportProgress("CgkI-bbVjLkNEAIQBQ", 100f, (bool success) => { });
        }
        if (Util.gm.distance > 450f) {
            Social.ReportProgress("CgkI-bbVjLkNEAIQBg", 100f, (bool success) => { });
        }
    }

    public void buyRocketAchievement() {
        Social.ReportProgress("CgkI-bbVjLkNEAIQBw", 100f, (bool success) => { });
    }

    public void firstLaunch() {
        Social.ReportProgress("CgkI-bbVjLkNEAIQAw", 100f, (bool success) => { });
    }
}
