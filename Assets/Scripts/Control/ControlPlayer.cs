using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
class ControlPlayer : ControlUnit
{
    public float speed = 2.0f;
    public float jumpForce = 8.0f;
    [Range(0, 3)]
    public int jumpTimeMax = 2;
    private int jumpTimes = 0;
    public Collider2D col;
    public Rigidbody2D rigidBody;
    public LayerMask layer;
    private Animator animator;
    private Vector3 initialScale;
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        if (col == null)
            col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogWarning("player has no animator");
        initialScale = this.transform.localScale;
    }
    public override void execute(InputKey inputKey)
    {
        rigidBody.velocity = new Vector2(inputKey.horizontalRaw * speed, rigidBody.velocity.y);
        if (inputKey.horizontalRaw != 0)
        {
            this.transform.localScale = new Vector3(inputKey.horizontalRaw * initialScale.x, initialScale.y, initialScale.z);
        }
        if (inputKey.keyJump)
        {
            if (col.IsTouchingLayers(layer))
            {
                this.jumpTimes = 0;
            }
            if (jumpTimes < jumpTimeMax)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                jumpTimes++;
            }
        }
        if (animator)
        {
            animator.SetFloat("speedX", Mathf.Abs(rigidBody.velocity.x));
        }
    }
}