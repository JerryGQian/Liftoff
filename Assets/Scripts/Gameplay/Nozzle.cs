using UnityEngine;
using System.Collections;

public class Nozzle : MonoBehaviour {
    public float maxAngle = 10f;
    public float nozzleAngle = 0;

    float spewDelay = 0.1f;
    public GameObject flamePrefab;
    GameObject flame;


    public Sprite f1;
    public Sprite f2;
    //public Sprite f3;
    void Awake() {
        Util.nozzle = this;
    }
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        nozzleAngle = maxAngle * Util.im.angleRatio;
        transform.eulerAngles = new Vector3(0, 0, nozzleAngle);
    }

    public void spew() {
        if (Util.rocket.ri.fire) {
            flame = Instantiate(flamePrefab);
            flame.transform.position = transform.position - new Vector3(0, -0.4f);
            flame.transform.SetParent(transform);
            flame.transform.eulerAngles = new Vector3(0, 0, 0);
            flame.transform.localScale = new Vector3(0.5f, 2f, 1f);
            if (Util.wm.gameActive) Invoke("spew", spewDelay);
        }
    }
}
