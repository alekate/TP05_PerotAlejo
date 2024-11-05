using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private InitialPlayerData playerData;
    [SerializeField] private SoundController soundController;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private Animator handAnim;
    [SerializeField] private bool grounded = true;

    [SerializeField] private GameObject feet;

    public float horizontallInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = transform.Find("Player Sprite").GetComponent<Animator>(); //referenciar los animator de esta forma porque unity no le gusta cuando referencias dos cosas iguales al mimso tiempo >:/
        handAnim = transform.Find("Hand Sprite").GetComponent<Animator>();
    }

    private void Update()
    {
        Animations();

        Movement();

        feet.gameObject.SetActive(false);

        if (!grounded)
        {
            feet.gameObject.SetActive(true);
        }


        //flip player
        if(horizontallInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontallInput < -0.01)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    private void Movement()
    {
        horizontallInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontallInput * playerData.movementSpeed, rb.velocity.y);

        if (Input.GetKey(playerData.jumpKey) && grounded == true)
        {
            Jump();
            soundController.JumpSFX();
        }
    }    
    private void Jump()
    {
        grounded = false;
        rb.velocity = new Vector2(rb.velocity.x, playerData.jumpForce);
    }

    private void Animations()
    {
        playerAnim.SetBool("run", horizontallInput != 0);
        handAnim.SetBool("run", horizontallInput != 0);

        playerAnim.SetBool("grounded", grounded);
        handAnim.SetBool("grounded", grounded);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true; 
        }
    }
}