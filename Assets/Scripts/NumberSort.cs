using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class NumberSort : MonoBehaviour
{
    [SerializeField] private BattleManager bm;
    [SerializeField] private TextMeshProUGUI[] buttonLabels;
    [SerializeField] private ButtonManager[] levelManagers;
    [SerializeField] private GameObject[] layouts;
    private int difficulty;
    private int order = 0;
    private List<int> numbers = new List<int>();
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void GenerateChallenge()
    {
        levelManagers[0].ResetAllButtons();
        levelManagers[1].ResetAllButtons();
        levelManagers[2].ResetAllButtons();
        numbers.Clear();
        order = 0;
        difficulty = bm.tokenDifficulty;
        switch(difficulty)
        {
            case 0:
                layouts[0].SetActive(true);
                layouts[1].SetActive(false);
                layouts[2].SetActive(false);
                GetUniqueNumbers(4);
                for(int i=0; i<4; i++)
                {
                    buttonLabels[i].text = numbers[i].ToString();
                }
                break;
            case 1:
                layouts[0].SetActive(false);
                layouts[1].SetActive(true);
                layouts[2].SetActive(false);
                GetUniqueNumbers(6);
                for(int i=4; i<10; i++)
                {
                    buttonLabels[i].text = numbers[i - 4].ToString();
                }
                break;
            case 2:
                layouts[0].SetActive(false);
                layouts[1].SetActive(false);
                layouts[2].SetActive(true);
                GetUniqueNumbers(8);
                for(int i=10; i<18; i++)
                {
                    buttonLabels[i].text = numbers[i - 10].ToString();
                }
                break;
        }
    }
    public void SubmitValue(int id)
    {
        if(order == numbers[id] - 1)
        {
            if(order == numbers.Count - 1)
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
    //I learned something new today! This is a Fisher-Yates shuffle :o
    private void GetUniqueNumbers(int range)
    {
        for(int i=0; i<range; i++)
        {
            numbers.Add(i + 1);
        }
        for(int i=0; i<numbers.Count; i++)
        {
            int temp = numbers[i];
            int randomIndex = Random.Range(i, numbers.Count);
            numbers[i] = numbers[randomIndex];
            numbers[randomIndex] = temp;
        }
    }
}
