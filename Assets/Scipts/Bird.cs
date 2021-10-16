using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    //references
    public Score score;
    public GameManager gameManager;
    public Sprite birdDead;
    public PipeSpawner pipeSpawner;
    public Animator birdParentAnim;
    public Animator camraShake;


    SpriteRenderer sp;
    Animator animator;
    Rigidbody2D rb;

    //scene index
    int buildIndex;

    //bird stats
    public float speed;
    int angle;
    int maxAngle = 20;
    int minAngle = -90;


    bool touchedGround;
    private bool istriggered = false;
    private bool firstTimeHit = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        BirdRotation();
    }

    void BirdRotation()
    {
        if (EventSystem.current.IsPointerOverGameObject() &&
        EventSystem.current.currentSelectedGameObject != null &&
        EventSystem.current.currentSelectedGameObject.CompareTag("PauseButton"))
            return;

        if (Input.GetMouseButtonDown(0) && GameManager.gameOver == false &&
        GameManager.gamePaused == false)
        {
            if (GameManager.waitingToPlay == false)
            {
                rb.gravityScale = 0.8f;
                birdParentAnim.enabled = false;
                Flap();
                pipeSpawner.InstantiateColumn();
                gameManager.GameStarted();
            }
            else
            {
                Flap();
            }
        }

        if (rb.velocity.y > 0)
        {
            rb.gravityScale = 0.8f;
            if (angle <= maxAngle)
            {
                angle = angle + 5;
            }
        }
        else if (rb.velocity.y < -2f)
        {
            rb.gravityScale = 0.65f;
            if (rb.velocity.y < 1.3f)
            {
                if (angle >= minAngle)
                {
                    angle = angle - 4;
                }
            }

        }

        if (touchedGround == false)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void Flap()
    {
        AudioManager.audiomanager.Play("flap");
        rb.velocity = Vector2.zero;
        rb.velocity = new Vector2(rb.velocity.x, speed);
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.CompareTag("Column"))
        {
            //print("We have scored");
            AudioManager.audiomanager.Play("point");
            score.Scored();
        }
        else if (collison.CompareTag("Pipe"))
        {
            TriggerCzek();
            camraShake.SetTrigger("Shake");
            gameManager.GameOver();
            GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (GameManager.gameOver == false)
            {
                camraShake.SetTrigger("Shake");
                gameManager.GameOver();
                TriggerCzek();
                GameOver();
            }
            else
            {
                touchedGround = true;
                TriggerCzek();
                GameOver();
            }
        }
    }

    void GameOver()
    {
        touchedGround = true;
        animator.enabled = false;
        sp.sprite = birdDead;
        // transform.rotation = Quaternion.Euler(0, 0, -90);
        TriggerCzek();
    }

    void TriggerCzek()
    {
        if (istriggered == false)
        {
            AudioManager.audiomanager.Play("die");
            istriggered = true;
        }
        if (firstTimeHit == false)
        {
            AudioManager.audiomanager.Play("hit");
            firstTimeHit = true;
        }
    }
}