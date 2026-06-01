using TMPro;
using UnityEngine;

public class QBit : MonoBehaviour
{
    // Are set in Unity
    public RectTransform arrowTransform;
    public TextMeshProUGUI superpositionLabel;

    public bool isStateKnown { get; private set; } = false;
    public float currentAngle { get; private set; } = 0.0f;
    public float superposition
    {
        get => Mathf.Sin(Mathf.Deg2Rad * currentAngle);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetSuperposition(Random.Range(0.0f, 360.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStateKnown)
        {
            SetSuperposition(currentAngle + Time.deltaTime * 180.0f);
        }
    }

    public void SetSuperposition(float angle)
    {
        currentAngle = angle;
        string superpositionString = (Mathf.Round(superposition * 100.0f) / 100.0f).ToString();
        superpositionLabel.text = superpositionString;
        arrowTransform.rotation = Quaternion.Euler(0, 0, angle - 90.0f);
    }
}
