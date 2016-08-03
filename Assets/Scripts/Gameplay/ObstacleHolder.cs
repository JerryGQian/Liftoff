using UnityEngine;
using System.Collections;

public class ObstacleHolder : MonoBehaviour {
    int meteorCount = 11;
    int satelliteCount = 7;
    int asteroidCount = 8;
    public Sprite m1;
    public Sprite m2;
    public Sprite m3;
    public Sprite m4;
    public Sprite m5;
    public Sprite m6;
    public Sprite m7;
    public Sprite m8;
    public Sprite m9;
    public Sprite m10;
    public Sprite m11;
    public Sprite m12;

    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    public Sprite s4;
    public Sprite s5;
    public Sprite s6;
    public Sprite s7;
    public Sprite s8;
    public Sprite s9;
    public Sprite s10;

    public Sprite a1;
    public Sprite a2;
    public Sprite a3;
    public Sprite a4;
    public Sprite a5;
    public Sprite a6;
    public Sprite a7;
    public Sprite a8;
    public Sprite a9;
    public Sprite a10;
    public Sprite a11;

    /*public Sprite r1;
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
    public Sprite r25;*/

    public RocketInfo ri;
    void Awake() {
        Util.obstacleHolder = this;
    }

    public Sprite getMeteor(int i) {
        switch (i) {
            case 0: return getMeteor((int)Random.Range(1f, meteorCount + 0.99f));
            case 1: return m1;
            case 2: return m2;
            case 3: return m3;
            case 4: return m4;
            case 5: return m5;
            case 6: return m6;
            case 7: return m7;
            case 8: return m8;
            case 9: return m9;
            case 10: return m10;
            case 11: return m11;
            case 12: return m12;
            /*case 13: return new RocketInfo(r13, "");
            case 14: return new RocketInfo(r14, "");
            case 15: return new RocketInfo(r15, "");
            case 16: return new RocketInfo(r16, "");
            case 17: return new RocketInfo(r17, "");
            case 18: return new RocketInfo(r18, "");
            case 19: return new RocketInfo(r19, "");
            case 20: return new RocketInfo(r20, "");*/
        }
        return null;
    }

    public Sprite getSatellite(int i) {
        switch (i) {
            case 0: return getSatellite((int)Random.Range(1f, satelliteCount + 0.99f));
            case 1: return s1;
            case 2: return s2;
            case 3: return s3;
            case 4: return s4;
            case 5: return s5;
            case 6: return s6;
            case 7: return s7;
            case 8: return s8;
            case 9: return s9;
            case 10: return s10;
            /*case 11: return new RocketInfo(r11, "");
            case 12: return new RocketInfo(r12, "");
            case 13: return new RocketInfo(r13, "");
            case 14: return new RocketInfo(r14, "");
            case 15: return new RocketInfo(r15, "");
            case 16: return new RocketInfo(r16, "");
            case 17: return new RocketInfo(r17, "");
            case 18: return new RocketInfo(r18, "");
            case 19: return new RocketInfo(r19, "");
            case 20: return new RocketInfo(r20, "");*/
        }
        return null;
    }

    public Sprite getAsteroid(int i) {
        switch (i) {
            case 0: return getAsteroid((int)Random.Range(1f, asteroidCount + 0.99f));
            case 1: return a1;
            case 2: return a2;
            case 3: return a3;
            case 4: return a4;
            case 5: return a5;
            case 6: return a6;
            case 7: return a7;
            case 8: return a8;
            case 9: return a9;
            case 10: return a10;
            case 11: return a11;
                /*case 12: return new RocketInfo(r12, "");
                case 13: return new RocketInfo(r13, "");
                case 14: return new RocketInfo(r14, "");
                case 15: return new RocketInfo(r15, "");
                case 16: return new RocketInfo(r16, "");
                case 17: return new RocketInfo(r17, "");
                case 18: return new RocketInfo(r18, "");
                case 19: return new RocketInfo(r19, "");
                case 20: return new RocketInfo(r20, "");*/
        }
        return null;
    }
}

