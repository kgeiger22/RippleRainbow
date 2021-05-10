//Controls the main menu

using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;
    Animator anim;

    private void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
    }

    public void Open()
    {
        Debug.Log("opening main menu");
        anim.Play("MainMenuOpen");
    }

    public void Close()
    {
        Debug.Log("closing main menu");
        anim.Play("MainMenuClose");
    }

    public void StartGame()
    {
        if (!GameController.Instance.IsSFXMuted)
        {
            GameObject.Find("StartSound").GetComponent<AudioSource>().Play();
        }
        StateMachine.Instance.AddStateToQueue(new EnterGameScreenState());
        StateMachine.Instance.AddStateToQueue(new StartRoundState());
        StateMachine.Instance.AddStateToQueue(new StartLevelState());
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
}
