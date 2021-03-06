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
    public AudioSource bgmSource;
    public bool isPause { get; private set; } = false;
    public Canvas canvas;
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
        if (controlNowUnit != null && !isPause)
            controlNowUnit.execute(this.inputKey);
        if (Input.GetKeyDown(KeyCode.Escape))
            menuIn();
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
        bgmSource = GameObject.Find("BGM")?.GetComponent<AudioSource>();
        if (sceneAnimator == null)
            Debug.LogWarning(string.Format("{0} has no scene load aniamtor", scene.name));
        if (cineMachine == null)
            Debug.LogWarning(string.Format("{0} has no cineMachine", scene.name));
        if (bgmSource == null)
            Debug.LogWarning(string.Format("{0} has no BGM", scene.name));

    }
    public void playBGM()
    {
        bgmSource?.Play();
    }
    public void pauseBGM()
    {
        bgmSource?.Pause();
    }
    public void unpauseBGM()
    {
        bgmSource?.UnPause();
    }
    public void menuIn()
    {
        if (SceneManager.GetActiveScene().name == "Start")
            return;
        isPause = true;
        canvas?.gameObject.SetActive(true);
    }
    public void backToStart()
    {
        canvas?.gameObject.SetActive(false);
        this.changeScene("Start");
    }
    public void backToGame()
    {
        isPause = false;
        canvas?.gameObject.SetActive(false);
    }
}
