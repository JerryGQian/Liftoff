using UnityEngine;
using System.Collections;

public class RocketHolder : MonoBehaviour {
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

    public RocketInfo ri;
    void Awake() {
        Util.rocketHolder = this;
        ri = new RocketInfo(r1, "");
    }
    void Start() {
        
    }

    public RocketInfo getRocket() {
        return getRocket((int)ScrollManager.selector);
    }

    public RocketInfo getRocket(int i) {
        switch (i) {
            case 0: if (Util.wm.alternate) {
                    ri = getRocket((int)Random.Range(1f, ScrollManager.rocketCount + 0.99f));
                    ri.name = "RANDOM";
                    Util.wm.alternate = !Util.wm.alternate;
                    return ri;
                }
                else {
                    return ri;
                }
            case 1:  return new RocketInfo(r1, "ROCKET");
            case 2:  return new RocketInfo(r2, "FALCON 9");
            case 3:  return new RocketInfo(r3, "SOYUZ");
            case 4:  return new RocketInfo(r4, "PROTON");
            case 5:  return new RocketInfo(r5, "ATLAS 5");
            case 6:  return new RocketInfo(r6, "ARIANE 5");
            case 7:  return new RocketInfo(r7, "SPACE SHUTTLE");
            case 8:  return new RocketInfo(r8, "V2");
            case 9:  return new RocketInfo(r9, "SATURN V");
            case 10: return new RocketInfo(r10,"N1");
            case 11: return new RocketInfo(r11,"");
            case 12: return new RocketInfo(r12,"");
            case 13: return new RocketInfo(r13,"");
            case 14: return new RocketInfo(r14,"");
            case 15: return new RocketInfo(r15,"");
            case 16: return new RocketInfo(r16,"");
            case 17: return new RocketInfo(r17,"");
            case 18: return new RocketInfo(r18,"");
            case 19: return new RocketInfo(r19,"");
            case 20: return new RocketInfo(r20,"");
            case 21: return new RocketInfo(r21,"");
            case 22: return new RocketInfo(r22,"");
            case 23: return new RocketInfo(r23,"");
            case 24: return new RocketInfo(r24,"");
            case 25: return new RocketInfo(r25,"");
        }
        return null;
    }
}

public class RocketInfo {
    public Sprite sprite;
    public string name;

    public RocketInfo(Sprite spr, string n) {
        sprite = spr;
        name = n;
    }
}
