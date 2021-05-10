//The outer frame that contains the main game

using UnityEngine;

public class Frame : MonoBehaviour
{
    public static Frame Instance;
    
    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }
    
    public Collider2D GetCollider()
    {
        return transform.GetChild(0).GetComponent<Collider2D>();
    }
}
