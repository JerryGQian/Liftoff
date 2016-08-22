using UnityEngine;
using System.Collections;

public class DailyReward : MonoBehaviour {

    public SmoothMotion sm1;
    public SmoothMotion sm2;

    // Use this for initialization
    void Start() {
        transform.SetParent(Util.canvas.transform);
        transform.localScale = new Vector3(1f, 1f, 1f);
        GetComponent<RectTransform>().anchoredPosition = new Vector3(1500f, 208f, 0);

        sm1.begin();
    }

    void Update() {
        
    }

    public void close() {
        sm2.startPos = GetComponent<RectTransform>().anchoredPosition;
        sm2.begin();
    }
}
