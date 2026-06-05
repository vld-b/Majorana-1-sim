using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIActions : MonoBehaviour
{
    // Are set in Unity
    public ParticleSystem condensationEffect;
    private ParticleSystem.EmissionModule eModule;
    public GameObject qBitsContainer;
    public GameObject vlKorrekteZahl;
    public GameObject eventLogObject;
    public CameraMovement camMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eModule = condensationEffect.emission;

        EventTrigger trigger = eventLogObject.GetComponent<EventTrigger>();
        if (!trigger)
            trigger = eventLogObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entryEnter = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter,
        };
        entryEnter.callback.AddListener(_ => camMovement.isVisionLocked = true);
        trigger.triggers.Add(entryEnter);

        EventTrigger.Entry entryExit = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit,
        };
        entryExit.callback.AddListener(_ => camMovement.isVisionLocked = false);
        trigger.triggers.Add(entryExit);
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

        Tools.AddEventMessage("Starten des Quantencomputers...");

        StartCoroutine("SendCoolingMessage");
    }

    private IEnumerator TurnCondensationOff()
    {
        yield return new WaitForSeconds(10);
        eModule.enabled = false;

        Tools.AddEventMessage("Kühlung abgeschlossen");

        qBitsContainer.SetActive(true);
        vlKorrekteZahl.SetActive(true);
    }

    private IEnumerator SendCoolingMessage()
    {
        yield return new WaitForSeconds(1);

        Tools.AddEventMessage("Kühlen des Quantencomputers auf 4mK...");
    }
}
