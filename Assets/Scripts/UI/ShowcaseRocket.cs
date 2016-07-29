using UnityEngine;
using System.Collections;

public class ShowcaseRocket : MonoBehaviour {
    RocketInfo rocketInfo;
    public SpriteRenderer rocket;
	// Use this for initialization
	void Start () {
        
    }

    public void setup(RocketInfo ri) {
        rocketInfo = ri;
        rocket.sprite = rocketInfo.sprite;
        transform.SetParent(Util.scrollManager.scrollParent.transform);
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }
}
