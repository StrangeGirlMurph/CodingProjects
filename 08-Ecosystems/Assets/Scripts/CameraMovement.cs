﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float mouseMovementSensitivity = 4f;
    public float mouseZoomingSensitivity = 1f;
    public float distance = 20f;
    public float initAngle = 20f;
    public int initHeight = 2;
    Vector3 center = Vector3.up * 5;

    // variables
    [Header("Settings")]
    [SerializeField] float maxAngle = 88f;
    [SerializeField] float minAngle = 2f;
    [SerializeField] float maxHeight = 9;
    [SerializeField] float minHeight = 0;
    [SerializeField] float maxDistance = 60;
    [SerializeField] float minDistance = 0;
    [SerializeField] bool freeView = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // - init -
        defaultView();
    }


    void Update()
    {
        //Debug.DrawRay(transform.position, transform.forward * 5, Color.green, 2f);

        if (Input.GetMouseButton(0))
        {
            float horizontal = Input.GetAxis("Mouse X") * mouseMovementSensitivity;
            float vertical = -Input.GetAxis("Mouse Y") * mouseMovementSensitivity;

            // horizontal rotation
            transform.RotateAround(center, Vector3.up, horizontal);

            // vertical rotation
            if (freeView)
            {
                Vector3 around = Vector3.Cross(transform.forward, Vector3.up);
                transform.RotateAround(center, around, vertical);
            }
            else
            {
                float angle = transform.eulerAngles.x;
                angle = (angle > 180) ? angle - 360 : angle;
                if (angle + vertical >= minAngle & angle + vertical < maxAngle)
                {
                    Vector3 around = Vector3.Cross(transform.forward, Vector3.up);
                    transform.RotateAround(center, around, -vertical);
                }
            }

        }

        if (Input.GetMouseButton(1))
        {
            float zoom = Input.GetAxis("Mouse X") * mouseZoomingSensitivity;
            if (distance - zoom >= minDistance & distance - zoom <= maxDistance)
            {
                distance -= zoom;
                transform.position = -transform.forward * distance + center;
            }
        }

        // move center
        if (Input.GetKeyDown(KeyCode.UpArrow) & center.y <= maxHeight - 1)
        {
            center += Vector3.up;
            transform.position += Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) & center.y >= minHeight + 1)
        {
            center += Vector3.down;
            transform.position += Vector3.down;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            defaultView();
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            transform.rotation = Quaternion.Euler(90, 0, 0);
            transform.position = -transform.forward * distance + center;
        }
    }

    void defaultView()
    {
        center = Vector3.up * initHeight; // center
        transform.rotation = Quaternion.Euler(initAngle, 0, 0); // angle
        transform.position = -transform.forward * distance + center; // position
    }
}
