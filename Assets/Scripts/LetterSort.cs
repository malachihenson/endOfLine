using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
public class LetterSort : MonoBehaviour
{
    [SerializeField] private BattleManager bm;
    [SerializeField] private TextMeshProUGUI[] buttonLabels;
    [SerializeField] private ButtonManager[] levelManagers;
    [SerializeField] private GameObject[] layouts;
    private int difficulty;
    private int order = 0;
    private List<char> letters = new List<char>();
    private char[] allLetters = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void GenerateChallenge()
    {
        letters.Clear();
        order = 0;
        difficulty = bm.tokenDifficulty;
        switch(difficulty)
        {
            case 0:
                layouts[0].SetActive(true);
                layouts[1].SetActive(false);
                layouts[2].SetActive(false);
                GetUniqueLetters(4);
                for(int i=0; i<4; i++)
                {
                    buttonLabels[i].text = letters[i].ToString();
                }
                break;
            case 1:
                layouts[0].SetActive(false);
                layouts[1].SetActive(true);
                layouts[2].SetActive(false);
                GetUniqueLetters(6);
                for(int i=4; i<10; i++)
                {
                    buttonLabels[i].text = letters[i - 4].ToString();
                }
                break;
            case 2:
                layouts[0].SetActive(false);
                layouts[1].SetActive(false);
                layouts[2].SetActive(true);
                GetUniqueLetters(8);
                for(int i=10; i<18; i++)
                {
                    buttonLabels[i].text = letters[i - 10].ToString();
                }
                break;
        }
    }
    public void SubmitValue(int id)
    {
        if(order == Array.IndexOf(allLetters, letters[id]))
        {
            if(order == letters.Count - 1)
            {
                levelManagers[difficulty].ResetAllButtons();
                bm.WinChallenge();
            } else
            {
                order++;
            }
        } else
        {
            bm.FailChallenge();
            levelManagers[difficulty].ResetAllButtons();
            GenerateChallenge();
        }
    }
    private void GetUniqueLetters(int range)
    {
        for(int i=0; i<range; i++)
        {
            letters.Add(allLetters[i]);
        }
        for(int i=0; i<letters.Count; i++)
        {
            char temp = letters[i];
            int randomIndex = UnityEngine.Random.Range(i, letters.Count);
            letters[i] = letters[randomIndex];
            letters[randomIndex] = temp;
        }
    }
}
