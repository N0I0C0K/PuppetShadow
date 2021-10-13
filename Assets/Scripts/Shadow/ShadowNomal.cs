using UnityEngine;

class ShadowNomal : ShadowUnit
{
    public override void execute(Vector3 lightOffsetPos)
    {
        offsetPos = new Vector3(-lightOffsetPos.x / lightOffsetPos.z, -lightOffsetPos.y / lightOffsetPos.z, 0);
        float k = (lightOffsetPos.z + 1) / lightOffsetPos.z / 2;
        this.offsetScale = new Vector3(initialScale.x * k, initialScale.y * k, initialScale.z);
        this.transform.position = this.initialPos + this.offsetPos;
        this.transform.localScale = this.offsetScale;
    }
}