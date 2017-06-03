using Code.Common;
using Common.Code;
using Common.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatView : MonoBehaviour
{
    public Text textRecord;
    public InputField inTalk;

    public Transform accountParent;
    public GameObject accountTestPerfab;

    private Dictionary<string, GameObject> dtoGoDict = new Dictionary<string, GameObject>();

    public void Init(RoomDto room)
    {
        textRecord.text = "您已经入聊天室.";
        foreach (var item in room.accountList)
            AddAccount(item);
    }

    //添加用户
    public void AddAccount(AccountDto accountDto)
    {
        if (dtoGoDict.ContainsKey(accountDto.Account))
            return;
        GameObject go = Instantiate(accountTestPerfab);
        go.GetComponent<Text>().text = accountDto.Account;
        go.transform.SetParent(accountParent);
        dtoGoDict.Add(accountDto.Account, go);
    }

    //用户离开
    public void LeaveRoom(AccountDto accountDto)
    {
        if (!dtoGoDict.ContainsKey(accountDto.Account))
            return;

        Destroy(dtoGoDict[accountDto.Account]);
        dtoGoDict.Remove(accountDto.Account);
    }

    //更新聊天信息
    public void Append(string text)
    {
        textRecord.text += "\n" + text;
    }

    //清除输入框
    public void ClearInputField()
    {
        inTalk.text = "";
    }

    //发送输入框信息
    public void OnSend()
    {
        if (string.IsNullOrEmpty(inTalk.text))
            return;
        Dictionary<byte, object> p = new Dictionary<byte, object>();
        p[0] = inTalk.text;
        PhotonManager.Instance.OnOperationRequest((byte)OpCode.Chat,p,(byte)ChatCode.Talk);
        ClearInputField();
    }
}
