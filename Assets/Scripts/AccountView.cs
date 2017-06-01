using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common.Dto;
using Code.Common;

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

        AccountDto dto = new AccountDto();
        dto.Account = inAccountLogin.text;
        dto.Password = inPasswordLogin.text;
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        parameters[0] = JsonUtility.ToJson(dto);
        PhotonManager.Instance.OnOperationRequest((byte)OpCode.Account, parameters,(byte)AccountCode.Login);
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

        AccountDto dto = new AccountDto();
        dto.Account = inAccountLogin.text;
        dto.Password = inPasswordLogin.text;
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        parameters[0] = JsonUtility.ToJson(dto);
        PhotonManager.Instance.OnOperationRequest((byte)OpCode.Account, parameters, (byte)AccountCode.Register);

    }


}
