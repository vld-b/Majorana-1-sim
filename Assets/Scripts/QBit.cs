using TMPro;
using UnityEngine;

public class QBit : MonoBehaviour
{
    // Are set in Unity
    public RectTransform arrowTransform;
    public TextMeshProUGUI superpositionLabel;

    public bool isStateKnown = false;
    public bool leansTowardsZero;
    public float currentAngle { get; private set; } = 0.0f;
    private float currentAngleVel = 0.0f;
    public float superposition // Is also the chance of measuring a one
    {
        get
        {
            float reducedAngle = (arrowTransform.eulerAngles.z % 360.0f) - 270;
            if (reducedAngle >= 0.0f && reducedAngle <= 90.0f)
                return Mathf.Sin(Mathf.Deg2Rad * (reducedAngle));
            else
                return float.NaN; // If the angle is outside of defined range, return NaN
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        arrowTransform.eulerAngles = new Vector3(0, 0, Random.Range(0.0f, 360.0f));
        SetSuperposition(arrowTransform.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStateKnown)
        {
            SetSuperposition(currentAngle + Time.deltaTime * 180.0f);
        } else
        {
            SetSuperposition(currentAngle);
        }
    }

    public void SetSuperposition(float angle)
    {
        currentAngle = angle;
        string superpositionString = (Mathf.Floor(superposition * 100.0f) / 100.0f).ToString();
        superpositionLabel.text = superpositionString;
        arrowTransform.eulerAngles = new Vector3(0, 0, Mathf.SmoothDampAngle(arrowTransform.eulerAngles.z, angle - 90.0f, ref currentAngleVel, 0.5f));
    }

    public void DoGroverStep(float angle)
    {
        SetSuperposition(currentAngle + (leansTowardsZero ? -angle : angle));
    }
}
