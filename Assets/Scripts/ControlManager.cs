using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ControlManager : MonoBehaviour
{
    public static ControlManager instance { private set; get; }
    public delegate void changeControlUnitEvent(ControlUnit controlUnit);
    public static changeControlUnitEvent changeControlUnit;
    public ControlUnit controlNowUnit;
    public InputKey inputKey;
    public CinemachineVirtualCamera cineMachine;
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
        this.cineMachine.Follow = controlUnit.transform;
    }
    private void GetInput()
    {
        inputKey.update();
    }
}
