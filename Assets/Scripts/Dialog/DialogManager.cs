using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance { get; private set; }
    public Text textUI;
    private string[] tarString;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
    public void showTexts(string texts)
    {
        var lines = texts.Split('\n');
        tarString = lines;
        StartCoroutine(setTexts());
    }
    IEnumerator setTexts()
    {
        foreach (var line in tarString)
        {
            textUI.text = line;
            yield return new WaitForSeconds(line.Length * 0.1f);
        }
    }

}
