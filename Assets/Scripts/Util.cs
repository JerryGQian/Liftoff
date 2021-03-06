﻿using UnityEngine;
using System.Collections;
using System;

public class Util {

    public static WorldManager wm;
    public static InputManager im;
    public static CameraManager cm;
    public static GameManager gm;
    public static SaveManager saveManager;
    public static MenuManager menuManager;
    public static GameObject canvas;
    public static ScrollManager scrollManager;
    public static RocketHolder rocketHolder;
    public static ObstacleHolder obstacleHolder;
    public static CoinPatternHolder coinPatternHolder;
    public static AchievementManager achievementManager;
    public static AudioManager audioManager;

    public static Nozzle nozzle;
    public static Wind wind;
    public static Rocket rocket;
    public static RectTransform coin;

    public static float width;

    public static float adCoinsCooldown = 600f;
    public static float adLifeCooldown = 180f;
    public static int adLifeMinGames = 4;
    public static float adLifeMinDist = 200f;
    public static float adLifeProbability = 40f;
    public static int coinReward = 350;

    public static int dailyReward = 500;

    public static bool even;
    public static bool even2;
    public static bool even3;
    public static bool even10 = true;

    public static string encodeNumberInteger(int num) {
        return string.Format("{0:0}", num);
    }

    public static string encodeTime(float time) {
        TimeSpan t = TimeSpan.FromSeconds(time);

        string text = "";
        if (t.Days > 0) {
            text += string.Format("{0:0}Days ", t.Days);
        }
        if (t.Hours > 0) {
            text += string.Format("{0:D2}Hrs ", t.Hours);
        }
        text += string.Format("{0:D2}M {1:D2}s ", t.Minutes, t.Seconds);
        return text;
    }

    public static string encodeTimeShort(float time) {
        TimeSpan t = TimeSpan.FromSeconds(time);

        string text = "";
        if (t.Days > 0) {
            text += string.Format("{0:0}D ", t.Days);
        }
        if (t.Hours > 0) {
            text += string.Format("{0:D1}H ", t.Hours);
        }
        text += string.Format("{0:D1}M", t.Minutes);
        if (t.Hours == 0 && t.Days == 0) {
            text += string.Format("{0:D1}S", t.Seconds);
        }
        return text;
    }

    public static string encodeTimeColon(float time) {
        TimeSpan t = TimeSpan.FromSeconds(time);

        string text = "";
        if (t.Hours > 0) {
            text += string.Format("{0:D1}:", t.Hours);
        }
        text += string.Format("{0:D2}:", t.Minutes);
        if (t.Hours == 0 && t.Days == 0) {
            text += string.Format("{0:D2}", t.Seconds);
        }
        return text;
    }
}
