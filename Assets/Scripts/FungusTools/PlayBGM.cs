using UnityEngine;
using Fungus;

[CommandInfo("other", "play bgm", "start to play bgm")]
public class PlayBGM : Command
{
    public override void OnEnter()
    {
        MyGameManager.instance.playBGM();
        Continue();
    }
}