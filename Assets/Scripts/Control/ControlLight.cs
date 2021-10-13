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
    public Light2D globalLight;
    public float speed = 2.0f;
    public float zoomSpeed = 2.0f;
    public Vector3 offsetPos = new Vector3(0, 0, 0);
    private float horizontalRaw, verticalRaw, lightZoom;
    private bool keyReSet = false;
    private float innerRadius;
    private float outerRatio = 3f;
    private float intensity;
    public delegate void lightOffsetChangeEvent(Vector3 offset);
    public static lightOffsetChangeEvent onLightOffsetChange;
    private void Start()
    {
        if (light2D == null)
        {
            light2D = GetComponent<Light2D>();
            if (light2D == null)
                Debug.LogError("can not find light2d");
        }
        if (globalLight == null)
            Debug.LogError("global can not be null");
        this.innerRadius = light2D.pointLightInnerRadius;
        this.intensity = light2D.intensity;
    }
    private void Update()
    {
        updateInputData();
        handleInput();
        effectProess();
    }
    /// <summary>
    /// 光随着距离变化导致颜色变化
    /// </summary>
    private void effectProess()
    {
        if (canZoom(offsetPos))
        {
            light2D.pointLightInnerRadius = this.innerRadius - offsetPos.z / 2;
            light2D.pointLightOuterRadius = light2D.pointLightInnerRadius * (this.outerRatio + this.outerRatio / (10 - offsetPos.z));
            light2D.intensity = this.intensity - offsetPos.z / 10;

        }
    }
    private bool canZoom(Vector3 pos)
    {
        return this.innerRadius - pos.z / 2 >= 0.5 && this.innerRadius - pos.z / 2 <= this.innerRadius;
    }
    private void handleInput()
    {
        Vector3 tempOffsetPos = offsetPos;
        if (!this.keyReSet)
            tempOffsetPos += new Vector3(horizontalRaw * speed * Time.deltaTime, verticalRaw * speed * Time.deltaTime, lightZoom * zoomSpeed * Time.deltaTime);
        else
            tempOffsetPos = new Vector3(0, 0, 0);
        if (tempOffsetPos != offsetPos && canZoom(tempOffsetPos))
        {
            offsetPos = tempOffsetPos;
            onLightOffsetChange?.Invoke(this.offsetPos + new Vector3(0, 0, 1));
        }
        this.transform.position = Camera.main.transform.position + offsetPos + new Vector3(0, 0, 10);
    }
    private void updateInputData()
    {
        horizontalRaw = Input.GetAxisRaw("RightX");
        verticalRaw = Input.GetAxisRaw("RightY");
        lightZoom = Input.GetAxisRaw("LightZoom");
        keyReSet = Input.GetKeyDown(KeyCode.RightAlt);
    }
}
