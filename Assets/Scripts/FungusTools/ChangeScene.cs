using UnityEngine;
using Fungus;

[CommandInfo("other", "change scene", "change scene to target scene")]
public class ChangeScene : Command
{
    public string tarSceneName = "";
    public override void OnEnter()
    {
        MyGameManager.instance.changeScene(tarSceneName);
    }
    public override string GetSummary()
    {
        return string.Format("change scene to {0}", tarSceneName);
    }
    public override Color GetButtonColor()
    {
        return new Color32(152, 251, 152, 255);
    }
}