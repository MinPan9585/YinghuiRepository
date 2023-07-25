using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeParentMovement : MonoBehaviour
{
    public Transform objectToCopy;
    private Vector3 initialPositionOffset;
    private Quaternion initialRotationOffset;

    void Start()
    {
        // Calculate the initial offset between the two objects
        initialPositionOffset = transform.position - objectToCopy.position;
        initialRotationOffset = Quaternion.Inverse(objectToCopy.rotation) * transform.rotation;
    }

    void LateUpdate()
    {
        // Calculate the difference in position and rotation
        Vector3 positionDifference = objectToCopy.TransformVector(initialPositionOffset);
        Quaternion rotationDifference = objectToCopy.rotation * initialRotationOffset;

        // Apply the difference to the copied position and rotation
        transform.position = objectToCopy.position + positionDifference;
        transform.rotation = rotationDifference;
    }

}