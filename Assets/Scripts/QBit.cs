using UnityEngine;

public class QBit : MonoBehaviour
{
    // Is set in Unity
    public RectTransform arrowTransform;

    public bool isStateKnown { get; private set; } = false;
    public float currentAngle { get; private set; } = 0.0f;

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
        arrowTransform.rotation = Quaternion.Euler(0, 0, angle - 90.0f);
    }
}
