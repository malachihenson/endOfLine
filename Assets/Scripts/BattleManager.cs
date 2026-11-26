using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;
public class BattleManager : MonoBehaviour
{
    [SerializeField] private GameObject battleContainer;
    [SerializeField] private TextMeshProUGUI attackPercentText;
    [SerializeField] private TextMeshProUGUI defensePercentText;
    [SerializeField] private TextMeshProUGUI tokenCounter;
    [SerializeField] private TextMeshProUGUI taskText;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private TextMeshProUGUI pushText;
    [SerializeField] private TextMeshProUGUI buildText;
    [SerializeField] private GameObject startHint;
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject topBar;
    [SerializeField] private GameObject mainGame;
    [SerializeField] private GameObject[] challenges;
    [SerializeField] private Transform attackVector;
    [SerializeField] private Transform defenseVector;
    [SerializeField] private VectorController AVController;
    [SerializeField] private VectorController DVController;
    [SerializeField] private BrickSpawner attackSpawner;
    [SerializeField] private BrickSpawner defenseSpawner;
    [SerializeField] private Button pushButton;
    [SerializeField] private Button buildButton;
    private bool selectedDifficulty = false;
    public int tokenDifficulty = -1;
    private int tokens = 5;
    private int pushCost = 2;
    private int buildCost = 2;
    private int AVSpeed = 5;
    private int DVSpeed = 8;
    private int clumpSize = 2;
    private int clumpNum = 0;
    private int challengeValue = 0;
    private int currentChallenge = 0;
    private int prevChallengeID = -1;
    private float pushLength = 3f;
    private double attackPercent = 0;
    private double defensePercent = 0;
    void Start()
    {
        battleContainer.SetActive(false);
        startHint.SetActive(true);
        content.SetActive(false);
        topBar.SetActive(false);
        for(int i=0; i<challenges.Length; i++)
        {
            challenges[i].SetActive(false);
        }
        
    }
    void Update()
    {
        attackPercent = Math.Round((attackVector.localPosition.y + 100) / 2);
        defensePercent = Math.Round(0 - ((defenseVector.localPosition.y - 100) / 2));
        attackPercentText.text = attackPercent.ToString() + "%";
        defensePercentText.text = defensePercent.ToString() + "%";
        if(tokens < pushCost && pushButton.enabled)
        {
            pushButton.enabled = false;
        }
        if(tokens < buildCost && buildButton.enabled)
        {
            buildButton.enabled = false;
        }
        if(tokens > pushCost && !pushButton.enabled)
        {
            pushButton.enabled = true;
        }
        if(tokens > buildCost && !buildButton.enabled)
        {
            buildButton.enabled = true;
        }
    }
    public void StartBattle()
    {
        //eventually animate intro, for now we will have intrusive popup
        battleContainer.SetActive(true);
        mainGame.SetActive(false);
        attackSpawner.SpawnBrick(15);
        defenseSpawner.SpawnBrick(15);
        StartCoroutine(StartAttack());
    }
    public void Push()
    {
        if(tokens >= pushCost)
        {
            tokens -= pushCost;
            StartCoroutine(PushAttack());
        }
        tokenCounter.text = tokens.ToString() + "¢";
    }
    public void Build()
    {
        if(tokens >= buildCost)
        {
            tokens -= buildCost;
            defenseSpawner.SpawnBrick(1);
        }
        tokenCounter.text = tokens.ToString() + "¢";
    }
    public void SetDifficulty(int choice)
    {
        if(!selectedDifficulty)
        {
            selectedDifficulty = true;
            startHint.SetActive(false);
            content.SetActive(true);
            topBar.SetActive(true);
        }
        tokenDifficulty = choice;
        GenerateClump();
    }
    public void GenerateClump()
    {
        clumpNum = 0;
        clumpSize = UnityEngine.Random.Range(2, 5);
        currentChallenge = UnityEngine.Random.Range(0, challenges.Length);
        if(prevChallengeID != -1)
        {
            challenges[prevChallengeID].SetActive(false);
        }
        challenges[currentChallenge].SetActive(true);
        switch(currentChallenge)
        {
            case 0:
                taskText.text = "task = \"sort_the_numbers\";";
                break;
            case 1:
                taskText.text = "task = \"find_the_answer\";";
                break;
            case 2:
                taskText.text = "task = \"sort_the_letters\";";
                break;
            case 3:
                taskText.text = "task = \"spot_the_difference\";";
                break;
            case 4:
                taskText.text = "task = \"push_the_button\";";
                break;
            case 5:
                taskText.text = "task = \"copy_the_word\";";
                break;
        }
        prevChallengeID = currentChallenge;
        LoadChallenge();
    }
    public void LoadChallenge()
    {
        switch(tokenDifficulty)
        {
            case 0:
                challengeValue = UnityEngine.Random.Range(1, 4);
                break;
            case 1:
                challengeValue = UnityEngine.Random.Range(3, 7);
                break;
            case 2:
                challengeValue = UnityEngine.Random.Range(6, 10);
                break;
        }
        rewardText.text = "reward = " + challengeValue.ToString() + "¢;";
        switch(currentChallenge)
        {
            case 0:
                challenges[currentChallenge].GetComponent<NumberSort>().GenerateChallenge();
                break;
            case 1:
                challenges[currentChallenge].GetComponent<MultiMath>().GenerateChallenge();
                break;
            case 2:
                challenges[currentChallenge].GetComponent<LetterSort>().GenerateChallenge();
                break;
            case 3:
                challenges[currentChallenge].GetComponent<SpotTheDifference>().GenerateChallenge();
                break;
            case 4:
                challenges[currentChallenge].GetComponent<Clicker>().GenerateChallenge();
                break;
            case 5:
                challenges[currentChallenge].GetComponent<CopyWord>().GenerateChallenge();
                break;
        }
    }
    public void WinChallenge()
    {
        tokens += challengeValue;
        tokenCounter.text = tokens.ToString() + "¢";
        clumpNum++;
        if(clumpNum == clumpSize)
        {
            GenerateClump();
        } else
        {
            LoadChallenge();
        }
    }
    public void FailChallenge()
    {
        //shake effect
    }
    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(3);
        SyncVectors();
        AVController.BeginPath();
        DVController.BeginPath();
    }
    IEnumerator PushAttack()
    {
        AVSpeed = AVSpeed * 3;
        SyncVectors();
        yield return new WaitForSeconds(pushLength);
        AVSpeed = AVSpeed / 3;
        SyncVectors();
    }
    private void SyncVectors()
    {
        AVController.approachSpeed = AVSpeed;
        DVController.approachSpeed = DVSpeed;
    }
}
