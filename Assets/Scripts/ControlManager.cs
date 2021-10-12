using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ControlManager : MonoBehaviour
{
    public static ControlManager instance { private set; get; }
    public delegate void changeControlUnitEvent(ControlUnit controlUnit);
    public static changeControlUnitEvent changeControlUnit;
    public ControlUnit controlNowUnit;
    public InputKey inputKey;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        inputKey = InputKey.GetInputKeyByInput();
        changeControlUnit += this.changeControlNowUnit;
    }
    private void Update()
    {
        GetInput();
        if (controlNowUnit != null)
            controlNowUnit.execute(this.inputKey);
    }
    private void changeControlNowUnit(ControlUnit controlUnit)
    {
        this.controlNowUnit = controlUnit;
    }
    private void GetInput()
    {
        inputKey.update();
    }
}
