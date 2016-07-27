using UnityEngine;
using System.Collections;

public class SpriteRandomizer : MonoBehaviour {
    public int spriteCount;
    public Sprite s0;
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
    public Sprite s11;
    public Sprite s12;
    public Sprite s13;
    public Sprite s14;
    public Sprite s15;
    public Sprite s16;
    public Sprite s17;
    public Sprite s18;
    // Use this for initialization
    void Start () {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        switch ((int)Random.Range(0, spriteCount - 0.001f)) {
            case 0:  sr.sprite = s0;  break;
            case 1:  sr.sprite = s1;  break;
            case 2:  sr.sprite = s2;  break;
            case 3:  sr.sprite = s3;  break;
            case 4:  sr.sprite = s4;  break;
            case 5:  sr.sprite = s5;  break;
            case 6:  sr.sprite = s6;  break;
            case 7:  sr.sprite = s7;  break;
            case 8:  sr.sprite = s8;  break;
            case 9:  sr.sprite = s9;  break;
            case 10: sr.sprite = s10; break;
            case 11: sr.sprite = s11; break;
            case 12: sr.sprite = s12; break;
            case 13: sr.sprite = s13; break;
            case 14: sr.sprite = s14; break;
            case 15: sr.sprite = s15; break;
            case 16: sr.sprite = s16; break;
            case 17: sr.sprite = s17; break;
            case 18: sr.sprite = s18; break;
        }
	}
}
