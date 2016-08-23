using UnityEngine;
using System.Collections;

public class WindBar : MonoBehaviour {
    RectTransform rect;
    void Awake() {
        
        
    }

	// Use this for initialization
	void Start () {
        transform.SetParent(Util.canvas.transform);
        rect = GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector3(0, 0, 0);
        rect.localScale = new Vector3(1f, 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
        if (Util.wm.gameActive) {
            if (Wind.maxWind != 0) {
                rect.localScale = new Vector3(Wind.wind / Wind.maxWind, 1f, 1f);
            }
            else {
                rect.localScale = new Vector3(0, 1f, 1f);
            }
            
        }
        else {
            rect.localScale = new Vector3(0, 1f, 1f);
        }
	}
}
