using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    private const float MOVE_SPEED = 1.0f;
    private const float ROTATE_SPEED = 3.0f;
    private const float HIGHT = 2.0f;

    private float angle;

    private void Start()
    {
        angle = 180.0f * (Mathf.PI / 180.0f);
    }

    public void Following(Vector3 pos)
    {
        transform.position = new Vector3(pos.x, pos.y+HIGHT, pos.z);
    }

    public void Rotation(Transform target)
    {
        float axis = Input.GetAxis("Horizontal2") * Time.deltaTime * MOVE_SPEED;
        if (axis > 0) angle += ROTATE_SPEED * Time.deltaTime;
        if (axis < 0) angle -= ROTATE_SPEED * Time.deltaTime;

        transform.position = new Vector3(
            Mathf.Sin(angle) * 4.0f,
            transform.position.y,
            Mathf.Cos(angle) * 4.0f) + target.position;

        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;
    }
}
