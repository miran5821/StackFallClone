using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float lerpValue;
    Vector3 offset;
    private void Start()
    {
        offset = transform.position - player.position;
    }
    void Update()
    {
       // transform.position = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, player.position + offset, lerpValue);
    }
}
