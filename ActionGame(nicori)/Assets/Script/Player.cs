using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Player : MonoBehaviour
{

    [SerializeField, Header("移動速度")]
    private float moveSpeed;
    [SerializeField, Header("ジャンプ速度")]
    private float jumpSpeed;
    [SerializeField, Header("体力")]
    private int hp;
    [SerializeReference, Header("無敵時間")]
    private float damageTime;
    [SerializeReference, Header("点滅時間")]
    private float flashTime;


    private Vector2 _inputDirection;
    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private bool bJump;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim  = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LookMoveDirec();
        HitFloor();
    }

    private void Move()
    {
        if (bJump) return;
        rigid.velocity = new Vector2(_inputDirection.x * moveSpeed, rigid.velocity.y);
        anim.SetBool("Walk", _inputDirection.x != 0.0f);
    }

    private void LookMoveDirec()
    {
        if(_inputDirection.x > 0.0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if(_inputDirection.x < 0.0f)
        {
            transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "Enemy")
        {
            HitEnemy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "Goal")
        {
            FindObjectOfType<MainManager>().ShowGameClearUI();
            enabled = false;
            GetComponent<PlayerInput>().enabled = false;
        }
    }

    private void HitFloor()
    {
        int layerMask = LayerMask.GetMask("Floor");
        Vector3 rayPos = transform.position - new Vector3(0.0f, transform.lossyScale.y / 2.0f);
        Vector3 raySize = new Vector3(transform.lossyScale.x - 0.1f, 0.1f);
        RaycastHit2D rayHit = Physics2D.BoxCast(rayPos, raySize, 0.0f, Vector2.zero, 0.0f, layerMask);
        if(rayHit.transform == null)
        {
            bJump = true;
            anim.SetBool("Jump", bJump);
            return;
        }
        if(rayHit.transform.tag == "Floor" && bJump)
        {
            bJump = false;
            anim.SetBool("Jump", bJump);
        }
    }

    private void HitEnemy(GameObject enemy)
    {
        float halfScaleY = transform.lossyScale.y / 2.0f;
        float enemyHalfScaleY = enemy.transform.lossyScale.y / 2.0f;
        if (transform.position.y - (halfScaleY - 0.1f) >= enemy.transform.position.y + (enemyHalfScaleY - 0.1f))
        {
            Destroy(enemy);
            rigid.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
        else
        {
            enemy.GetComponent<Enemy>().PlayerDamage(this);
            gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
            StartCoroutine(Damage());
        }
    }

    IEnumerator Damage()
    {
        Color color = spriteRenderer.color;
        for (int i = 0; i < damageTime; i++)
        {
            yield return new WaitForSeconds(flashTime);
            spriteRenderer.color = new Color(color.r, color.g, color.b, 0.0f);
            yield return new WaitForSeconds(flashTime);
            spriteRenderer.color = new Color(color.r, color.g, color.b, 1.0f);

        }

        spriteRenderer.color = color;
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private void Dead()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Camera camera = Camera.main;
        if(camera.name == "Main Camera" && camera.transform.position.y > transform.position.y)
        {
            Destroy(gameObject);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _inputDirection = context.ReadValue<Vector2>();
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed || bJump) return;

        rigid.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        //bJump = true;
        //anim.SetBool("Jump", bJump);
    }

    public void Damage(int damage)
    {
        hp = Mathf.Max(hp - damage, 0);
        Dead();
    }

    public int GetHP()
    {
        return hp;
    }
}
