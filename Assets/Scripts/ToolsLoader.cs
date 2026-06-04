using UnityEngine;

public class ToolsLoader : MonoBehaviour
{
    public GameObject eventLogMessage;
    public RectTransform attachEventMessagesTo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Tools.eventLogMessage = eventLogMessage;
        Tools.attachTo = attachEventMessagesTo;
    }
}
