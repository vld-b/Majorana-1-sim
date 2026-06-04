using TMPro;
using UnityEngine;

public static class Tools
{
    public static GameObject eventLogMessage;
    public static RectTransform attachTo;

    public static void AddEventMessage(string message)
    {
        GameObject newMessage = GameObject.Instantiate(eventLogMessage, attachTo);
        newMessage.GetComponent<TextMeshProUGUI>().text = message;
        attachTo.sizeDelta = new Vector2(attachTo.sizeDelta.x, attachTo.sizeDelta.y + newMessage.GetComponent<RectTransform>().sizeDelta.y + 10);
    }

    public static char[] byteToBin(byte number)
    {
        char[] bin = new char[8];
        byte mask = 1 << 7;
        for (int i = 0; i < 8; ++i)
        {
            bin[i] = (number & mask) == 0 ? '0' : '1';
            mask >>= 1;
        }
        return bin;
    }
}
