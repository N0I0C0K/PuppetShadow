using UnityEngine;

public class ShadowUnit : MonoBehaviour
{
    protected Vector3 initialPos;
    protected Vector3 initialScale;
    protected Vector3 offsetPos = new Vector3(0, 0, 0);
    protected Vector3 offsetScale = new Vector3(1, 1, 1);
    public virtual void Start()
    {
        ControlLight.onLightOffsetChange += this.execute;
        initialPos = this.transform.position;
        initialScale = this.transform.localScale;
    }
    public virtual void execute(Vector3 lightOffsetPos)
    {

    }
    private void OnDestroy()
    {
        ControlLight.onLightOffsetChange -= this.execute;
    }
}