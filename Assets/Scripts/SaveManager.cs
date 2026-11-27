using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private PlayerMove pm;
    [SerializeField] private InventoryManager im;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void SaveGame(int currentSave)
    {
        
    }
    public void LoadGame(int currentSave)
    {
        
    }
    public class SaveObject
    {
        public Vector3 playerPosition;
        public List<int> items;
        public int slot1;
        public int slot2;
        public int slot3;
        public int slot4;
    }
}
