using UnityEngine;
using System.Collections;

public class ColorSwitcher : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.SetParent(Util.canvas.transform);
        GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(-225f, 331f);
	}
	
	public void switchColor() {
        switch (Util.rocketHolder.crayonColor) {
            case CrayonColor.one: Util.rocketHolder.crayonColor = CrayonColor.two; break;
            case CrayonColor.two: Util.rocketHolder.crayonColor = CrayonColor.three; break;
            case CrayonColor.three: Util.rocketHolder.crayonColor = CrayonColor.four; break;
            case CrayonColor.four: Util.rocketHolder.crayonColor = CrayonColor.one; break;
        }
        Debug.Log("Switch color");
    }
}
