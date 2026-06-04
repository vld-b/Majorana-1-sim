using NUnit.Framework;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Majorana1 : MonoBehaviour
{
    // Are set in Unity
    public System.Collections.Generic.List<QBit> qBits;
    public UnityEngine.UI.Button btKorrekteZahl;
    public TMP_InputField ifKorrekteZahl;
    public GameObject vlKorrekteZahl;

    public GameObject hoveringTextObject;
    private TextMeshProUGUI hoveringText;

    private byte korrekteZahl = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hoveringText = hoveringTextObject.GetComponent<TextMeshProUGUI>();
        btKorrekteZahl.onClick.AddListener(() =>
        {
            if (int.TryParse(ifKorrekteZahl.text, out int zahl) && 0 <= zahl && zahl < 256)
            {
                korrekteZahl = (byte)zahl;
                vlKorrekteZahl.SetActive(false);
                Tools.AddEventMessage("Korrekte Zahl gesetzt auf " + korrekteZahl + " mit Binärdarstellung: 0b" + new string(Tools.byteToBin(korrekteZahl)));
            }
            else
            {
                ifKorrekteZahl.text = "";
                ifKorrekteZahl.placeholder.GetComponent<TMP_Text>().text = "Nur 0-255 erlaubt";
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        hoveringText.text = "Quantenchip";
        hoveringTextObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        hoveringTextObject.SetActive(false);
    }
}
