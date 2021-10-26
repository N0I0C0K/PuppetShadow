using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("other", "wait for press", "wait until press any key")]
public class WaitForPress : Command
{
    [Header("特定的按键, 没写的话默认任意按键")]
    public KeyCode key;
    public override void OnEnter()
    {
        StartCoroutine(getAnyKey());
    }
    IEnumerator getAnyKey()
    {
        while (!Input.anyKeyDown) { }
        Continue();
        yield return null;
    }
    public override string GetSummary()
    {
        return string.Format("wait for key down");
    }
    public override Color GetButtonColor()
    {
        return new Color32(139, 105, 105, 255);
    }
}
