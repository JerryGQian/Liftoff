using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public AudioSource explosionSource;

    void Awake() {
        Util.audioManager = this;
    }
	// Use this for initialization
	void Start () {
	
	}
	
}
