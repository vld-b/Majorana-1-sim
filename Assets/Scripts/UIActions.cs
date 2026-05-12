using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIActions : MonoBehaviour
{
    public ParticleSystem condensationEffect;
    private ParticleSystem.EmissionModule eModule;

    public VerticalLayoutGroup eventLogVerticalGroup;
    public GameObject eventLogMessage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eModule = condensationEffect.emission;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Triggered by on-off button
    public void StartMajorana()
    {
        eModule.enabled = true;

        StartCoroutine("TurnCondensationOff");

        AddEventMessage("Starten des Quantencomputers...");

        StartCoroutine("SendCoolingMessage");
    }

    private IEnumerator TurnCondensationOff()
    {
        yield return new WaitForSeconds(10);
        eModule.enabled = false;

        AddEventMessage("Kühlung abgeschlossen");
    }

    private IEnumerator SendCoolingMessage()
    {
        yield return new WaitForSeconds(1);

        AddEventMessage("Kühlen des Quantencomputers auf 4mK...");
    }

    private void AddEventMessage(string message)
    {
        GameObject newMessage = Instantiate(eventLogMessage, eventLogVerticalGroup.transform);
        newMessage.GetComponent<TextMeshProUGUI>().text = message;
    }
}
