using UnityEngine;

public class ControlUnit : MonoBehaviour
{
    public ControlUnit()
    {

    }
    public virtual void execute(InputKey inputKey)
    {
        return;
    }
    private void OnMouseDown()
    {
        ControlManager.changeControlUnit(this);
    }
}