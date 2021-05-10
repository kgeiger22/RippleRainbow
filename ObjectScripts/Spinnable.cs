//Spins an object over time

using UnityEngine;

public class Spinnable : MonoBehaviour
{
    public float rotationSpeed = 90f;
    private Vector3 rotateVector;

    // Start is called before the first frame update
    void Start()
    {
        rotateVector = new Vector3(0, 0, rotationSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.Instance.IsPaused())
        {
            transform.Rotate(rotateVector * Time.deltaTime);
        }
    }
}
