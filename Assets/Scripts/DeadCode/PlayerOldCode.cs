using UnityEngine;

public class PlayerOldCode : MonoBehaviour
{
    public enum Type
    {
        Normal,
        Water,
        Magma,
        Poison
    }
    const int SLICETHRESHOLD = 1000;

    [SerializeField] float jumpForce = 0.3f;
    [SerializeField] float spinSpeed = 4f;
    [SerializeField] float doubleJumpForce = 2f;
    [SerializeField] float doubleJumpSideForce = 4f;
    [SerializeField] float doubleJumpSpinSpeed = 2f;

    // ----------------------------------------
    [SerializeField] PolygonCollider2D blade;
    [SerializeField] PolygonCollider2D handle;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool canDoubleJump = false;

    private float jumpCooldown = 0.1f;
    private float lastJumpTime = -1f;

    // TODO: inventory & coin

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void PlayerControl()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            rb.transform.position = new Vector3(0, 1f, 0);
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrounded)
            {
                Jump();
            }
            else if(canDoubleJump)
            {
                DoubleJump();
            }
        }
    }


    void Jump()
    {
        float facingDirection = rb.transform.localEulerAngles.z;
        float spinDirection = 1f;
        if (facingDirection >= 180)
        {
            spinDirection = -1f;
        }

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        rb.AddTorque(spinDirection * spinSpeed, ForceMode2D.Impulse);

        isGrounded = false;
        canDoubleJump = true;
        lastJumpTime = Time.time;
    }

    void DoubleJump()
    {
        float xDirection = 0f;
        float spinDirection = Mathf.Sign(rb.angularVelocity);

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            xDirection = -1f;
            spinDirection = 1f;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            xDirection = 1f;
            spinDirection = -1f;
        }

        rb.angularVelocity = 0f;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(new Vector2(xDirection * doubleJumpSideForce, doubleJumpForce), ForceMode2D.Impulse);
        rb.AddTorque(spinDirection * doubleJumpSpinSpeed, ForceMode2D.Impulse);

        canDoubleJump = false;
    }

    public virtual void OnDestroyableObjectCollide(Collision2D collision){ 
        
    }


    void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
        if(Time.time - lastJumpTime > jumpCooldown)
        {
            canDoubleJump = false;
        }

        if(collision.otherCollider == blade)
        {
            // jott
            if(collision.gameObject.layer == 3){
                float rotationSpeed = rb.angularVelocity;
                if(Mathf.Abs(rotationSpeed) >= SLICETHRESHOLD){
//                    collision.collider
                }
            } 
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}

