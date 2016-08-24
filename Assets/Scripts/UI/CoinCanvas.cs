using UnityEngine;
using System.Collections;

public class CoinCanvas : MonoBehaviour {
    Vector2 targetPos;
    bool randomized = false;
    // Use this for initialization
    void Awake() {
        float scale = 1.125f * (Screen.height / 1920f);
        GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
        targetPos = Util.wm.coinIcon.anchoredPosition + new Vector2(540f, 960f);
    }
	void Start () {
        transform.SetParent(Util.canvas.transform);
        
        GetComponent<SmoothMotion>().endPos = targetPos;
        GetComponent<SmoothMotion>().startPos = GetComponent<RectTransform>().anchoredPosition;
        GetComponent<SmoothMotion>().duration = 0.8f;
        GetComponent<SmoothMotion>().begin();
        if (!randomized) Invoke("incrementCoin", 0.7f);
    }

    public void randomize(Vector2 pos, float w, float h) {
        randomized = true;
        GetComponent<RectTransform>().anchoredPosition = pos + new Vector2(Random.Range(-w, w), Random.Range(-h, h));
    }

    void incrementCoin() {
        Util.wm.coins++;
        WorldManager.updateCoinCount();
    }
}
