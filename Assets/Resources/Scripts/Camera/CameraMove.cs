using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.position;
    }
    private void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
