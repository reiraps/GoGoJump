using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip;
    public float jumpForce = 400f;
    public float moveSpeed = 3f;
    //private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isDead = false;
    private Rigidbody2D playerRbody;
    private Animator animator;
    private AudioSource playerAudio;

    private Transform deadZoneTransform;
    private bool deadZoneMoved = false;

    void Start()
    {
        playerRbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        deadZoneTransform = GameObject.FindGameObjectWithTag("Dead").transform;
    }

    void Update()
    {
        if (isDead) return;

        CheckOutOfCamera();

        float horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
        }

        Vector2 movement = new Vector2(horizontalInput * moveSpeed, playerRbody.velocity.y);
        playerRbody.velocity = movement;

        if (isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            Jump();
        }

        animator.SetBool("Grounded", isGrounded);
    }
    //void LateUpdate()
    //{
    //    MoveDeadZone();
    //}

    void Jump()
    {
        //jumpCount++;
        playerRbody.velocity = Vector2.zero;
        playerRbody.AddForce(new Vector2(0, jumpForce));
        playerAudio.Play();

        if (!deadZoneMoved)
        {
            Vector3 deadZonePosition = deadZoneTransform.position;
            deadZonePosition.y = Mathf.Min(deadZonePosition.y, transform.position.y);
            deadZoneTransform.position = deadZonePosition;
            deadZoneMoved = true;
        }
    }
    //void MoveDeadZone()
    //{
    //    Vector3 deadZonePosition = deadZoneTransform.position;

    //    if (!deadZoneMoved)
    //    {
    //        deadZonePosition.y = Mathf.Min(deadZonePosition.y, transform.position.y);

    //        deadZoneTransform.position = deadZonePosition;

    //        deadZoneMoved = true;
    //    }
    //}

    void CheckOutOfCamera()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPosition.x < 0f || viewportPosition.x > 1f || viewportPosition.y < 0f || viewportPosition.y > 1f)
        {
            Die();
        }
    }

    //void Die()
    //{
    //    animator.SetTrigger("Die");

    //    playerAudio.clip = deathClip;
    //    playerAudio.Play();

    //    playerRbody.velocity = Vector2.zero;
    //    isDead = true;
    //    GameManager.gamemanager.OnPlayerDead();
    //}

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if(other.tag =="Dead"&& !isDead)
    //    {
    //        Die();
    //    }
    //}
    void Die()
    {

        animator.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerRbody.velocity = Vector2.zero;
        isDead = true;
        GameManager.gamemanager.OnPlayerDead();
    }

    IEnumerator RestartAfterDeath()
    {
        yield return new WaitForSeconds(1.0f);

        if (GameManager.gamemanager != null)
        {
            GameManager.gamemanager.OnPlayerDead();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Dead" && !isDead)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
        {                          
            isGrounded = true;
            //jumpCount = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
