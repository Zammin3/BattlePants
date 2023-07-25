using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantsGamePlayer : MonoBehaviour
{
    private void Start()
    {
        Camera cam = Camera.main;
        cam.transform.SetParent(transform);
        cam.transform.localPosition = new Vector3(0f, 0f, -10f);
    }

}
