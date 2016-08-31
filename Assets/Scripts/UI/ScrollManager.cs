using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScrollManager : MonoBehaviour {
    public ScrollRect sr;
    public static float selector = 1;
    public float selectorVis;
    public static int selectedRocket = 1;
    public static float selectedRocketTarget;
    float selectorInitial = 0;
    public bool dragging;
    public static float scrollSpeed = 14f;
    public static float juice = 7f;
    public static float rocketSep = 4.5f;
    public static int rocketCount = 14;

    public GameObject showcaseRocketPrefab;
    public GameObject rocket1;
    public GameObject rocket2;
    public GameObject rocket3;

    public GameObject bg1;
    public GameObject bg2;
    public GameObject bg3;

    public GameObject scrollParent;
    public Text rocketName;
    public GameObject buyInfo;
    public GameObject owned;
    public Text cost;

    public RocketInfo ri;

    void Awake() {
        Util.scrollManager = this;
        //rocketCount = Util.rocketHolder.rocketCount;
    }

	// Use this for initialization
	void Start () {
        rocketCount = Util.rocketHolder.rocketCount;
        sr = GetComponent<ScrollRect>();
        if (Application.platform != RuntimePlatform.WindowsEditor) {
            Destroy(sr.horizontalScrollbar);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!dragging && !Util.wm.gameActive) {
            scrollParent.transform.position = scrollParent.transform.position + new Vector3((selectedRocketTarget - scrollParent.transform.position.x) * Time.deltaTime * juice, 0, 0);
            moveBG();
        }
        if (Util.even) {
            setRocket();
        }
        selectorVis = selector;
	}

    public void OnBeginDrag() {
        if (!Util.wm.gameActive) {
            dragging = true;
            selectorInitial = selector;
        }
    }

    public void OnDrag() {
        if (!Util.wm.gameActive) {
            int prevSelected = (int)(selector + 0.5f);
            selector = selectorInitial + dragOffset() * scrollSpeed;
            if (selector > rocketCount + 1.5f) {
                selector = rocketCount + 1.5f;
            }
            if (selector < -0.5f) {
                selector = -0.5f;
            }
            scrollParent.transform.position = new Vector3((selector - 1f) * -rocketSep, 0);
            moveBG();
            if ((int)(selector + 0.5f) != prevSelected) Util.audioManager.playSelectorClick();
        }
    }

    public void OnEndDrag() {
        if (!Util.wm.gameActive) {
            OnDrag();
            dragging = false;
            sr.horizontalNormalizedPosition = 0.5f;
            setClosestRocket();
            selector = selectedRocket;
        }
    }

    float dragOffset() {
        return (sr.horizontalNormalizedPosition - 0.5f);
    }

    public void setClosestRocket() {
        
        for (int i = 0; i <= rocketCount; i++) {
            if (Mathf.Abs(i - selector) <= 0.5f) {
                selectedRocket = i;
                break;
            }
        }
        if (selector < 0) {
            selectedRocket = 0;
        }
        else if (selector > rocketCount) {
            selectedRocket = rocketCount;
        }
        selectedRocketTarget = (selectedRocket - 1) * -rocketSep;
    }

    public void setRocket() {
        setClosestRocket();
        if (selectedRocket > 0) {
            rocket1.SetActive(true);
            rocket1.transform.localPosition = new Vector3((selectedRocket - 2) * rocketSep, 0, 0);
            rocket1.GetComponent<ShowcaseRocket>().setup(Util.rocketHolder.getRocket(selectedRocket - 1));
        }
        else {
            rocket1.SetActive(false);
        }

        ri = Util.rocketHolder.getRocket(selectedRocket);
        rocket2.transform.localPosition = new Vector3((selectedRocket - 1) * rocketSep, 0, 0);
        rocket2.GetComponent<ShowcaseRocket>().setup(ri);
        rocketName.text = ri.name;
        if (ri.purchased) {
            owned.SetActive(true);
            buyInfo.SetActive(false);
        }
        else {
            owned.SetActive(false);
            buyInfo.SetActive(true);
            cost.text = "" + ri.cost;
        }



        if (selectedRocket < rocketCount) {
            rocket3.SetActive(true);
            rocket3.transform.localPosition = new Vector3(selectedRocket * rocketSep, 0, 0);
            rocket3.GetComponent<ShowcaseRocket>().setup(Util.rocketHolder.getRocket(selectedRocket + 1));
        }
        else {
            rocket3.SetActive(false);
        }
    }

    public void spawnShowcase() {
        rocket1 = Instantiate(showcaseRocketPrefab);
        rocket2 = Instantiate(showcaseRocketPrefab);
        rocket3 = Instantiate(showcaseRocketPrefab);
    }

    public void hidden(bool b) {
        if (b) {
            rocket2.SetActive(false);
        }
        else {
            rocket2.SetActive(true);
        }
    }

    public void moveBG() {
        float offset = scrollParent.transform.position.x / (rocketCount * rocketSep) * 2f;
        bg1.transform.localPosition = new Vector3(offset, 2.28f, 0);
        bg2.transform.localPosition = new Vector3(-offset, 0.951f, 0);
        bg3.transform.localPosition = new Vector3(3.59f + offset, -31.2f, 0);
    }
}
