using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, 0);

    private void LateUpdate()
    {
        transform.position = target.transform.position + offset;
    }
}
