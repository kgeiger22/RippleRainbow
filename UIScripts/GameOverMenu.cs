//The menu that pops up when the player loses

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : PauseMenu
{
    public static new GameOverMenu Instance;

    private void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
    }

    public override void Open()
    {
        base.Open();

        Text textObject = GameObject.Find("GameOverLevelText").GetComponent<Text>();
        textObject.text = "LEVEL: " + ScoreUI.Instance.currentLevel.ToString();
        textObject.transform.GetChild(0).gameObject.SetActive(ScoreUI.Instance.currentLevel >= PlayerStats.Instance.Data.BestLevel);

        textObject = GameObject.Find("GameOverScoreText").GetComponent<Text>();
        textObject.text = "SCORE: " + GameController.Instance.score.ToString();
        textObject.transform.GetChild(0).gameObject.SetActive(GameController.Instance.score >= PlayerStats.Instance.Data.BestScore);
    }
}
