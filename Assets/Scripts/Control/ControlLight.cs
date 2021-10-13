using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

/// <summary>
/// 单独把光源控制和其他物品控制分开
/// </summary>
public class ControlLight : MonoBehaviour
{
    // Start is called before the first frame update
    public Light2D light2D;
    private float horizontalRaw, verticalRaw;
    private bool rightShift, rightCtrl;
    private void Start()
    {
        if (light2D == null)
        {
            light2D = GetComponent<Light2D>();
            if (light2D == null)
                Debug.LogError("can not find light2d");
        }
    }
    private void Update()
    {
        updateInputData();
        handleInput();
    }
    private void handleInput()
    {

    }
    private void updateInputData()
    {
        horizontalRaw = Input.GetAxisRaw("RightX");
        verticalRaw = Input.GetAxisRaw("RightY");
        rightCtrl = Input.GetKey(KeyCode.RightControl);
        rightShift = Input.GetKey(KeyCode.RightShift);
    }
}
