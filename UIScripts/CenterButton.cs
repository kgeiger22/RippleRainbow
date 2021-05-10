using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterButton : MonoBehaviour
{
    public static CenterButton Instance;
    private Animator animator;

    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    public void Press()
    {
        Debug.Log("Button pressed");
        StateMachine.Instance.AddStateToQueue(new EnterGameScreenState());
        StateMachine.Instance.AddStateToQueue(new StartRoundState());
        StateMachine.Instance.AddStateToQueue(new StartLevelState());
    }

    public void InitGame()
    {
        animator.Play("CenterButtonInit");
    }

    public void ExitMainMenu()
    {
        animator.Play("CenterButtonExitMainMenu");
    }

    public void StartGame()
    {

    }

    public void TransIn()
    {
        animator.Play("PlayButtonTransIn");
    }

    public void TransOut()
    {
        animator.Play("PlayButtonTransOut");
    }
}
