using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common.Dto;
using Common.Code;

//用户信息详细处理
public class AccountView : MonoBehaviour
{
    public InputField loginAccountInput;
    public InputField loginPasswordInput;

    public InputField registerAccountInput;
    public InputField registerPasswordInput;

    //注册面板
    public GameObject registerPanel;

    //登录请求响应
    public void OnLoginClick()
    {
        if (string.IsNullOrEmpty(loginAccountInput.text) || string.IsNullOrEmpty(loginPasswordInput.text))
        {
            Debug.Log("用户或密码不能为空。");
            return;
        }
        SendAccountRequest(loginAccountInput.text, loginPasswordInput.text, (byte)AccountCode.Login);
    }

    //注册请求响应
    public void OnRegisterClick()
    {
        if (string.IsNullOrEmpty(registerAccountInput.text) || string.IsNullOrEmpty(registerPasswordInput.text))
        {
            Debug.Log("输入不能为空。");
            return;
        }
        SendAccountRequest(registerAccountInput.text, registerPasswordInput.text, (byte)AccountCode.Register);
    }

    //发送用户信息
    public void SendAccountRequest(string accountName,string password,byte subCode)
    {
        AccountDto dto = new AccountDto();
        dto.AccountName = accountName;
        dto.Password = password;
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        parameters[0] = JsonUtility.ToJson(dto);
        PhotonManager.Instance.OnOperationRequest((byte)OpCode.Account, parameters, subCode);
    }

    //显示注册面板
    public void OnShowRegisterPanel()
    {
        registerPanel.SetActive(true);
    }

    //隐藏注册面板
    public void OnHideRegisterPanel()
    {
        registerPanel.SetActive(false);
    }


}
