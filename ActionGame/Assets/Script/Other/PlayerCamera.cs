﻿using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    private const float MOVE_SPEED = 1.0f;
    private const float ROTATE_SPEED = 3.0f;
    private const float HIGHT = 1.5f;
    private const float DISTANCE = 3.0f;

    private float horizontalAngle;
    private float verticalAngle;

    public Vector3 right { get { return transform.right; } }
    public Vector3 forward { get { return Vector3.Scale(transform.forward, new Vector3(1, 0, 1)); } }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="target"></param>
    public void Init(Transform target)
    {
        horizontalAngle = 180.0f * (Mathf.PI / 180.0f);
        verticalAngle = 45.0f * (Mathf.PI / 180.0f);
    }

    /// <summary>
    /// 操作
    /// </summary>
    /// <param name="target"></param>
    public void Controll(Transform target)
    {
        float axis = Input.GetAxis("Horizontal2") * Time.deltaTime * MOVE_SPEED;
        if (axis > 0) horizontalAngle += ROTATE_SPEED * Time.deltaTime;
        if (axis < 0) horizontalAngle -= ROTATE_SPEED * Time.deltaTime;
        float axis2 = Input.GetAxis("Vertical2") * Time.deltaTime * MOVE_SPEED;
        if (axis2 > 0) verticalAngle += ROTATE_SPEED * Time.deltaTime * 0.5f;
        if (axis2 < 0) verticalAngle -= ROTATE_SPEED * Time.deltaTime * 0.5f;

        transform.position = new Vector3(
            Mathf.Sin(horizontalAngle) * DISTANCE,
            Mathf.Cos(verticalAngle) * DISTANCE,
            Mathf.Cos(horizontalAngle) * DISTANCE) + (target.position + new Vector3(0, HIGHT, 0));

        Vector3 relativePos = (target.position + new Vector3(0, HIGHT, 0)) - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;

    }
}
