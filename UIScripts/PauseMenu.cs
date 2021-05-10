using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    protected Animator anim;

    private void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
    }
    
    public virtual void Open()
    {
        anim.Play("PauseMenuOpen");
        SetMusicGraphic();
        SetSFXGraphic();
    }

    public virtual void Close()
    {
        anim.Play("PauseMenuClose");
    }

    public void Unpause()
    {
        if (GameController.Instance.IsPaused())
        {
            Close();
            GameController.Instance.Invoke("Unpause", 0.5f);
        }
    }

    public void OpenChallengeMenu()
    {
        ChallengeMenu.Instance.TransIn(gameObject);
    }

    public void MuteMusic()
    {
        Debug.Log("Mute Music Pressed");
        GameController.Instance.MuteMusic();
        SetMusicGraphic();
    }

    public void MuteSFX()
    {
        Debug.Log("Mute SFX Pressed");
        GameController.Instance.MuteSFX();
        SetSFXGraphic();
    }

    private void SetMusicGraphic()
    {
        GameObject button = transform.Find("PlayButton").transform.Find("MusicButton").gameObject;
        button.transform.GetChild(0).gameObject.SetActive(!GameController.Instance.IsMusicMuted);
        button.transform.GetChild(1).gameObject.SetActive(GameController.Instance.IsMusicMuted);
    }

    private void SetSFXGraphic()
    {
        GameObject button = transform.Find("PlayButton").transform.Find("SFXButton").gameObject;
        button.transform.GetChild(0).gameObject.SetActive(!GameController.Instance.IsSFXMuted);
        button.transform.GetChild(1).gameObject.SetActive(GameController.Instance.IsSFXMuted);
    }

    public void Restart()
    {
        if (!GameController.Instance.IsSFXMuted)
        {
            GameObject.Find("StartSound").GetComponent<AudioSource>().Play();
        }
        StateMachine.Instance.ForceReset();
        GameController.Instance.EndGame();
        LevelController.Instance.Restart();
        StateMachine.Instance.AddStateToQueue(new EndLevelState());
        StateMachine.Instance.AddStateToQueue(new StartRoundState());
        StateMachine.Instance.AddStateToQueue(new StartLevelState());
        if (GameController.Instance.IsPaused())
        {
            Unpause();
        }
        else
        {
            Close();
        }
    }
}
