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
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        if (col == null)
            col = GetComponent<Collider2D>();
    }
    public override void execute(InputKey inputKey)
    {
        rigidBody.velocity = new Vector2(inputKey.horizontalRaw * speed, rigidBody.velocity.y);
        if (Mathf.Abs(rigidBody.velocity.x) > speed)
        {
            Debug.LogError("speed error");
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
    }
}