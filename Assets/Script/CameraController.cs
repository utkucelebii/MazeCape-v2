using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform player;
    private Vector3 camPos;

    private void Start()
    {
        camPos = player.position + offset;
    }

    private void LateUpdate()
    {
        camPos = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, camPos, 0.0125f);

        transform.LookAt(player);
    }
}
