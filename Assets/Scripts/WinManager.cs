using UnityEngine;
using TMPro;
using System;
using System.Collections;
public class WinManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statText;
    [SerializeField] private BattleManager bm;
    [SerializeField] private GameObject winButton;
    private string textToDisplay;
    private string itemString;
    private int minutes;
    private string seconds;
    private int count = 0;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void StartWin()
    {
        winButton.SetActive(false);
        count = 0;
        //generate item string here later
        statText.text = "";
        minutes = (int)Math.Floor(bm.fullBattleTime / 60);
        seconds = Math.Round(bm.fullBattleTime - (minutes * 60)).ToString("00");
        textToDisplay = bm.enemyName + " was defeated!\n" + "Time: " + minutes.ToString() + ":" + seconds + "\nTokens Earned: " + bm.tokensEarned.ToString() + "\nChallenges Solved: " + bm.challengesSolved.ToString() + itemString;
        InvokeRepeating("RenderChar", 0.02f, 0.02f);
    }
    private void RenderChar()
    {
        if(count < textToDisplay.Length)
        {
            statText.text += textToDisplay[count];
            count++;
        } else
        {
            CancelInvoke();
            winButton.SetActive(true);
        }
    }
    
}
