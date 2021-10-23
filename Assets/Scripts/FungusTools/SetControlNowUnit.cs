using UnityEngine;
using Fungus;


[CommandInfo("other", "Set Control Now Unit", "set the object witch control now")]
public class SetControlNowUnit : Command
{
    public ControlUnit unit;
    public override void OnEnter()
    {
        MyGameManager.instance.changeControlNowUnit(unit);
        Continue();
    }
    public override Color GetButtonColor()
    {
        return new Color32(139, 134, 78, 255);
    }
    public override string GetSummary()
    {
        return string.Format("set {0} to controlNowUnit", unit?.gameObject.name);
    }
}
