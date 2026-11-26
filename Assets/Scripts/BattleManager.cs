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
    [SerializeField] private Transform attackVector;
    [SerializeField] private Transform defenseVector;
    [SerializeField] private VectorController AVController;
    [SerializeField] private VectorController DVController;
    [SerializeField] private BrickSpawner attackSpawner;
    [SerializeField] private BrickSpawner defenseSpawner;
    [SerializeField] private Button pushButton;
    [SerializeField] private Button buildButton;
    private bool selectedDifficulty = false;
    private int tokenDifficulty = -1;
    private int tokens = 5;
    private int currentReward = 1;
    private int pushCost = 1;
    private int buildCost = 1;
    private int AVSpeed = 5;
    private int DVSpeed = 8;
    private float pushLength = 3f;
    private double attackPercent = 0;
    private double defensePercent = 0;
    void Start()
    {
        battleContainer.SetActive(false);
        startHint.SetActive(true);
        content.SetActive(false);
        topBar.SetActive(false);
        
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
        UpdateContent();
    }
    private void UpdateContent()
    {
        
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
