using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
public class AdManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RequestBanner();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void RequestBanner() {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3270795222614514/4092700411";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3270795222614514/7046166818";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);
    }
}
