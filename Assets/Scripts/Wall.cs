using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wall : MonoBehaviour
{
    private Snake _snake;
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _snake = GameObject.Find("Snake").GetComponent<Snake>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_snake == null)
            Debug.LogError("SNAKE is NULL");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Body")
        {
            Body impactBody = other.transform.GetComponent<Body>();
            _snake.DamageBody(impactBody.orderNumber);
        }

        if (other.transform.tag == "Player")
        {
            _snake.KillSnakeAndBodies();
            _uiManager.GameOverSequence();
        }
    }
}
