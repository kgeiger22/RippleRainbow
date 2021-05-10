//Ball script for the small balls flying around the screen

using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public enum State
    {
        MOVING,
        EXPLODING
    }

    //Average Starting Speed
    public float AverageSpeed = 5f;
    //The amount of change between the ball's starting velocity and averag. Between 0 and 1
    public float SpeedVariance = 0.2f;

    private float spawnTime = 0.6f;
    private float speed;
    public State state;
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    MaterialPropertyBlock materialPropertyBlock;
    private float startingScale;
    private bool bHasExploded = false;
    private Vector2 pauseVelocity;

    void Start()
    {
        Vector2 initialVelocity = new Vector2(Random.Range(-0.9f, 0.9f), Random.Range(-0.9f, 0.9f));
        if (Mathf.Abs(initialVelocity.x) < 0.1f)
        {
            initialVelocity.x *= 10f;
        }
        if (Mathf.Abs(initialVelocity.y) < 0.1f)
        {
            initialVelocity.y *= 10f;
        }
        speed = AverageSpeed + SpeedVariance * Random.Range(0, 1f);
        initialVelocity = initialVelocity.normalized * speed;

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = initialVelocity;

        spriteRenderer = GetComponent<SpriteRenderer>();

        startingScale = transform.localScale.x;

        GameController.Instance.PauseEvent.AddListener(Pause);
        GameController.Instance.UnpauseEvent.AddListener(Unpause);

        state = State.MOVING;

        StartCoroutine(Spawn());
    }

    private void Update()
    {
        //make sure velocity doesn't drop too low
        if (rigidBody.velocity.magnitude != speed)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * speed;
        }
        
    }

    public void Pause()
    {
        pauseVelocity = rigidBody.velocity;
        rigidBody.Sleep();
    }

    public void Unpause()
    {
        rigidBody.WakeUp();
        rigidBody.velocity = pauseVelocity;
    }

    private void OnDestroy()
    {
        GameController.Instance.PauseEvent.RemoveListener(Pause);
        GameController.Instance.UnpauseEvent.RemoveListener(Unpause);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state == State.MOVING && collision.gameObject.layer == 10)
        {
            Explode();
        }
        else if (collision.gameObject.layer == 11)
        {
            GameController.Instance.HandleExplosion();
            Destroy(gameObject);
        }
    }

    public void Explode()
    {
        if (!bHasExploded)
        {
            bHasExploded = true;
            GameController.Instance.HandleExplosion();
            GameObject explosion = Instantiate(Resources.Load<GameObject>("Prefabs/Explosion"));
            explosion.transform.position = transform.position;
            Destroy(gameObject);
        }
    }

    IEnumerator Spawn()
    {
        Vector2 newScale = new Vector3(0, 0, 1);
        transform.localScale = newScale;
        float currentTime = 0f;
        float val;
        while (currentTime < spawnTime)
        {
            while (GameController.Instance.IsPaused()) { yield return null; }

            val = Mathf.Lerp(0, 1, currentTime/ spawnTime);
            val *= val;
            newScale.x = val * startingScale;
            newScale.y = val * startingScale;
            transform.localScale = newScale;

            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}
