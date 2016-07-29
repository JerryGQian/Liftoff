using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScrollManager : MonoBehaviour {
    ScrollRect sr;
    public static float selector = 1;
    public float selectorVis;
    public static int selectedRocket = 1;
    public static float selectedRocketTarget;
    float selectorInitial = 0;
    bool dragging;
    public static float scrollSpeed = 8f;
    public static float juice = 7f;
    public static float rocketSep = 4.5f;
    public static int rocketCount = 10;

    public GameObject showcaseRocketPrefab;
    public GameObject rocket1;
    public GameObject rocket2;
    public GameObject rocket3;

    public GameObject scrollParent;

    void Awake() {
        Util.scrollManager = this;
    }

	// Use this for initialization
	void Start () {
        sr = GetComponent<ScrollRect>();
        if (Application.platform != RuntimePlatform.WindowsEditor) {
            Destroy(sr.horizontalScrollbar);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!dragging && !Util.wm.gameActive) {
            scrollParent.transform.position = scrollParent.transform.position + new Vector3((selectedRocketTarget - scrollParent.transform.position.x) * Time.deltaTime * juice, 0, 0);
        }
        if (Util.even) {
            setRocket();
        }
	}

    public void OnBeginDrag() {
        if (!Util.wm.gameActive) {
            dragging = true;
            selectorInitial = selector;
        }
    }

    public void OnDrag() {
        if (!Util.wm.gameActive) {
            selector = selectorInitial + dragOffset() * scrollSpeed;
            if (selector > rocketCount + 1.5f) {
                selector = rocketCount + 1.5f;
            }
            if (selector < -0.5f) {
                selector = -0.5f;
            }
            scrollParent.transform.position = new Vector3((selector - 1f) * -rocketSep, 0);
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

    void setClosestRocket() {
        
        for (int i = 0; i <= rocketCount; i++) {
            if (Mathf.Abs(i - selector) < 0.5f) {
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

        rocket2.transform.localPosition = new Vector3((selectedRocket - 1) * rocketSep, 0, 0);
        rocket2.GetComponent<ShowcaseRocket>().setup(Util.rocketHolder.getRocket(selectedRocket));

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
}
