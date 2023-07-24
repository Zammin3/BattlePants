using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PantsGamePlayer : NetworkBehaviour
{
    private void Start()
    {
        if (hasAuthority)
        {
            Camera cam = Camera.main;
            cam.transform.SetParent(transform);
            cam.transform.localPosition = new Vector3(0f, 0f, -10f);
        }
    }

}
