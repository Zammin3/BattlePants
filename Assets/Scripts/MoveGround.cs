using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    public float maxDistance = 2f;

    private float initialPositionX;

    // Start is called before the first frame update
    void Start()
    {
        initialPositionX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float newPositionX = initialPositionX + Mathf.PingPong(Time.time * speed, maxDistance * 2) - maxDistance;
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
    }
}
