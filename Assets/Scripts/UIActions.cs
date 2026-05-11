using System.Collections;
using UnityEngine;

public class UIActions : MonoBehaviour
{
    public ParticleSystem condensationEffect;
    private ParticleSystem.EmissionModule eModule;

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
    }

    private IEnumerator TurnCondensationOff()
    {
        yield return new WaitForSeconds(10);
        eModule.enabled = false;
    }
}
