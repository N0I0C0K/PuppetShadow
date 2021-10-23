using UnityEngine;
using Fungus;

public class PlotTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public string message;
    public bool canRepeatTrigger = false;
    private int triggerTime = 0;
    private void Start()
    {
        if (message.Length == 0)
            Debug.LogError(string.Format("{0} : message is none", this.gameObject.name));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (canRepeatTrigger)
            {
                Fungus.Flowchart.BroadcastFungusMessage(message);
                triggerTime++;
            }
            else if (!canRepeatTrigger && triggerTime == 0)
            {
                Fungus.Flowchart.BroadcastFungusMessage(message);
                triggerTime++;
            }
        }
    }
}
