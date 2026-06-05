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
    public GameObject btBellZustand;
    public TMP_Text lbGroverSchritte;
    public GameObject vlGrover;
    public GameObject btAuslesen;

    public GameObject hoveringTextObject;
    private TextMeshProUGUI hoveringText;

    private readonly float initialGroverAngle = Mathf.Pow(2, 8.0f * -0.5f);

    private byte korrekteZahl;
    private char[] korrekteZahlBin;
    private int groverStep = 0;
    private float groverStepSize;
    private char[] measuredNumber;

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
                korrekteZahlBin = Tools.byteToBin(korrekteZahl);
                Tools.AddEventMessage("Korrekte Zahl gesetzt auf " + korrekteZahl + " mit Binärdarstellung: 0b" + new string(korrekteZahlBin));
                btBellZustand.SetActive(true);
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

    public void SetzeBellZustand()
    {
        for (int i = 0; i < 8; ++i)
        {
            qBits[i].leansTowardsZero = korrekteZahlBin[i] == '0';
            qBits[i].isStateKnown = true;
            qBits[i].SetSuperposition(45.0f);
        }
        Tools.AddEventMessage("Bell-Zustand in allen QBits erzeugt");
    }

    public void CalculateGroverStepSize()
    {
        groverStepSize = initialGroverAngle * Mathf.Rad2Deg;
    }

    public void GroverStep()
    {
        foreach (QBit qBit in qBits)
            qBit.DoGroverStep(groverStepSize);
        lbGroverSchritte.text = "Schritt " + ++groverStep + "/12";
        Tools.AddEventMessage("Grover-Schritt " + groverStep + " ausgeführt");

        if (groverStep >= 12)
        {
            vlGrover.SetActive(false);
            btAuslesen.SetActive(true);
        }
    }

    public void MeasureComputer()
    {
        measuredNumber = new char[8];
        for (int i = 0; i < 8; ++i)
        {
            measuredNumber[i] = (Random.Range(0.0f, 1.0f) < (qBits[i].superposition * qBits[i].superposition)) ? '1' : '0'; // Compare to square as per state vector
        }
        foreach (QBit qBit in qBits)
        {
            qBit.isStateKnown = false;
        }
        Tools.AddEventMessage("Ausgelesene Zahl: 0b" + new string(measuredNumber) + " im vergleich zur korrekten Zahl: 0b" + new string(korrekteZahlBin));

        bool korrekteMessung = true;
        for (int i = 0; i < 8; ++i)
            if (measuredNumber[i] != korrekteZahlBin[i])
                korrekteMessung = false;
        Tools.AddEventMessage(korrekteMessung ? "Korrekte und ausgelesene Zahl stimmen überein." : "Korrekte und ausgelesene Zahl stimmen nicht überein. Unglückliche Messung!");
    }
}
