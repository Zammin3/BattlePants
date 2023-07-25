using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//닉네임 입력하는 창
public class EnterRoomUI : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField nicknameInputField;
    [SerializeField]
    private GameObject enterRoomUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickEnterGameButton()
    {
        if (nicknameInputField.text != "")
        {
            SceneManager.LoadScene("Game Ready");

            NetworkManager.instance.Connect(nicknameInputField.text);
        }
        else
        {
            nicknameInputField.GetComponent<Animator>().SetTrigger("on");
        }
    }



    public void CreateRoom2()
    {
        if (nicknameInputField.text != "")
        {
            SceneManager.LoadScene("Game Ready");

            NetworkManager.instance.Connect(nicknameInputField.text);
        }
        else
        {
            nicknameInputField.GetComponent<Animator>().SetTrigger("on");
        }
    }
}
