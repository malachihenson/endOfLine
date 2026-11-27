using System;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject attrib;
    [SerializeField] private GameObject main;
    private int introStage = 0;
    void Start()
    {
        attrib.SetActive(true);
        main.SetActive(false);
    }
    void Update()
    {
        if(Time.time >= 6.0f && introStage == 0)
        {
            introStage++;
            main.SetActive(true);
        }
    }
    public void LoadSave()
    {
        
    }
    public void NewGame()
    {
        
    }
    public void OpenSettings()
    {
        
    }
}
