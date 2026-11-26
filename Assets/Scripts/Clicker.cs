using UnityEngine;
using TMPro;
public class Clicker : MonoBehaviour
{
    [SerializeField] private BattleManager bm;
    [SerializeField] private TextMeshProUGUI counter;
    int count;
    int requiredCount;
    int difficulty;
    public void GenerateChallenge()
    {
        count = 0;
        difficulty = bm.tokenDifficulty;
        switch(difficulty)
        {
            case 0:
                requiredCount = 5;
                break;
            case 1:
                requiredCount = 15;
                break;
            case 2:
                requiredCount = 30;
                break;
        }
        counter.text = count.ToString() + "/" + requiredCount.ToString();
    }
    public void ClickButton()
    {
        count++;
        counter.text = count.ToString() + "/" + requiredCount.ToString();
        if(count == requiredCount)
        {
            bm.WinChallenge();
        }
    }
}
