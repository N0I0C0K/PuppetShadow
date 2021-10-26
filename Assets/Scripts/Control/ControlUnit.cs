using UnityEngine;

public class ControlUnit : MonoBehaviour
{
    public bool isDead { get; protected set; } = false;
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
        return this.transform.position.y <= -50;
    }
    private void OnMouseDown()
    {
        MyGameManager.changeControlUnit(this);
    }
}