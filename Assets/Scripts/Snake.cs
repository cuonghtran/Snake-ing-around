using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private float _screenWidth;
    private Vector2 _zeroVelocity = Vector2.zero;

    [SerializeField] private float _speed = 1f;
    private float _maxSpeed = 5f;

    private float _canCheckDistance = 6f;
    private float _checkRate = 4f;

    private Rigidbody2D _rigidBody2d;
    [SerializeField] private Transform _bodyPrefab;
    public List<Transform> snakeBodyList;
    public Vector2 initialPosition;

    public int distanceTravelled;

    private float currentRotation;
    private float rotationSense = 110f;

    public bool isGameOver;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _screenWidth = Screen.width;
        snakeBodyList = new List<Transform>();
        _rigidBody2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver == false)
        {
            // handle touch button
            int i = 0;
            while (i < Input.touchCount) // loop over every touch found
            {
                if (Input.GetTouch(i).position.x > _screenWidth / 2)
                    currentRotation -= rotationSense * Time.deltaTime; // move right
                else if (Input.GetTouch(i).position.x < _screenWidth / 2)
                    currentRotation += rotationSense * Time.deltaTime; // move left
                ++i;
            }

            // calculate distance travelled
            distanceTravelled = (int)(transform.position.y - initialPosition.y);

            if (_speed < _maxSpeed)
            {
                if (Time.timeSinceLevelLoad > _canCheckDistance)
                {
                    _canCheckDistance += _checkRate;
                    IncreaseMaxSpeed(distanceTravelled);
                }
            }
            
            // for testing
            #if UNITY_EDITOR
            float hor = Input.GetAxis("Horizontal");
            if (hor < 0)
                currentRotation += rotationSense * Time.deltaTime;
            if (hor > 0)
                currentRotation -= rotationSense * Time.deltaTime;
            #endif
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (isGameOver == false)
        {
            MoveForward();
            Rotation();
        }
    }

    void MoveForward()
    {
        transform.position += transform.up * _speed * Time.deltaTime;
    }

    void Rotation()
    {
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, currentRotation));
    }

    public void EatFood()
    {
        if (snakeBodyList.Count == 0)
        {
            Vector3 currentPos = transform.position;
            Transform newBody = Instantiate(_bodyPrefab, currentPos, Quaternion.identity) as Transform;
            snakeBodyList.Add(newBody);
        }
        else
        {
            Vector3 currentPos = snakeBodyList[snakeBodyList.Count-1].transform.position;
            Transform newBody = Instantiate(_bodyPrefab, currentPos, Quaternion.identity) as Transform;
            snakeBodyList.Add(newBody);
        }
    }

    public void DamageBody(int impactBodyPos)
    {
        if (snakeBodyList.Count > 0)
        {
            int bodyCount = snakeBodyList.Count;
            for (int i = bodyCount - 1; i >= impactBodyPos; i--)
            {
                Transform body = snakeBodyList[i];
                Rigidbody2D rb = body.GetComponent<Rigidbody2D>();
                rb.gravityScale = 1.5f;

                snakeBodyList.RemoveAt(i);
                Destroy(body.gameObject, 2f);
                
            }
        }
    }

    private void IncreaseMaxSpeed(int distance)
    {
        _speed += 0.25f;
    }

    public void KillSnakeAndBodies()
    {
        isGameOver = true;
        DamageBody(0);
        _rigidBody2d.gravityScale = 1f;
        //Destroy(this.gameObject, 1.5f);
    }
}
