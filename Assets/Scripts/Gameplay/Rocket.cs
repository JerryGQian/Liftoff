using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {
    public float rocketAngle = 0;
    public float tipRate;
    public float engineForce;

    public float tipRateActual;
    public float engineForceActual;

    public float tipAmount = 0;
    public float enginePush = 0;
    public float finalAngle = 0;
    public Vector3 finalVector;

    public Vector3 bottomPos;
    public Vector3 tipPos;

    public SpriteRenderer rocketRenderer;
    public SpriteRenderer nozzleRenderer;
    public SpriteRenderer shockDiamondRenderer;

    public RocketInfo ri;

    public AudioClip rocketBG;
    public AudioClip car;
    public AudioClip drone;
    public AudioClip jet;
    public AudioClip gun;

    public AudioSource rocketBGSource;
    public AudioSource rocketStartSource;

    void Awake() {
        Util.rocket = this;
    }

	// Use this for initialization
	void Start () {
        bottomPos = transform.position;
        tipPos = transform.position + new Vector3(-Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.z), Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.z), 0) * 5f;
        rocketAngle = transform.eulerAngles.z;
        //transform.FindChild("Rocket").gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 2f, 0);
    }

    // Update is called once per frame
    void Update() {
        if (Util.wm.gameActive && !Util.gm.invincible) {
            bottomPos = transform.position;
            rocketAngle = transform.eulerAngles.z - 90f;
            tipPos = transform.position + new Vector3(-Mathf.Sin(Mathf.Deg2Rad * rocketAngle), Mathf.Cos(Mathf.Deg2Rad * rocketAngle), 0) * 5f;
            tipAmount = -rocketAngle / 90f * tipRateActual * Time.deltaTime + Wind.wind;
            tipPos += new Vector3(tipAmount + Random.Range(-0.0001f, 0.0001f), 0);
            bottomPos += new Vector3(tipAmount, 0);
            enginePush = (-Util.nozzle.nozzleAngle) / 90f * engineForceActual * Time.deltaTime;
            bottomPos += new Vector3(enginePush, 0);
            finalVector = (tipPos - bottomPos);
            if (finalVector.x >= 0) {
                finalAngle = Mathf.Atan(finalVector.y / finalVector.x) * Mathf.Rad2Deg;
            }
            else {
                finalAngle = -(Mathf.Atan(finalVector.y / -finalVector.x) * Mathf.Rad2Deg - 90f) + 90f;
            }
            //finalAngle = Mathf.Atan(finalVector.y / finalVector.x) * Mathf.Rad2Deg + 90f;
            transform.position = bottomPos + new Vector3(0, Mathf.Cos(Util.wm.gameTime) * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, 0, finalAngle);
        }
    }

    public void setup(RocketInfo ri) {
        this.ri = ri;
        rocketRenderer.sprite = ri.sprite;
        if (ri.nozzle) {
            nozzleRenderer.color = new Color(1f, 1f, 1f);
        }
        else {
            nozzleRenderer.color = new Color(1f, 1f, 1f, 0);
        }

        if (ri.fire == FlameType.flame) {
            shockDiamondRenderer.color = new Color(1f, 1f, 1f, 0.4f);
        }
        else {
            shockDiamondRenderer.color = new Color(1f, 1f, 1f, 0);
        }

        if (!Util.wm.soundMuted) {
            switch (ri.sound) {
                case SoundType.rocket: rocketBGSource.clip = rocketBG;  break;
                case SoundType.none: rocketBGSource.clip = new AudioClip(); break;
                case SoundType.jet: rocketBGSource.clip = jet; break;
                case SoundType.drone: rocketBGSource.clip = drone; break;
                case SoundType.car: rocketBGSource.clip = car; break;
                case SoundType.gun: rocketBGSource.clip = gun; break;
            }
            rocketBGSource.Play();
            rocketStartSource.Play();
        }
        else {
            rocketBGSource.Stop();
            rocketStartSource.Stop();
        }
    }
}
