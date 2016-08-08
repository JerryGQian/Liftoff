using UnityEngine;
using System.Collections;

public class RocketHolder : MonoBehaviour {
    public bool[] purchased;

    public Sprite r1;
    public Sprite r2;
    public Sprite r3;
    public Sprite r4;
    public Sprite r5;
    public Sprite r6;
    public Sprite r7;
    public Sprite r8;
    public Sprite r9;
    public Sprite r10;
    public Sprite r11;
    public Sprite r12;
    public Sprite r13;
    public Sprite r14;
    public Sprite r15;
    public Sprite r16;
    public Sprite r17;
    public Sprite r18;
    public Sprite r19;
    public Sprite r20;
    public Sprite r21;
    public Sprite r22;
    public Sprite r23;
    public Sprite r24;
    public Sprite r25;
    public Sprite r26;
    public Sprite r27;
    public Sprite r28;
    public Sprite r29;
    public Sprite r30;
    public Sprite r31;

    public RocketInfo ri;
    void Awake() {
        Util.rocketHolder = this;
        ri = new RocketInfo(r1, true, "RANDOM", 0);
        purchased = new bool[400];
        purchased[1] = true;
    }
    void Start() {
        
    }

    public RocketInfo getRocket() {
        return getRocket((int)ScrollManager.selector);
    }

    public RocketInfo getRocket(int i) {
        switch (i) {
            case 0: if (Util.wm.alternate) {
                    string oldName = ri.name;
                    do {
                        ri = getRocket((int)Random.Range(1f, ScrollManager.rocketCount + 0.99f));
                    }
                    while (!ri.purchased || oldName.Equals(ri.name));
                    ri.name = "RANDOM";
                    Util.wm.alternate = !Util.wm.alternate;
                    return ri;
                }
                else {
                    return ri;
                }
            case 1:  return new RocketInfo(r1,  purchased[i], "ROCKET", 0);
            case 2:  return new RocketInfo(r2,  purchased[i], "FALCON 9", 50);
            case 3:  return new RocketInfo(r3,  purchased[i], "SOYUZ", 50);
            case 4:  return new RocketInfo(r4,  purchased[i], "PROTON", 100);
            case 5:  return new RocketInfo(r5,  purchased[i], "ATLAS 5", 100);
            case 6:  return new RocketInfo(r6,  purchased[i], "ARIANE 5", 100);
            case 7:  return new RocketInfo(r7,  purchased[i], "SPACE SHUTTLE", 150);
            case 8:  return new RocketInfo(r8,  purchased[i], "V2", 175);
            case 9:  return new RocketInfo(r9,  purchased[i], "SATURN V", 200);
            case 10: return new RocketInfo(r10, purchased[i], "N1", 200);
            case 11: return new RocketInfo(r11, purchased[i], "BOTTLE ROCKET", 150, false, true);
            case 12: return new RocketInfo(r12, purchased[i], "PAPER AIRPLANE", 200, false, false);
            case 13: return new RocketInfo(r13, purchased[i], "MACHINE GUN", 200, false, true);
            case 14: return new RocketInfo(r14, purchased[i], "STEEL MAN", 250, false, true);
            case 15: return new RocketInfo(r15, purchased[i], "", 0);
            case 16: return new RocketInfo(r16, purchased[i], "", 0);
            case 17: return new RocketInfo(r17, purchased[i], "", 0);
            case 18: return new RocketInfo(r18, purchased[i], "", 0);
            case 19: return new RocketInfo(r19, purchased[i], "", 0);
            case 20: return new RocketInfo(r20, purchased[i], "", 0);
            case 21: return new RocketInfo(r21, purchased[i], "", 0);
            case 22: return new RocketInfo(r22, purchased[i], "", 0);
            case 23: return new RocketInfo(r23, purchased[i], "", 0);
            case 24: return new RocketInfo(r24, purchased[i], "", 0);
            case 25: return new RocketInfo(r25, purchased[i], "", 0);
            case 26: return new RocketInfo(r26, purchased[i], "", 0);
            case 27: return new RocketInfo(r27, purchased[i], "", 0);
            case 28: return new RocketInfo(r28, purchased[i], "", 0);
            case 29: return new RocketInfo(r29, purchased[i], "", 0);
            case 30: return new RocketInfo(r30, purchased[i], "", 0);
            case 31: return new RocketInfo(r31, purchased[i], "", 0);
        }
        return null;
    }
}

public class RocketInfo {
    public Sprite sprite;
    public string name;
    public bool nozzle;
    public bool fire;
    public int cost;
    public bool purchased;
    

    public RocketInfo(Sprite spr, bool p, string n, int c) {
        setup(spr, p, n, c, true, true);
    }

    public RocketInfo(Sprite spr, bool p, string n, int c, bool noz, bool f) {
        setup(spr, p, n, c, noz, f);
    }



    public void setup(Sprite spr, bool p, string n, int c, bool noz, bool f) {
        sprite = spr;
        purchased = p;
        name = n;
        cost = c;
        nozzle = noz;
        fire = f;
    }
}
