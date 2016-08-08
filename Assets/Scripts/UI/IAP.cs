using UnityEngine;
using System.Collections;

public class IAP : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.SetParent(Util.canvas.transform);
        transform.localScale = new Vector3(1f, 1f, 1f);
        GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }

    public void close() {
        Destroy(gameObject);
    }
}
