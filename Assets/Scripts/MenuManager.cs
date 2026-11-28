using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject attrib;
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject loader;
    [SerializeField] private GameObject warning;
    [SerializeField] private Animator mainAnim;
    private int introStage = 0;
    void Start()
    {
        attrib.SetActive(true);
        main.SetActive(false);
        loader.SetActive(false);
        warning.SetActive(false);
    }
    void Update()
    {
        if(Time.time >= 7.0f && introStage == 0)
        {
            introStage++;
            main.SetActive(true);
        }
    }
    public void LoadSave()
    {
        if(File.Exists(Application.persistentDataPath + "/save1.save"))
        {
            StartCoroutine(LoadAnim());
        }
    }
    public void NewGame()
    {
        string json = JsonUtility.ToJson(new SaveObject
        {
            fileTime = 0,
            playerPosition = new Vector3(0, 0, 2),
            areaID = 0,
            items = new List<int>(),
            slot1 = -1,
            slot2 = -1,
            slot3 = -1,
            slot4 = -1
        });
        File.WriteAllText(Application.persistentDataPath + "/save1.save", json);
        StartCoroutine(NewAnim());
    }
    public void OpenSettings()
    {
        
    }
    IEnumerator LoadAnim()
    {
        mainAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1);
        main.SetActive(false);
        loader.SetActive(true);
        SceneManager.LoadScene("MainGame");
    }
    IEnumerator NewAnim()
    {
        mainAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(3);
        main.SetActive(false);
        warning.SetActive(true);
        yield return new WaitForSeconds(6);
        warning.SetActive(false);
        loader.SetActive(true);
        SceneManager.LoadScene("MainGame");
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
