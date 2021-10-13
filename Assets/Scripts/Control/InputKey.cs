using UnityEngine;

public class InputKey
{
    public float horizontalRaw { private set; get; }
    public float verticalRaw { private set; get; }
    public bool keyJump { private set; get; }
    public InputKey(float horizontal, float vertical, bool keyJump)
    {
        this.horizontalRaw = horizontal;
        this.verticalRaw = vertical;
        this.keyJump = keyJump;
    }
    public static InputKey GetInputKeyByInput()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        bool _keyJump = Input.GetButtonDown("Jump");
        return new InputKey(hor, ver, _keyJump);
    }
    public void update()
    {
        this.horizontalRaw = Input.GetAxisRaw("Horizontal");
        this.verticalRaw = Input.GetAxisRaw("Vertical");
        this.keyJump = Input.GetButtonDown("Jump");
    }
}