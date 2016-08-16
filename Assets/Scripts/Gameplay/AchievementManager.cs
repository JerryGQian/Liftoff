﻿using UnityEngine;
using System.Collections;
using System;
#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#elif UNITY_IOS
using UnityEngine.SocialPlatforms.GameCenter;
#endif

public class AchievementManager : MonoBehaviour {

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
}