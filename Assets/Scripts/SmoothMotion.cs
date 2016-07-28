using UnityEngine;
using System.Collections;

public class SmoothMotion : MonoBehaviour {
    public Vector3 startPos;
    public Vector3 endPos;
    public float duration = 1f;
    bool began = false;
    private float startTime;

    bool isRectTransform = false;

    public bool deactivateOnFinish;
    public bool deleteOnFinish;
    RectTransform rect;
    void Awake() {
        if (GetComponent<RectTransform>() != null) {
            isRectTransform = true;
            rect = GetComponent<RectTransform>();
        }
    }
    void Start() {
        if (!isRectTransform) {
            startPos = transform.position;
        }
        else {
            startPos = rect.anchoredPosition;
        }
    }
    public void begin() {
        startTime = Time.time;
        began = true;
        
        Invoke("end", duration);
    }

    public void end() {
        began = false;
        if (deactivateOnFinish) {
            gameObject.SetActive(false);
        }
        if (deleteOnFinish) {
            Destroy(gameObject);
        }
    }

    public void reset() {
        if (!isRectTransform) {
            transform.position = startPos;
        }
        else {
            rect.anchoredPosition = startPos;
        }
        began = false;
        CancelInvoke("end");
    }

    void Update() {
        if (began && Time.time < startTime + duration) {
            float t = (Time.time - startTime) / duration;
            if (!isRectTransform) {
                transform.position = new Vector3(Mathf.SmoothStep(startPos.x, endPos.x, t), Mathf.SmoothStep(startPos.y, endPos.y, t), Mathf.SmoothStep(startPos.z, endPos.z, t));
            }
            else {
                rect.anchoredPosition = new Vector3(Mathf.SmoothStep(startPos.x, endPos.x, t), Mathf.SmoothStep(startPos.y, endPos.y, t), Mathf.SmoothStep(startPos.z, endPos.z, t));
            }
        }
    }
}