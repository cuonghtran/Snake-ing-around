using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private Snake _snake;

    // Start is called before the first frame update
    void Start()
    {
        _snake = GameObject.Find("Snake").GetComponent<Snake>();
        if (_snake == null)
            Debug.LogError("SNAKE is NULL");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _snake.EatFood();
            Destroy(this.gameObject, 0.1f);
        }
    }
}
