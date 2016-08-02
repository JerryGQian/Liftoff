using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ZoneText : MonoBehaviour {

    public SmoothMotion sm1;
    public SmoothMotion sm2;

	// Use this for initialization
	void Start () {
        
        RectTransform rect = GetComponent<RectTransform>();
        rect.SetParent(Util.canvas.transform);
        rect.anchoredPosition = new Vector2(111f, -2500f);
        sm1.begin();
        rect.localScale = new Vector3(1f, 1f, 1f);
	}

    public void setText(string str) {
        GetComponent<Text>().text = str.ToUpper();
    }

    public void remove() {
        sm2.startPos = sm1.endPos;
        sm2.begin();
    }
}
