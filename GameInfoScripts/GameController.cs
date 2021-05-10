//Handles game-level entities and properties

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LevelController))]
public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private bool paused = false;
    public bool IsPaused() { return paused; }
    public UnityEvent PauseEvent = new UnityEvent();
    public UnityEvent UnpauseEvent = new UnityEvent();

    public int numBallsRemaining { get; private set; } = 0;
    public int numPressesRemaining { get; private set; }
    public int score { get; private set; }
    public int combo { get; private set; }

    private int numBalls = 40;
    private int numBallsRemainingToWin = 5;
    public int numClicks { get; private set; } = 4;

    private AudioSource audioSource;
    private float audioStaringVolume;
    public bool IsSFXMuted { get; private set; } = false;
    public bool IsMusicMuted { get; private set; } = false;

    private enum State
    {
        INACTIVE,
        LOADING,
        RUNNING,
    }
    private State state;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        audioStaringVolume = audioSource.volume;
        state = State.INACTIVE;
    }

    public void StartRound()
    {
        score = 0;
        combo = 0;
        numBallsRemaining = 0;
        state = State.LOADING;
        ScoreUI.Instance.Reset();
        GameDisplay.Instance.ResetDisplay();
    }

    public void StartLevel()
    {
        state = State.RUNNING;
        numPressesRemaining = numClicks;

        for (int i = numBallsRemaining; i < numBalls; ++i)
        {
            Ball newBall = Instantiate(Resources.Load<Ball>("Prefabs/Ball"));
            newBall.transform.position = MathHelper.RandomPointInBounds(GetComponent<Collider2D>().bounds);
            numBallsRemaining++;
        }
    }

    void ProgressToNextLevel()
    {
        Debug.Log("Progress to next level");

        if (numBallsRemaining == 0)
        {
            PlayerStats.Instance.Data.LevelsFullyCleared++;
        }

        state = State.LOADING;
        GameDisplay.Instance.ResetDisplay();
        StateMachine.Instance.AddStateToQueue(new EndLevelState());
        StateMachine.Instance.AddStateToQueue(new LoadNextLevelState());
        StateMachine.Instance.AddStateToQueue(new StartLevelState());
        ScoreUI.Instance.IncrementLevel();

        if (score > PlayerStats.Instance.Data.BestScore)
        {
            PlayerStats.Instance.Data.BestScore = score;
        }

        PlayerStats.Instance.SaveData();
    }

    void LoadLevel()
    {

    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanPress())
        {
            Press();
        }
        else if (state == State.RUNNING && CanPress() && numBallsRemaining <= numBallsRemainingToWin)
        {
            ProgressToNextLevel();
        }
        else if (state == State.RUNNING && CanPress() && numPressesRemaining <= 0)
        {
            GameOver();
        }
    }

    private bool CanPress()
    {
        return !MathHelper.IsPointerOverUIObject() && state != State.LOADING && (state == State.INACTIVE || GameObject.FindGameObjectsWithTag("Explosion").Length == 0);
    }

    private void GameOver()
    {
        state = State.LOADING;
        StateMachine.Instance.AddStateToQueue(new GameOverState());
    }

    private void Press()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (MathHelper.IsInBounds(mousePosition, Frame.Instance.GetCollider().bounds))
        {
            Explosion explosion = Instantiate(Resources.Load<Explosion>("Prefabs/Explosion"));
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0;
            explosion.transform.localPosition = newPosition;
            numPressesRemaining--;
            GameDisplay.Instance.DarkenClickIcon(numClicks - 1 - numPressesRemaining);

            if (combo > PlayerStats.Instance.Data.BestCombo)
            {
                PlayerStats.Instance.Data.BestCombo = combo;
            }
            combo = 0;
        }
    }

    public void HandleExplosion()
    {
        numBallsRemaining--;
        score += 10 + combo * 5;
        ScoreUI.Instance.SetScore(score);
        PlayerStats.Instance.Data.TotalBubblesPopped++;
        combo++;
        float r = ((float)numBalls - numBallsRemaining) / ((float)numBalls - numBallsRemainingToWin);

        if (r <= 1)
        {
            GameDisplay.Instance.SetMeter(r);
        }
    }

    public void EndGame()
    {
        if (score > PlayerStats.Instance.Data.BestScore)
        {
            PlayerStats.Instance.Data.BestScore = score;
        }

        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
        {
            Destroy(ball);
        }

        foreach (GameObject explosion in GameObject.FindGameObjectsWithTag("Explosion"))
        {
            Destroy(explosion);
        }

        PlayerStats.Instance.SaveData();
    }

    public void Pause()
    {
        paused = true;
        PauseEvent.Invoke();
    }

    public void Unpause()
    {
        paused = false;
        UnpauseEvent.Invoke();
    }

    public void MuteMusic()
    {
        if (!IsMusicMuted)
        {
            audioSource.volume = 0;
        }
        else
        {
            audioSource.volume = audioStaringVolume;
        }
        IsMusicMuted = !IsMusicMuted;
    }

    public void MuteSFX()
    {
        IsSFXMuted = !IsSFXMuted;
    }
}
