using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Vector3 offset;
    public bool IsFollowing = false;
    public float speed;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        transform.position = BallController.instance.transform.position + offset;

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsFollowing) return;
        if (BallController.instance == null) return;
        transform.position = Vector3.MoveTowards(transform.position, BallController.instance.transform.position + offset, speed);
    }
}
