using UnityEngine;
public class PlayerController : MonoBehaviour
{
  public AudioClip deathClip = null;
  public float jumpForce = 0f;
  int jumpCount = 0;
  bool isGrounded = false;
  bool isDead = false;

  Rigidbody2D playerRigidBody = null;
  Animator animator = null;
  AudioSource playerAudio = null;

  void Start()
  {
    playerRigidBody = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    playerAudio = GetComponent<AudioSource>();
  }

  void Update()
  {
    if (isDead)
    {
      return;
    }

    if (Input.GetMouseButtonDown(0) && jumpCount < 2)
    {
      jumpCount++;
      playerRigidBody.linearVelocity = Vector2.zero;
      playerRigidBody.AddForce(new Vector2(0, jumpForce));
      playerAudio.Play();
    }
    else if (Input.GetMouseButtonUp(0) && playerRigidBody.linearVelocity.y > 0)
    {
      playerRigidBody.linearVelocity = playerRigidBody.linearVelocity * 0.5f;
    }
  }


  void Die()
  {
    animator.SetTrigger("Die");

    playerAudio.clip = deathClip;
    playerAudio.Play();

    playerRigidBody.linearVelocity = Vector2.zero;
    isDead = true;

    GameManager.instance.OnPlayerDead();
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Dead" && !isDead)
    {
      Die();
    }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.contacts[0].normal.y > 0.7f)
    {
      isGrounded = true;
      jumpCount = 0;
    }
  }

  void OnCollisionExit2D(Collision2D collsision)
  {
    isGrounded = false;
  }
}
