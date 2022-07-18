using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public float rotation = 0.0f;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] GameObject objectToRotate;
    Quaternion targetRotation;

    float timeCount = 0.0f;

    void Update()
    {
        if (rotation >= 360.0f || rotation <= -360.0f) rotation %= 360.0f;
        targetRotation = Quaternion.Euler(0, 0, rotation);
        objectToRotate.transform.rotation = Quaternion.Lerp(objectToRotate.transform.rotation, targetRotation, timeCount * rotationSpeed / 1000);
        timeCount += Time.deltaTime;
    }

    public void RotateAntiClockwise()
    {
        rotation += 90.0f;
    }
    public void RotateClockwise()
    {
        rotation -= 90.0f;
    }
}
