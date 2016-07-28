using UnityEngine;
using System.Collections;

public class CoinCanvas : MonoBehaviour {
    Vector2 targetPos;
	// Use this for initialization
	void Start () {
        targetPos = Util.coin.anchoredPosition + new Vector2(540f, 960f);
        GetComponent<SmoothMotion>().endPos = targetPos;
        GetComponent<SmoothMotion>().duration = 0.8f;
        GetComponent<SmoothMotion>().begin();
    }
}
