using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WindBar : MonoBehaviour {
    RectTransform rect;
    Image img;
    void Awake() {
        
        
    }

	// Use this for initialization
	void Start () {
        transform.SetParent(Util.canvas.transform);
        rect = GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector3(0, 0, 0);
        rect.localScale = new Vector3(1f, 1f, 1f);

        img = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Util.wm.gameActive) {
            if (Wind.maxWind != 0) {
                rect.localScale = new Vector3(Wind.wind / Wind.maxWind, 1f, 1f);
                if (Wind.wind > 0) {
                    img.color = new Color(0.5f, 1f, 0.5f, 0.2f);
                }
                else {
                    img.color = new Color(1f, 0.7f, 0.7f, 0.3f);
                }
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
