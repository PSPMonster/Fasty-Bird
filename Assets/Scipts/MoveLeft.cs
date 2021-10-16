using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    float speed;
    float pipeWidth;

    BoxCollider2D box;
    float groundWidth;

    void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int buildIndex = currentScene.buildIndex;

        // Check the scene name as a conditional.
        switch (buildIndex)
        {
            case 2:
                speed = 1.7f;
                break;
            case 1:
                speed = 0.5f;
                break;
        }
    }

    void Start()
    {
        if (gameObject.CompareTag("Ground"))
        {
            box = GetComponent<BoxCollider2D>();
            groundWidth = box.size.x;
        }

        if (gameObject.CompareTag("Column"))
        {
            pipeWidth = GameObject.FindGameObjectWithTag("Pipe").GetComponent<BoxCollider2D>().size.x;
        }

    }

    void Update()
    {
        OnCollison();
    }

    void OnCollison()
    {
        if (GameManager.gameOver == false)
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime,
            transform.position.y);
        }

        if (gameObject.CompareTag("Column"))
        {
            if (transform.position.x < GameManager.bottomLeft.x - pipeWidth)
            {
                Destroy(gameObject);
            }
        }

        else if (gameObject.CompareTag("Ground"))
        {
            if (transform.position.x <= -groundWidth)
            {
                transform.position = new Vector2(
                    transform.position.x + 2 * groundWidth,
                    transform.position.y);
            }
        }
    }
}
