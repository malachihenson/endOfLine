using UnityEngine;
using UnityEngine.UI;
public class SpotTheDifference : MonoBehaviour
{
    [SerializeField] private BattleManager bm;
    [SerializeField] private Image[] colorButtons;
    [SerializeField] private GameObject[] layouts;
    private Color normalColor;
    private Color diffColor;
    private int difficulty;
    private int diffId;
    private int trueDiffId;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void GenerateChallenge()
    {
        difficulty = bm.tokenDifficulty;
        normalColor = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f));
        switch(difficulty)
        {
            case 0:
                layouts[0].SetActive(true);
                layouts[1].SetActive(false);
                layouts[2].SetActive(false);
                diffId = Random.Range(0, 4);
                trueDiffId = diffId;
                diffColor = normalColor + new Color(0.15f, 0.15f, 0.15f);
                for(int i=0; i<4; i++)
                {
                    if(i == diffId)
                    {
                        colorButtons[i].color = diffColor;
                    } else
                    {
                        colorButtons[i].color = normalColor;
                    }
                }
                break;
            case 1:
                layouts[0].SetActive(false);
                layouts[1].SetActive(true);
                layouts[2].SetActive(false);
                diffId = Random.Range(4, 13);
                trueDiffId = diffId - 4;
                diffColor = normalColor + new Color(0.1f, 0.1f, 0.1f);
                for(int i=4; i<13; i++)
                {
                    if(i == diffId)
                    {
                        colorButtons[i].color = diffColor;
                    } else
                    {
                        colorButtons[i].color = normalColor;
                    }
                }
                break;
            case 2:
                layouts[0].SetActive(false);
                layouts[1].SetActive(false);
                layouts[2].SetActive(true);
                diffId = Random.Range(13, 29);
                trueDiffId = diffId - 13;
                diffColor = normalColor + new Color(0.07f, 0.07f, 0.07f);
                for(int i=13; i<29; i++)
                {
                    if(i == diffId)
                    {
                        colorButtons[i].color = diffColor;
                    } else
                    {
                        colorButtons[i].color = normalColor;
                    }
                }
                break;
        }
    }
    public void AnswerQuestion(int id)
    {
        if(trueDiffId == id)
        {
            bm.WinChallenge();
        } else
        {
            bm.FailChallenge();
            GenerateChallenge();
        }
    }
}
