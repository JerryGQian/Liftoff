using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SaveManager : MonoBehaviour {


    void Awake() {
        Util.saveManager = this;
    }

	// Use this for initialization
	void Start () {
	
	}

    public void load() {
        if (File.Exists(Application.persistentDataPath + "/gamedata.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamedata.dat", FileMode.Open);
            GameData data = null;
            try {
                data = (GameData)bf.Deserialize(file);
            }
            catch (Exception e) {
                Debug.LogError("Failed to load gamedata: " + e.Message);
            }
            file.Close();

            Util.wm.best = data.best;
            Util.wm.coins = data.coins;
            Util.wm.totalDistance = data.totalDistance;
            Util.wm.attempts = data.attempts;
        }

        if (File.Exists(Application.persistentDataPath + "/settings.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/settings.dat", FileMode.Open);
            Settings data = null;
            try {
                data = (Settings)bf.Deserialize(file);
            }
            catch (Exception e) {
                Debug.LogError("Failed to load settings: " + e.Message);
            }
            file.Close();

            Util.wm.muted = data.muted;
            Util.wm.controlScheme = data.controlScheme;
            Util.wm.hasCheated = data.hasCheated;
        }
        Debug.Log("Data loaded");
    }

    public void save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamedata.dat");

        GameData data = new GameData();

        data.best = Util.wm.best;
        data.coins = Util.wm.coins;
        data.totalDistance = Util.wm.totalDistance;
        data.attempts = Util.wm.attempts;

        bf.Serialize(file, data);
        file.Close();


        ////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////


        bf = new BinaryFormatter();
        file = File.Create(Application.persistentDataPath + "/settings.dat");

        Settings data2 = new Settings();

        data2.muted = Util.wm.muted;
        data2.controlScheme = Util.wm.controlScheme;
        data2.hasCheated = Util.wm.hasCheated;

        bf.Serialize(file, data2);
        file.Close();

        Debug.Log("Saved all data successfully");
    }
}

[Serializable]
public class GameData {
    public float best;
    public int attempts;
    public int coins;
    public float totalDistance;
}

[Serializable]
public class Settings {
    public bool muted;
    public int controlScheme;
    public bool hasCheated;
}
