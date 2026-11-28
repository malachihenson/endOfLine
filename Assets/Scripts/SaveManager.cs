using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private PlayerMove pm;
    [SerializeField] private InventoryManager im;
    private int thisSave = 0;
    void Start()
    {
        LoadGame(0);
        InvokeRepeating("AutoSave", 120, 120);
    }
    void Update()
    {
        
    }
    public void SaveGame(int currentSave)
    {
        string json = JsonUtility.ToJson(new SaveObject
        {
            fileTime = 0,
            playerPosition = pm.gameObject.transform.position,
            areaID = pm.currentArea,
            items = im.items,
            slot1 = im.slot1,
            slot2 = im.slot2,
            slot3 = im.slot3,
            slot4 = im.slot4,
        });
        File.WriteAllText(Application.persistentDataPath + "/save" + (currentSave + 1).ToString() + ".save", json);
        Debug.Log("Game saved!");
    }
    public void LoadGame(int currentSave)
    {
        thisSave = currentSave;
        string json = File.ReadAllText(Application.persistentDataPath + "/save" + (currentSave + 1).ToString() + ".save");
        SaveObject saveObject = JsonUtility.FromJson<SaveObject>(json);
        pm.gameObject.transform.position = saveObject.playerPosition;
        pm.currentArea = saveObject.areaID;
        im.items = saveObject.items;
        im.slot1 = saveObject.slot1;
        im.slot2 = saveObject.slot2;
        im.slot3 = saveObject.slot3;
        im.slot4 = saveObject.slot4;
    }
    public void AutoSave()
    {
        SaveGame(thisSave);
    }
    public class SaveObject
    {
        public int fileTime;
        public Vector3 playerPosition;
        public int areaID;
        public List<int> items;
        public int slot1;
        public int slot2;
        public int slot3;
        public int slot4;
    }
}
