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
    public CircleCollider2D movingRange;
    public Vector3 offsetPos = new Vector3(0, 0, 0);
    private float horizontalRaw, verticalRaw, lightZoom;
    private bool keyReSet = false;
    private float innerRadius;
    private float outerRatio;
    private float intensity;
    private float initialZ;
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
        this.outerRatio = light2D.pointLightOuterRadius / this.innerRadius;
        this.intensity = light2D.intensity;
        if (this.transform.position.z == 0)
            this.transform.position += new Vector3(0, 0, 1);
        this.initialZ = this.transform.position.z;
        ControlPlayer.onPlayerDying += this.reSetLight;
        onLightOffsetChange?.Invoke(new Vector3(0, 0, initialZ));
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
        Vector3 pos = this.transform.position;
        light2D.pointLightInnerRadius = this.innerRadius * (pos.z / initialZ);
        light2D.pointLightOuterRadius = light2D.pointLightInnerRadius * ((outerRatio - 1) * pos.z / initialZ + 1);
        light2D.intensity = this.intensity / (pos.z * pos.z);
        globalLight.intensity = light2D.intensity * 0.7f;
    }
    /// <summary>
    /// 判断是否可以移动
    /// </summary>
    /// <param name="pos">pos指的是offset</param>
    /// <returns></returns>
    private bool canZoom(Vector3 pos)
    {
        float tarInner = this.innerRadius * ((pos.z + initialZ) / initialZ);
        if (tarInner < 1.8 || tarInner > 7)
            return false;
        if (movingRange != null && pos.x * pos.x + pos.y * pos.y >= movingRange.radius * movingRange.radius)
            return false;
        return true;
    }
    private void handleInput()
    {
        Vector3 tempOffsetPos = offsetPos;
        if (!this.keyReSet)
            tempOffsetPos += new Vector3(horizontalRaw * speed * Time.deltaTime, verticalRaw * speed * Time.deltaTime, lightZoom * zoomSpeed * Time.deltaTime);
        else
        {
            tempOffsetPos = new Vector3(0, 0, 0);
            keyReSet = false;
        }
        if (tempOffsetPos != offsetPos && canZoom(tempOffsetPos))
        {
            offsetPos = tempOffsetPos;
            onLightOffsetChange?.Invoke(this.offsetPos + new Vector3(0, 0, this.initialZ));
        }
        this.transform.position = Camera.main.transform.position + offsetPos + new Vector3(0, 0, 10 + this.initialZ);
    }
    public void reSetLight()
    {
        keyReSet = true;
    }
    private void updateInputData()
    {
        horizontalRaw = Input.GetAxisRaw("RightX");
        verticalRaw = Input.GetAxisRaw("RightY");
        lightZoom = Input.GetAxisRaw("LightZoom");
        //keyReSet = Input.GetKeyDown(KeyCode.RightAlt);
    }
    private void OnDestroy()
    {
        ControlPlayer.onPlayerDying -= this.reSetLight;
    }
}
