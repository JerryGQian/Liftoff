using UnityEngine;
using System.Collections;

public class SideBoundBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerExit2D(Collider2D coll) {
        if (Util.wm.gameActive && coll.gameObject.name.Equals("Rocket") && !Util.gm.invincible) {
            Util.gm.die();
        }
    }
}
