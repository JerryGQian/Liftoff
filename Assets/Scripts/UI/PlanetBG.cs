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

    // Use this for initialization
    void Start() {
        Sprite sp = null;
        float size = 1f;
        switch (Util.gm.zoneID) {
            case 5: sp = moon; size = 1.5f; break;
            case 6: sp = mars; size = 2f; break;
            case 8: sp = jupiter; size = 4f; break;
            case 9: sp = saturn; size = 4f; break;
            case 10: sp = uranus; size = 3.5f; break;
            case 11: sp = neptune; size = 3f; break;
            default: Destroy(gameObject); break;
        }
        GetComponent<SpriteRenderer>().sprite = sp;
        transform.position = new Vector3(Random.Range(-Util.width, Util.width), Camera.main.transform.position.y + 12f * size, 0);
        transform.localScale = new Vector3(size, size, size);
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360f));


        GetComponent<Motion>().endPos = new Vector3(transform.position.x, Camera.main.transform.position.y + GameManager.rocketSpeed * GameManager.zoneTime * 0.75f, 0);
        GetComponent<Motion>().duration = GameManager.zoneTime * 1.6f;
        GetComponent<Motion>().begin();
    }
}
