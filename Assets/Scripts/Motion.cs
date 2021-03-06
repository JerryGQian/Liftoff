﻿using UnityEngine;
using System.Collections;

public class Motion : MonoBehaviour {
    public Vector3 startPos;
    public Vector3 endPos;
    public float duration = 1f;
    bool began = false;
    private float startTime;

    bool isRectTransform = false;

    public bool deactivateOnFinish;
    public bool destroyOnFinish;
    RectTransform rect;
    void Awake() {
        if (GetComponent<RectTransform>() != null) {
            isRectTransform = true;
            rect = GetComponent<RectTransform>();
        }
    }
    void Start() {

    }
    public void begin() {
        startTime = Time.time;
        began = true;
        if (!isRectTransform) {
            startPos = transform.position;
        }
        else {
            startPos = rect.anchoredPosition;
        }
        Invoke("end", duration);
    }

    public void end() {
        began = false;
        if (destroyOnFinish) {
            Destroy(gameObject);
        }
        else if (deactivateOnFinish) {
            gameObject.SetActive(false);
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
                transform.position = new Vector3(Mathf.Lerp(startPos.x, endPos.x, t), Mathf.Lerp(startPos.y, endPos.y, t), Mathf.Lerp(startPos.z, endPos.z, t));
            }
            else {
                rect.anchoredPosition = new Vector3(Mathf.Lerp(startPos.x, endPos.x, t), Mathf.Lerp(startPos.y, endPos.y, t), Mathf.Lerp(startPos.z, endPos.z, t));
            }
        }
    }
}