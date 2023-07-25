using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnterUI : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField nicknameInputField;
    [SerializeField]
    private GameObject enterRoomUI;

    public void OnClickEnterGameButton()
    {
        if(nicknameInputField.text != "")
        {
            PlayerSettings.nickname = nicknameInputField.text; 
        }
        else
        {
            nicknameInputField.GetComponent<Animator>().SetTrigger("on");
        }
    }

    public void CreateRoom()
    {
        if (nicknameInputField.text != "")
        {
        }
    }

    public void EnterRoom()
    {
        if (nicknameInputField.text != "")
        {
        }
    }
}
