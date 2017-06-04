using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common.Code;
using Common.Dto;

//聊天房间信息详细处理
public class ChatView : MonoBehaviour
{
    public Text textContent;
    public InputField inputField;

    public Transform accountParent;             //用户列表
    public GameObject accountTestPerfab;        //用户名字预设

    private Dictionary<string, GameObject> dtoObjDict = new Dictionary<string, GameObject>();

    public void Init(RoomDto room)
    {
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

    //用户离开
    public void LeaveRoom(AccountDto accountDto)
    {
        if (!dtoObjDict.ContainsKey(accountDto.AccountName))
            return;

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
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        parameters[0] = inputField.text;
        PhotonManager.Instance.OnOperationRequest((byte)OpCode.Room,parameters,(byte)RoomCode.Talk);
        ClearInputField();              //清除输入框
    }
}
