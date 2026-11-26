using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogRenderer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private Image portraitBox;
    [SerializeField] private Sprite[] portraits;
    [SerializeField] private SettingsManager sm;
    [SerializeField] private TextAsset[] langFiles;
    [SerializeField] private GameObject dialogObject;
    [SerializeField] private GameObject nextIndicator;
    private TextLines JsonLines;
    private string textToRender;
    private int currentChar = 0;
    void Start()
    {
        JsonLines = JsonUtility.FromJson<TextLines>(langFiles[sm.language].text);
    }
    void Update()
    {
        
    }
    public void RenderText(int idToRender, bool lastLine)
    {
        if(lastLine)
        {
            nextIndicator.SetActive(false);
        } else
        {
            nextIndicator.SetActive(true);
        }
        textToRender = JsonLines.lines[idToRender].text;
        portraitBox.sprite = portraits[JsonLines.lines[idToRender].portrait];
        dialogText.text = "";
        currentChar = 0;
        InvokeRepeating("RenderChar", 0.1f, JsonLines.lines[idToRender].speed);
    }
    public void ToggleBox(bool toggleValue)
    {
        if(toggleValue)
        {
            dialogObject.SetActive(true);
        } else
        {
            dialogObject.SetActive(false);
        }
    }
    void RenderChar()
    {
        if(currentChar == textToRender.Length)
        {
            CancelInvoke();
        } else
        {
            dialogText.text += textToRender[currentChar];
            currentChar++;
        }
    }
}
[System.Serializable]
public class Line
{
    public string name;
    public string text;
    public int portrait;
    public float speed;
}
[System.Serializable]
public class TextLines
{
    public Line[] lines;
}