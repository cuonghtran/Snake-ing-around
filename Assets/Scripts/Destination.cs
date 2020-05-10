using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    private Snake _snake;
    private UIManager _uiManager;
    // Start is called before the first frame update
    void Start()
    {
        _snake = GameObject.Find("Snake").GetComponent<Snake>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
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
        if (other.transform.tag == "Player")
        {
            _snake.isGameOver = true;
            _uiManager.GameOverSequence();
        }
    }
}
