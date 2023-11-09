using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public Transform cameraTransform;
    public float shakeDuration;
    public float shakeMagnitude = 0.1f;

    private Vector3 originalPosition;
    private float elapsed = 0.0f;

    void Start()
    {
        originalPosition = cameraTransform.localPosition;
    }

    public void Rumble()
    {
        originalPosition = cameraTransform.localPosition;
        elapsed = 0.0f;
    }

    void Update()
    {
        if (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            cameraTransform.localPosition = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
        }
        else
        {
            cameraTransform.localPosition = originalPosition;
        }
    }
}