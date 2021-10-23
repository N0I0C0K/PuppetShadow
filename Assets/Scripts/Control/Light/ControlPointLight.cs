using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ControlPointLight : ControlLightUnit
{
    public Vector3 offsetPos = new Vector3(0, 0, 1);
    private Light2D light2D;
    private Vector3 initialPos;
    public float initialZ = 1f;
    private void Start()
    {
        initialPos = this.transform.position;
    }
    private void Update()
    {
        handleChange();
    }
    private void handleChange()
    {
        Vector3 tempPos = this.transform.position - initialPos;
        if (tempPos != offsetPos)
        {
            offsetPos = tempPos;
            onLightOffsetChange?.Invoke(offsetPos + new Vector3(0, 0, initialZ));
        }
    }
}
