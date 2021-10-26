using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EndTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public string nextSceneName;
    void Start()
    {
        if (nextSceneName.Length <= 0)
            Debug.LogError("next scene name could not be none");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        MyGameManager.instance.changeScene(this.nextSceneName);
    }
}
