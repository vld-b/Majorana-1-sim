using NUnit.Framework;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Majorana1 : MonoBehaviour
{
    // List is populated in Unity
    public System.Collections.Generic.List<QBit> qBits;

    public GameObject hoveringTextObject;
    private TextMeshProUGUI hoveringText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hoveringText = hoveringTextObject.GetComponent<TextMeshProUGUI>();
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
