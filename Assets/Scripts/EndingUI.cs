using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MainButtonClicked()
    {
        NetworkManager.instance.ExitRoom();
    }
}
