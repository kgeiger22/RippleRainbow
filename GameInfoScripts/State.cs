//Game state definitions

using UnityEngine;

public abstract class State
{
    protected enum TransitionState
    {
        INVALID,
        ENTER,
        EXIT,
    }
    protected TransitionState FSM = TransitionState.INVALID;
    protected float timer = 0;
    protected float duration = 0;

    public virtual bool IsDoneEntering()
    {
        return timer >= duration;
    }
    public virtual bool IsDoneExiting()
    {
        return timer >= duration;
    }

    public virtual void Enter()
    {
        timer = 0;
        FSM = TransitionState.ENTER;
    }
    public virtual void Exit()
    {
        timer = 0;
        FSM = TransitionState.EXIT;
    }

    public virtual void OnFinishEnter()
    {
    }
    public virtual void OnFinishExit()
    {
    }

    public virtual void Update()
    {
        if (!GameController.Instance.IsPaused())
        {
            timer += Time.deltaTime;
        }
    }

    public State() { }
}

public class GameIntroState : State
{
    public override void Enter()
    {
        duration = 0.8f;
        LogoIntro.Instance.PlayIntro();
        base.Enter();
    }
}

public class OpenMainMenuState : State
{
    public override void Enter()
    {
        duration = 0.5f;
        MainMenu.Instance.Open();
        base.Enter();
    }

    public override void Exit()
    {
        duration = 0.5f;
        MainMenu.Instance.Close();
        LogoIntro.Instance.PlayOutro();
        base.Exit();
    }
}

public class EnterGameScreenState : State
{
    public override void Enter()
    {
        duration = 0.5f;
        ScoreUI.Instance.gameObject.SetActive(true);
        GameDisplay.Instance.gameObject.SetActive(true);
        GameDisplay.Instance.Fade(0.5f, 0f, 1f);
        Frame.Instance.gameObject.SetActive(true);
        Frame.Instance.GetComponent<FadeSpriteController>().Fade(0.5f, 0f, 1f);
    }
}

public class StartRoundState : State
{
    public override void Enter()
    {
        GameController.Instance.StartRound();
        LevelController.Instance.LoadNextLevel();
        base.Enter();
    }
}

public class EndLevelState : State
{
    public override void Enter()
    {
        duration = 0.5f;
        Debug.Log("End Level");
        LevelController.Instance.EndCurrentLevel();
        base.Enter();
    }
}

public class LoadNextLevelState : State
{
    public override void Enter()
    {
        Debug.Log("Load Next Level");
        LevelController.Instance.LoadNextLevel();
        base.Enter();
    }
}

public class StartLevelState : State
{
    public override void Enter()
    {
        Debug.Log("Start Level");
        GameController.Instance.StartLevel();
        base.Enter();
    }
}

public class GameOverState : State
{
    public override void Enter()
    {
        GameOverMenu.Instance.Open();
        base.Enter();
    }

    public override void Exit()
    {
        GameOverMenu.Instance.Close();
        base.Exit();
    }
}