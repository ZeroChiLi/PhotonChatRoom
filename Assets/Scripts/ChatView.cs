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

    public void Init(RoomDto room)
    {
        textRecord.text = "您已经入聊天室.";
        foreach (var item in room.accountList)
            AddAccount(item);
    }

    private void AddAccount(AccountDto accountDto)
    {
        Debug.Log("add the fucking account here. " + accountDto.Account);
        GameObject go = Instantiate(accountTestPerfab);
        go.GetComponent<Text>().text = accountDto.Account;
        go.transform.SetParent(accountParent);
    }

    public void OnSend()
    {
        if (string.IsNullOrEmpty(inTalk.text))
            return;
        Dictionary<byte, object> p = new Dictionary<byte, object>();
        PhotonManager.Instance.OnOperationRequest((byte)OpCode.Chat,p,(byte)ChatCode.Talk);
    }
}
