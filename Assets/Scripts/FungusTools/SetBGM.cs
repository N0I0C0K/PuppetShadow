using UnityEngine;
using Fungus;

[CommandInfo("other", "set bgm", "set bgm to pause or unpause")]
public class SetBGM : Command
{
    [Header("set statu to pause")]
    public bool pause = false;
    public override void OnEnter()
    {
        if (pause)
            MyGameManager.instance.pauseBGM();
        else
            MyGameManager.instance.unpauseBGM();
        Continue();
    }
}