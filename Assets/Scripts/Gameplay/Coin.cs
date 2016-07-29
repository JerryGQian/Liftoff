using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
    public static float spinSpeed = 150f;
    public GameObject coinCanvasPrefab;
	// Use this for initialization
	void Start () {
        transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0, 360f));
    }
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles = transform.eulerAngles + new Vector3(0f, 0f, spinSpeed / 2f * Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D coll) {
        if (Util.wm.gameActive && coll.gameObject.name.Equals("Rocket")) {
            
            //SPAWN +1 TEXT HERE
            GameObject obj = Instantiate(coinCanvasPrefab);
            obj.GetComponent<RectTransform>().anchoredPosition = Camera.main.WorldToScreenPoint(transform.position);
            obj.GetComponent<RectTransform>().localScale = new Vector3(1.3f, 1.3f, 1.3f);
            obj.transform.SetParent(Util.canvas.transform);
            Destroy(gameObject);
        }
    }
}
