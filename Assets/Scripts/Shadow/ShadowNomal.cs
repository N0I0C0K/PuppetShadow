using UnityEngine;
using System.Collections.Generic;
class ShadowNomal : ShadowUnit
{
    //用来实现平台拖动物体
    private List<GameObject> colliders;
    private float offsetX = 0;
    private Renderer spriteRenderer;
    public float rate = 1f;
    public override void Start()
    {
        base.Start();
        colliders = new List<GameObject>();
        spriteRenderer = GetComponent<Renderer>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        colliders.Add(other.gameObject);
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        colliders.Remove(other.gameObject);
    }
    public override void execute(Vector3 lightOffsetPos)
    {
        offsetPos = new Vector3(-lightOffsetPos.x / lightOffsetPos.z, -lightOffsetPos.y / lightOffsetPos.z, 0);
        offsetPos = offsetPos * rate;
        offsetX = offsetPos.x - this.transform.position.x + this.initialPos.x;
        foreach (var item in this.colliders)
        {
            item.transform.position += new Vector3(offsetX, 0, 0);
        }
        float k = (lightOffsetPos.z + 1) / lightOffsetPos.z / 2;
        this.offsetScale = new Vector3(initialScale.x * k, initialScale.y * k, initialScale.z);
        this.transform.position = this.initialPos + this.offsetPos;
        this.transform.localScale = this.offsetScale;
        spriteRenderer?.material.SetFloat("_Blur", k - 0.7f);
    }
}