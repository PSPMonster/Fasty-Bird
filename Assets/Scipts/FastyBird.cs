using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FastyBird : MonoBehaviour
{
    private Vector3 touchPosition;
    private Vector3 direction;
    public Score score;
    public Sprite birdDead;
    public Animator birdParentAnim;
    public PipeSpawner pipeSpawner;

    Animator animator;
    SpriteRenderer sp;
    private Rigidbody2D rb;

    public GameManager gameManager;
    private float flySpeed = 3f;

    // bool touchedGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0 && GameManager.gameOver == false &&
        GameManager.gamePaused == false)
        {
            if (GameManager.waitingToPlay == false)
            {
                birdParentAnim.enabled = false;
                Fly();
                pipeSpawner.InstantiateColumn();
                gameManager.GameStarted();
            }
            else
            {
                Fly();
            }
        }
    }

    void Fly()
    {
        Touch touch = Input.GetTouch(0);
        touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        touchPosition.z = 0;
        direction = (touchPosition - transform.position);
        rb.velocity = new Vector2(direction.x, direction.y) * flySpeed;

        if (touch.phase == TouchPhase.Ended)
            rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.CompareTag("Column"))
        {
            //print("We have scored");
            score.Scored();
        }
        else if (collison.CompareTag("Pipe"))
        {
            gameManager.GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (GameManager.gameOver == false)
            {
                gameManager.GameOver();
                GameOver();
            }
            else
            {
                // touchedGround = true;
                GameOver();
            }
        }
    }

    void GameOver()
    {
        // touchedGround = true;
        animator.enabled = false;
        sp.sprite = birdDead;
        rb.gravityScale = 0.1f;
        // transform.rotation = Quaternion.Euler(0, 0, -90);
    }
}
