using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountView : MonoBehaviour
{
    public InputField inAccountLogin;
    public InputField inPasswordLogin;

    public InputField inAccountRegister;
    public InputField inPasswordRegister;

    public GameObject registerPanel;

    public void OnLoginClick()
    {
        if (string.IsNullOrEmpty(inAccountLogin.text) || string.IsNullOrEmpty(inPasswordLogin.text))
        {
            Debug.Log("输入不能为空。");
            return;
        }
    }

    public void ShowRegisterPanel()
    {
        registerPanel.SetActive(true);
    }

    public void OnRegisterClick()
    {
        if (string.IsNullOrEmpty(inAccountRegister.text) || string.IsNullOrEmpty(inPasswordRegister.text))
        {
            Debug.Log("输入不能为空。");
            return;
        }
    }


}
