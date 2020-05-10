using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    private Snake _snakeHead;
    public int orderNumber;

    private Vector3 movementVelocity;
    [Range(0.0f, 1.0f)]
    public float smoothSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _snakeHead = GameObject.Find("Snake").GetComponent<Snake>();

        if (_snakeHead == null)
        {
            Debug.LogError("Snake NULL");
        }

        orderNumber = _snakeHead.snakeBodyList.Count - 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (_snakeHead.snakeBodyList.Contains(transform))
        {
            if (orderNumber == 0)
            {
                transform.position = Vector3.SmoothDamp(transform.position, _snakeHead.transform.position, ref movementVelocity, smoothSpeed);
                transform.LookAt(_snakeHead.transform.position);
            } 
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, _snakeHead.snakeBodyList[orderNumber-1].transform.position, ref movementVelocity, smoothSpeed);
                transform.LookAt(_snakeHead.transform.position);
            }
        }
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
    }
}
