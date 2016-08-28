using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Alert : MonoBehaviour {
    public GameObject parent;
    RectTransform rect;
    public Sprite urgentSprite;
    Vector3 finalPos;
    float targetScale = 1f;
    float juice = 3f;

	// Use this for initialization
	void Start () {
        transform.SetParent(Util.canvas.transform);
        
        rect = GetComponent<RectTransform>();
        rect.localScale = new Vector3(targetScale, targetScale, targetScale);
        finalPos = new Vector3((parent.transform.position.x / Util.width) * 540f, -125f, 0);
        rect.anchoredPosition = new Vector3(Random.Range(-540f, 540f), -125f, 0);

        Invoke("changeSprite", GetComponent<Suicider>().life / 2f);
	}
	
	// Update is called once per frame
	void Update () {
        rect.anchoredPosition = rect.anchoredPosition + new Vector2((finalPos.x - rect.anchoredPosition.x) * Time.deltaTime * juice, 0);
        rect.localScale = rect.localScale + new Vector3(1f, 1f, 1f) * (targetScale - rect.localScale.x) * Time.deltaTime * juice * 3f;
    }

    void changeSprite() {
        GetComponent<Image>().sprite = urgentSprite;
        rect.localScale = new Vector3(1f, 1f, 1f) * targetScale * 2f;
        Util.audioManager.playAlertBeep();
    }
}
