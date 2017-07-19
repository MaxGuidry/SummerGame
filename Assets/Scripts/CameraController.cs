using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;

    public int CameraSpeed;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }
    // Use this for initialization
    void Update()
    {
        var b = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, b, Time.deltaTime * CameraSpeed);
    }
}
