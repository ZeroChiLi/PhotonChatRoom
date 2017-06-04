using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common.Code;
using Common.Dto;

//聊天房间信息详细处理
public class ChatView : MonoBehaviour
{
    public GameObject loginCanvas;      //登录面板
    public GameObject chatCanvas;       //聊天面板

    public Text textContent;
    public InputField inputField;

    public Transform accountParent;             //用户列表
    public GameObject accountTestPerfab;        //用户名字预设

    private Dictionary<string, GameObject> dtoObjDict = new Dictionary<string, GameObject>();
    private Dictionary<byte, object> parameters = new Dictionary<byte, object>();

    public void Init(RoomDto room)
    {
        loginCanvas.SetActive(false);   //隐藏登录面板
        chatCanvas.SetActive(true);     //显示聊天面板

        textContent.text = "";
        foreach (var item in room.AccountList)
            AddAccount(item);
    }

    //添加用户
    public bool AddAccount(AccountDto accountDto)
    {
        if (dtoObjDict.ContainsKey(accountDto.AccountName))
            return false;
        GameObject accountItem = Instantiate(accountTestPerfab);
        accountItem.GetComponent<Text>().text = accountDto.AccountName;
        accountItem.transform.SetParent(accountParent);             //添加到列表
        dtoObjDict.Add(accountDto.AccountName, accountItem);
        return true;
    }

    //新用户提示
    public void ShowNewAccountTip(string accountName)
    {
        textContent.text += string.Format("\n----用户“ {0} ”进入聊天室----", accountName);
    }

    //别的用户离开
    public void SomeOneLeave(AccountDto accountDto)
    {
        if (!dtoObjDict.ContainsKey(accountDto.AccountName))
            return;

        textContent.text += string.Format("\n----用户“ {0} ”离开聊天室----", accountDto.AccountName);
        Destroy(dtoObjDict[accountDto.AccountName]);                //先Destroy
        dtoObjDict.Remove(accountDto.AccountName);                  //再移出字典
    }

    //更新聊天信息
    public void Append(string text)
    {
        textContent.text += "\n" + text;
    }

    //清除输入框
    public void ClearInputField()
    {
        inputField.text = "";
    }

    //发送输入框信息
    public void OnSend()
    {
        if (string.IsNullOrEmpty(inputField.text))
            return;
        parameters[0] = inputField.text;
        PhotonManager.Instance.OnOperationRequest((byte)OpCode.Room,parameters,(byte)RoomCode.Talk);
        ClearInputField();              //清除输入框
    }

    //退出聊天室
    public void OnReturn()
    {
        chatCanvas.SetActive(false);
        loginCanvas.SetActive(true);
        PhotonManager.Instance.OnOperationRequest((byte)OpCode.Room, parameters, (byte)RoomCode.Leave);
    }
}
