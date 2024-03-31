using UnityEngine;

public class Player : MonoBehaviour
{
    // Player states
    public float moveSpeed;
    public float jumpForce;

    public bool isMoveable = true;
    private bool isJump;
    private bool isFloor = true;
    private bool isDash;

    // Respawn Position
    private Vector3 respawnPos;

    // 0 : RedKey, 1 : BlueKey, 2 : YellowKey
    private bool[] hasKeys = new bool[3];

    // Components
    private Rigidbody2D rigid2D;
    private Animator anim;
    public GameManager gameManager;

    void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        respawnPos = transform.position;
    }

    void Update()
    {
        if (isMoveable)
        {
            Move();
            Jump();
            Dash();
        }
    }

    // Player move
    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(h, 0).normalized * -moveSpeed * Time.deltaTime;
    }

    // Player jump
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isJump && isFloor)
        {
            isJump = true;
            isFloor = false;

            SoundManager.instance.PlaySFX("DownJump");

            rigid2D.AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.S) && !isJump && !isFloor)
        {
            isFloor = true;
            isJump = true;

            SoundManager.instance.PlaySFX("UpJump");

            rigid2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // Player Dash
    private void Dash()
    {
        if (!isDash)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isDash = true;

                SoundManager.instance.PlaySFX("Dash");

                anim.SetTrigger("Dash");
                rigid2D.AddForce(new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)) * jumpForce * 0.5f, ForceMode2D.Impulse);

                Invoke("ReloadDash", 8f);
            }
        }
    }

    // isDash is false delay.
    private void ReloadDash()
    {
        isDash = false;
    }

    // isMoveable true
    private void Moveable()
    {
        isMoveable = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            isJump = false;
        }
        else if (collision.collider.CompareTag("Finish"))
        {
            isMoveable = false;

            gameManager.nextStage();

            for (int i = 0; i < hasKeys.Length; i++)
            {
                hasKeys[i] = false;
            }

            transform.position = respawnPos;

            Invoke("Moveable", 0.5f);
        }
        else if (collision.collider.CompareTag("RedWall"))
        {
            if (hasKeys[0])
            {
                collision.gameObject.SetActive(false);
            }
        }
        else if (collision.collider.CompareTag("BlueWall"))
        {
            if (hasKeys[1])
            {
                collision.gameObject.SetActive(false);
            }
        }
        else if (collision.collider.CompareTag("YellowWall"))
        {
            if (hasKeys[2])
            {
                collision.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("WorldBorder"))
        {
            gameManager.DecreaseHp();

            transform.position = respawnPos;
        }
        else if (collision.CompareTag("RedKey"))
        {
            collision.gameObject.SetActive(false);

            hasKeys[0] = true;
        }
        else if (collision.CompareTag("BlueKey"))
        {
            collision.gameObject.SetActive(false);

            hasKeys[1] = true;
        }
        else if (collision.CompareTag("YellowKey"))
        {
            collision.gameObject.SetActive(false);

            hasKeys[2] = true;
        }
    }
}
