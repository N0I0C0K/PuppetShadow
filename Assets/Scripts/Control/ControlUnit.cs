using UnityEngine;

public class ControlUnit : MonoBehaviour
{
    public bool isDead { get; private set; } = false;
    public ControlUnit()
    {

    }
    public virtual void execute(InputKey inputKey)
    {
        return;
    }
    private void Update()
    {
        if (!isDead && checkDeath())
        {
            isDead = true;
            onDying();
        }
    }
    public virtual void onDying()
    {

    }
    public virtual bool checkDeath()
    {
        return this.transform.position.y <= -30;
    }
    private void OnMouseDown()
    {
        ControlManager.changeControlUnit(this);
    }
}