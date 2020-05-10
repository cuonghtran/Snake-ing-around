using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private Snake _snake;
    [SerializeField] Text _sizeText;
    [SerializeField] Text _distanceText;
    [SerializeField] Text _gameOverText;
    [SerializeField] Button _restartButton;
    public int finalBodyCount;

    // Start is called before the first frame update
    void Start()
    {
        _snake = GameObject.Find("Snake").GetComponent<Snake>();
    }

    void LateUpdate()
    {
        if (_snake.isGameOver == false)
        {
            _distanceText.text = "Distance: " + ((int)_snake.distanceTravelled).ToString() + " meters";
            _sizeText.text = "Size: " + _snake.snakeBodyList.Count.ToString() + " bodies";
        }
    }

    public void GameOverSequence()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
}
