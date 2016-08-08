using UnityEngine;
using System.Collections;

public class CoinPatternHolder : MonoBehaviour {

    public GameObject c0;
    public GameObject c1;
    public GameObject c2;
    public GameObject c3;
    public GameObject c4;
    public GameObject c5;
    public GameObject c6;
    public GameObject c7;
    public GameObject c8;
    public GameObject c9;
    public GameObject c10;
    public GameObject c11;
    public GameObject c12;
    public GameObject c13;
    public GameObject c14;
    public GameObject c15;
    public GameObject c16;
    public GameObject c17;
    public GameObject c18;
    public GameObject c19;
    public GameObject c20;
    public GameObject c21;
    public GameObject c22;
    public GameObject c23;
    public GameObject c24;
    public GameObject c25;
    public GameObject c26;
    public GameObject c27;
    public GameObject c28;
    public GameObject c29;
    public GameObject c30;
    public GameObject c31;
    public GameObject c32;
    public GameObject c33;
    public GameObject c34;
    public GameObject c35;
    public GameObject c36;
    public GameObject c37;
    public GameObject c38;
    public GameObject c39;

    void Awake() {
        Util.coinPatternHolder = this;
    }

    public GameObject getCoinPattern() {
        GameObject selected = null;
        do {
            switch ((int)Random.Range(0, 40.99f)) {
                case 0: selected = c0; break;
                case 1: selected = c1; break;
                case 2: selected = c2; break;
                case 3: selected = c3; break;
                case 4: selected = c4; break;
                case 5: selected = c5; break;
                case 6: selected = c6; break;
                case 7: selected = c7; break;
                case 8: selected = c8; break;
                case 9: selected = c9; break;
                case 10: selected = c10; break;
                case 11: selected = c11; break;
                case 12: selected = c12; break;
                case 13: selected = c13; break;
                case 14: selected = c14; break;
                case 15: selected = c15; break;
                case 16: selected = c16; break;
                case 17: selected = c17; break;
                case 18: selected = c18; break;
                case 19: selected = c19; break;
                case 20: selected = c20; break;
                case 21: selected = c21; break;
                case 22: selected = c22; break;
                case 23: selected = c23; break;
                case 24: selected = c24; break;
                case 25: selected = c25; break;
                case 26: selected = c26; break;
                case 27: selected = c27; break;
                case 28: selected = c28; break;
                case 29: selected = c29; break;
                case 30: selected = c30; break;
                case 31: selected = c31; break;
                case 32: selected = c32; break;
                case 33: selected = c33; break;
                case 34: selected = c34; break;
                case 35: selected = c35; break;
                case 36: selected = c36; break;
                case 37: selected = c37; break;
                case 38: selected = c38; break;
                case 39: selected = c39; break;
                default: selected = null; break;
            }
        }
        while (selected == null);
        return selected;
    }
}
