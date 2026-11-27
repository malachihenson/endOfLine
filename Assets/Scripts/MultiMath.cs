using UnityEngine;
using TMPro;
public class MultiMath : MonoBehaviour
{
    [SerializeField] private BattleManager bm;
    [SerializeField] private ButtonManager btm;
    [SerializeField] private TextMeshProUGUI question;
    [SerializeField] private TextMeshProUGUI[] answers;
    private int num1;
    private int num2;
    private int oper;
    private int answer1;
    private int answer2;
    private int answer3;
    private int answer4;
    private int correctAnswer;
    private int correctAnswerNum;
    private int difficulty;
    private char operChar;
    public void GenerateChallenge()
    {
        btm.ResetAllButtons();
        difficulty = bm.tokenDifficulty;
        switch(difficulty)
        {
            case 0:
                num1 = Random.Range(0, 10);
                num2 = Random.Range(0, 10);
                oper = Random.Range(0, 2);
                break;
            case 1:
                num1 = Random.Range(0, 100);
                num2 = Random.Range(0, 100);
                oper = Random.Range(0, 2);
                break;
            case 2:
                num1 = Random.Range(0, 32);
                num2 = Random.Range(0, 32);
                oper = 2;
                break;
        }
        switch(oper)
        {
            case 0:
                correctAnswerNum = num1 + num2;
                operChar = '+';
                break;
            case 1:
                correctAnswerNum = num1 - num2;
                operChar = '-';
                break;
            case 2:
                correctAnswerNum = num1 * num2;
                operChar = '*';
                break;
        }
        correctAnswer = Random.Range(0, 4);
        switch(correctAnswer)
        {
            case 0:
                answer1 = correctAnswerNum;
                answer2 = answer1 + Random.Range(1, 11);
                answer3 = answer1 + Random.Range(11, 21);
                answer4 = answer1 + Random.Range(21, 31);
                break;
            case 1:
                answer2 = correctAnswerNum;
                answer1 = answer2 - Random.Range(1, 11);
                answer3 = answer2 + Random.Range(1, 11);
                answer4 = answer2 + Random.Range(11, 21);
                break;
            case 2:
                answer3 = correctAnswerNum;
                answer1 = answer3 - Random.Range(11, 21);
                answer2 = answer3 - Random.Range(1, 11);
                answer4 = answer2 + Random.Range(11, 21);
                break;
            case 3:
                answer4 = correctAnswerNum;
                answer1 = answer3 - Random.Range(21, 31);
                answer2 = answer3 - Random.Range(11, 21);
                answer3 = answer2 - Random.Range(1, 11);
                break;
        }
        question.text = num1.ToString() + " " + operChar + " " + num2.ToString();
        answers[0].text = answer1.ToString();
        answers[1].text = answer2.ToString();
        answers[2].text = answer3.ToString();
        answers[3].text = answer4.ToString();
    }
    public void AnswerQuestion(int id)
    {
        if(id == correctAnswer)
        {
            btm.ResetAllButtons();
            bm.WinChallenge();
        } else
        {
            btm.ResetAllButtons();
            bm.FailChallenge();
            GenerateChallenge();
        }
    }
}
