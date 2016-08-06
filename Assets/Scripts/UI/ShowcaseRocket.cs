using UnityEngine;
using System.Collections;

public class ShowcaseRocket : MonoBehaviour {
    RocketInfo rocketInfo;
    public SpriteRenderer rocket;
    public SpriteRenderer nozzle;
	// Use this for initialization
	void Start () {
        
    }

    public void setup(RocketInfo ri) {
        rocketInfo = ri;
        rocket.sprite = rocketInfo.sprite;
        transform.SetParent(Util.scrollManager.scrollParent.transform);
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        if (ri.nozzle) {
            nozzle.color = new Color(1f, 1f, 1f);
        }
        else {
            nozzle.color = new Color(1f, 1f, 1f, 0);
        }
    }
}
