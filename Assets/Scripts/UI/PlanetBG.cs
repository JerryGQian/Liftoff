using UnityEngine;
using System.Collections;

public class PlanetBG : MonoBehaviour {
    public Sprite moon;
    public Sprite mars;
    public Sprite jupiter;
    public Sprite saturn;
    public Sprite uranus;
    public Sprite neptune;
    public Sprite pluto;


    public Sprite a0;
    public Sprite a1;
    public Sprite a2;
    public Sprite a3;

    public Sprite au0;
    public Sprite au1;
    public Sprite au2;
    public Sprite au3;


    Sprite sp;
    float size;
    // Use this for initialization
    void Start() {
        sp = null;
        size = 1f;
        switch (Util.gm.zoneID) {
            case 3: pickAurora(); break;
            case 5: sp = moon; size = 1.5f; break;
            case 6: sp = mars; size = 2f; break;
            case 7: pickAsteroid(); break;
            case 8: sp = jupiter; size = 4f; break;
            case 9: sp = saturn; size = 4f; break;
            case 10: sp = uranus; size = 3.5f; break;
            case 11: sp = neptune; size = 3f; break;
            case 12: pickAsteroid(); break;
            default: Destroy(gameObject); break;
        }
        GetComponent<SpriteRenderer>().sprite = sp;
        transform.position = new Vector3(Random.Range(-Util.width, Util.width), Camera.main.transform.position.y + 15f * size, 0);
        transform.localScale = new Vector3(size, size, size);
        if (Util.gm.zoneID != 3) transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360f));


        GetComponent<Motion>().endPos = new Vector3(transform.position.x, Camera.main.transform.position.y + GameManager.rocketSpeed * GameManager.zoneTime * 0.75f, 0);
        GetComponent<Motion>().duration = GameManager.zoneTime * 1.6f;
        GetComponent<Motion>().begin();
    }

    void pickAsteroid() {
        switch ((int)Random.Range(0, 3.99f)) {
            case 0: sp = a0; break;
            case 1: sp = a1; break;
            case 2: sp = a2; break;
            case 3: sp = a3; break;
        }
        size = Random.Range(1f, 1.9f);
    }

    void pickAurora() {
        switch ((int)Random.Range(0, 3.99f)) {
            case 0: sp = au0; break;
            case 1: sp = au1; break;
            case 2: sp = au2; break;
            case 3: sp = au3; break;
        }
        size = Random.Range(1.5f, 2.5f);
        GetComponent<SpriteRenderer>().color = new Color(Random.Range(0, 1f), 1f, Random.Range(0, 1f));
    }
}
