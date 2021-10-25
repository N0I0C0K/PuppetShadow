using UnityEngine;

public class AutoReSet : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 initialPos;
    private Quaternion initialRotate;
    private Rigidbody2D rigidBody;
    void Start()
    {
        initialPos = this.transform.position;
        initialRotate = this.transform.rotation;
        rigidBody = GetComponent<Rigidbody2D>();
        ControlPlayer.onPlayerDying += this.reSetObject;
    }
    public void reSetObject()
    {
        this.transform.position = initialPos;
        this.transform.rotation = initialRotate;
        if (rigidBody != null)
            rigidBody.velocity = new Vector2(0, 0);
    }
    private void OnDestroy()
    {
        ControlPlayer.onPlayerDying -= this.reSetObject;
    }
}
