using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
public class MyGameManager : MonoBehaviour
{
    public static MyGameManager instance { private set; get; }
    public delegate void changeControlUnitEvent(ControlUnit controlUnit);
    public static changeControlUnitEvent changeControlUnit;
    public ControlUnit controlNowUnit;
    public InputKey inputKey;
    public CinemachineVirtualCamera cineMachine;
    public Animator sceneAnimator;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += this.onSceneLoaded;
            changeControlUnit += this.changeControlNowUnit;
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


    }
    private void Update()
    {
        GetInput();
        if (controlNowUnit != null)
            controlNowUnit.execute(this.inputKey);
    }
    public void changeControlNowUnit(ControlUnit controlUnit)
    {
        Debug.Log("change control unit");
        if (controlUnit != null)
        {
            this.controlNowUnit = controlUnit;
            this.cineMachine.Follow = controlUnit.transform;
        }
        else
        {
            this.controlNowUnit = controlUnit;
        }
    }
    private void GetInput()
    {
        inputKey.update();
    }
    public void changeScene(string sceneName)
    {
        if (sceneName.Length <= 0)
            Debug.LogError("GameManager:scene name is none");
        else
            StartCoroutine(changeSceneSync(sceneName));
    }
    IEnumerator changeSceneSync(string sceneName)
    {
        sceneAnimator?.SetTrigger("SceneOut");
        yield return new WaitForSeconds(1f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    public void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneAnimator = GameObject.Find("SceneLoadEffect")?.GetComponent<Animator>();
        cineMachine = GameObject.Find("CM vcam1")?.GetComponent<CinemachineVirtualCamera>();
        if (sceneAnimator == null)
            Debug.LogWarning(string.Format("{0} has no scene load aniamtor", scene.name));
        if (cineMachine == null)
            Debug.LogWarning(string.Format("{0} has no cineMachine", scene.name));
    }
}
