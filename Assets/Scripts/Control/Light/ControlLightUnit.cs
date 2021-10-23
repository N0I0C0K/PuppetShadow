using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLightUnit : MonoBehaviour
{
    public delegate void lightOffsetChangeEvent(Vector3 offset);
    public static lightOffsetChangeEvent onLightOffsetChange;
}
