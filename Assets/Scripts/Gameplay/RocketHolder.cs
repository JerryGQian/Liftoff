using UnityEngine;
using System.Collections;

public enum CrayonColor { one, two, three, four }

public class RocketHolder : MonoBehaviour {
    public bool[] purchased;

    public int rocketCount;

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
    public Sprite r32;
    public Sprite r33;
    public Sprite r34;
    public Sprite r35;
    public Sprite r36;
    public Sprite r37;
    public Sprite r38;
    public Sprite r39;
    public Sprite r40;
    public Sprite r41;
    public Sprite r42;
    public Sprite r43;
    public Sprite r44;
    public Sprite r45;
    public Sprite r46;
    public Sprite r47;
    public Sprite r48;
    public Sprite r49;
    public Sprite r50;
    public Sprite r51;
    public Sprite r52;
    public Sprite r53;
    public Sprite r54;
    public Sprite r55;
    public Sprite r56;
    public Sprite r57;
    public Sprite r58;
    public Sprite r59;

    public Sprite crayon1;
    public Sprite crayon2;
    public Sprite crayon3;
    public Sprite crayon4;

    public CrayonColor crayonColor;

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

            // RocketInfo(Sprite spr, bool p, string n, int c, bool noz, bool fire)
            case 1:  return new RocketInfo(r1,  purchased[i], r1.name, 0); //rocket
            case 2:  return new RocketInfo(r2,  purchased[i], r2.name, 100); //f9
            case 3:  return new RocketInfo(r3,  purchased[i], r3.name, 550); //shuttle
            case 4:  return new RocketInfo(r4,  purchased[i], r4.name, 200); //soyuz
            case 5:  return new RocketInfo(r5,  purchased[i], r5.name, 270); //proton
            case 6:  return new RocketInfo(r6,  purchased[i], r6.name, 500); //SaturnV
            case 7:  return new RocketInfo(r7,  purchased[i], r7.name, 270); //atlas 5
            case 8:  return new RocketInfo(r8,  purchased[i], r8.name, 400); //ariane 4
            case 9:  return new RocketInfo(r9,  purchased[i], r9.name, 400); //ariane 5
            case 10: return new RocketInfo(r10, purchased[i], r10.name, 600); //titan
            case 11: return new RocketInfo(r11, purchased[i], r11.name, 400); //n1
            case 12: return new RocketInfo(r12, purchased[i], r12.name, 650); //delta IV heavy
            case 13: return new RocketInfo(r13, purchased[i], r13.name, 450); //v2
            case 14: return new RocketInfo(r14, purchased[i], r14.name, 700); //SLS
            case 15: return new RocketInfo(r15, purchased[i], r15.name, 1000); //Apollo
            case 16: return new RocketInfo(r16, purchased[i], r16.name, 500); //tomahawk
            case 17: return new RocketInfo(r17, purchased[i], r17.name, 450, false, FlameType.flame, SoundType.jet); //fighter jet
            case 18: return new RocketInfo(r18, purchased[i], r18.name, 500, false, FlameType.flame, SoundType.jet); //Blackbird
            case 19: return new RocketInfo(r19, purchased[i], r19.name, 500, false, FlameType.smoke, SoundType.jet); //Concorde
            case 20: return new RocketInfo(r20, purchased[i], r20.name, 1500, false, FlameType.none, SoundType.car); //Lambo
            case 21: return new RocketInfo(r21, purchased[i], r21.name, 1200, true, FlameType.scifi, SoundType.car); //magic school bus
            case 22: return new RocketInfo(r22, purchased[i], r22.name, 1100, false, FlameType.none, SoundType.car); //chitty
            case 23: return new RocketInfo(r23, purchased[i], r23.name, 1500, false, FlameType.none, SoundType.car); //F1
            case 24: return new RocketInfo(r24, purchased[i], r24.name, 1000, false, FlameType.none, SoundType.none); //rocketpop
            case 25: return new RocketInfo(r25, purchased[i], r25.name, 950, false, FlameType.none, SoundType.none); //rocket n roll
            case 26: return new RocketInfo(r26, purchased[i], r26.name, 800); //keyrocket
            case 27: return new RocketInfo(r27, purchased[i], r27.name, 1100, false, FlameType.none, SoundType.none); //hot docket
            case 28: return new RocketInfo(r28, purchased[i], r28.name, 650, false, FlameType.none, SoundType.none); //paper
            case 29: return new RocketInfo(r29, purchased[i], r29.name, 1200, false, FlameType.none, SoundType.none); //myphone
            case 30: return getCrayon(i); //crayon
            case 31: return new RocketInfo(r31, purchased[i], r31.name, 300, false, FlameType.scifi, SoundType.none); //bottle
            case 32: return new RocketInfo(r32, purchased[i], r32.name, 600, false, FlameType.bullet, SoundType.gun); ///machine gun
            case 33: return new RocketInfo(r33, purchased[i], r33.name, 1000); //empire state
            case 34: return new RocketInfo(r34, purchased[i], r34.name, 800); //Eiffel rocket
            case 35: return new RocketInfo(r35, purchased[i], r35.name, 1300); //rocket khalifa
            case 36: return new RocketInfo(r36, purchased[i], r36.name, 1000); //Oriental Pearl
            case 37: return new RocketInfo(r37, purchased[i], r37.name, 1000); //Rocket Ben
            case 38: return new RocketInfo(r38, purchased[i], r38.name, 1000, false, FlameType.flame, SoundType.rocket); //raygun gothic
            case 39: return new RocketInfo(r39, purchased[i], r39.name, 1200); //THE MARTIAN
            case 40: return new RocketInfo(r40, purchased[i], r40.name, 1500, false, FlameType.none, SoundType.none); //mary poppins
            case 41: return new RocketInfo(r41, purchased[i], r41.name, 1750, false, FlameType.none, SoundType.none); //UP
            case 42: return new RocketInfo(r42, purchased[i], r42.name, 1100, false, FlameType.smoke, SoundType.rocket);  //steel man
            case 43: return new RocketInfo(r43, purchased[i], r43.name, 1000); //apollo27
            case 44: return new RocketInfo(r44, purchased[i], r44.name, 1750, false, FlameType.scifi, SoundType.rocket); //pelican
            case 45: return new RocketInfo(r45, purchased[i], r45.name, 1800, false, FlameType.scifi, SoundType.rocket); //covenant carrier
            case 46: return new RocketInfo(r46, purchased[i], r46.name, 1500, false, FlameType.scifi, SoundType.none); //hogwarts
            case 47: return new RocketInfo(r47, purchased[i], r47.name, 1800, false, FlameType.none, SoundType.jet); //Cruise
            case 48: return new RocketInfo(r48, purchased[i], r48.name, 1500, false, FlameType.none, SoundType.none); //TITANIC
            case 49: return new RocketInfo(r49, purchased[i], r49.name, 1200, false, FlameType.none, SoundType.drone); //drone
            case 50: return new RocketInfo(r50, purchased[i], r50.name, 400, false, FlameType.scifi, SoundType.rocket); //star destroyer
            case 51: return new RocketInfo(r51, purchased[i], r51.name, 0);
            case 52: return new RocketInfo(r52, purchased[i], r52.name, 0);
            case 53: return new RocketInfo(r53, purchased[i], r53.name, 0);
            case 54: return new RocketInfo(r54, purchased[i], r54.name, 0);
            case 55: return new RocketInfo(r55, purchased[i], r55.name, 0);
            case 56: return new RocketInfo(r56, purchased[i], r56.name, 0);
            case 57: return new RocketInfo(r57, purchased[i], r57.name, 0);
            case 58: return new RocketInfo(r58, purchased[i], r58.name, 0);
            case 59: return new RocketInfo(r59, purchased[i], r59.name, 0);
                //RocketInfo(Sprite spr, bool p, string n, int c, bool noz, bool f)
        }
        return null;
    }

    public RocketInfo getCrayon(int i) {
        switch (crayonColor) {
            case CrayonColor.one: return new RocketInfo(crayon1, purchased[i], crayon1.name, 650, false, FlameType.none, SoundType.none);
            case CrayonColor.two: return new RocketInfo(crayon2, purchased[i], crayon2.name, 650, false, FlameType.none, SoundType.none);
            case CrayonColor.three: return new RocketInfo(crayon3, purchased[i], crayon3.name, 650, false, FlameType.none, SoundType.none);
            case CrayonColor.four: return new RocketInfo(crayon4, purchased[i], crayon4.name, 650, false, FlameType.none, SoundType.none);
        }
        return null;
    }
}

public class RocketInfo {
    public Sprite sprite;
    public string name;
    public bool nozzle;
    public FlameType fire;
    public int cost;
    public bool purchased;
    public SoundType sound;
    

    public RocketInfo(Sprite spr, bool p, string n, int c) {
        setup(spr, p, n, c, true, FlameType.flame, SoundType.rocket);
    }

    public RocketInfo(Sprite spr, bool p, string n, int c, bool noz, FlameType f, SoundType sou) {
        setup(spr, p, n, c, noz, f, sou);
    }



    public void setup(Sprite spr, bool p, string n, int c, bool noz, FlameType f, SoundType sou) {
        sprite = spr;
        purchased = p;
        name = n;
        cost = c;
        nozzle = noz;
        fire = f;
        sound = sou;
    }
}
