using UnityEngine;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour {
   public AudioClip deathClip; // 사망시 재생할 오디오 클립
   public float jumpForce = 700f; // 점프 힘

    private int jumpCount = 0; // 누적 점프 횟수
    private bool isGrounded = false; // 바닥에 닿았는지 나타냄
    private bool isDead = false; // 사망 상태

    [SerializeField]private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
    [SerializeField]private Animator animator; // 사용할 애니메이터 컴포넌트
    [SerializeField]private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트

    // private void Start()
    // {
    //     // 초기화
    //     playerRigidbody = GetComponent<Rigidbody2D>();
    //     animator = GetComponent<animator>();
    //     playerAudio = GetComponent<AudioSource>();
    // }

    private void Update() {
        // 사용자 입력을 감지하고 점프하는 처리

        if (isDead)
        {
            // 사망시 처리하지 않고 종료
            return;
        }


        // 마우스 왼쪽 버튼 0, 오른쪽 1, 휠 버튼 2
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            jumpCount++;
            playerRigidbody.velocity = Vector2.zero;

            // 리지드 바디에 위쪽을 힘 주기
            playerRigidbody.AddForce(Vector2.up * jumpForce);

            // 오디오
            playerAudio.Play();
        }
        else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        animator.SetBool("isGrounded", isGrounded);
    }

    private void Die()
    {
        // 사망 처리

        animator.SetTrigger("Die");

        // 오디오소스에 사망 오디오로 교체하여 플레이
        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerRigidbody.velocity = Vector2.zero;
        isDead = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 트리거 콜라이더를 가진 장애물과의 충돌을 감지

        if (other.CompareTag("Dead") && !isDead)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥에 닿았음을 감지하는 처리

        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 바닥에서 벗어났음을 감지하는 처리

        isGrounded = false;
    }
}