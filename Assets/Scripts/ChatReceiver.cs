using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Common.Code;
using Common.Dto;

public class ChatReceiver : MonoBehaviour ,IReceiver
{
    public GameObject loginCanvas;
    public GameObject chatCanvas;

    public ChatView chatView;

    public void OnReceive(byte subCode, OperationResponse response)
    {
        ChatCode code = (ChatCode)subCode;
        switch (code)
        {
            case ChatCode.Enter:
                if (response.ReturnCode == 0)
                {
                    loginCanvas.SetActive(false);
                    chatCanvas.SetActive(true);

                    RoomDto roomDto = JsonUtility.FromJson<RoomDto>(response.Parameters[0].ToString());
                    chatView.Init(roomDto);
                }
                break;
            case ChatCode.Add:
                AccountDto accountDto = JsonUtility.FromJson<AccountDto>(response.Parameters[0].ToString());
                chatView.AddAccount(accountDto);
                break;
            case ChatCode.Talk:
                string text = response.Parameters[0].ToString();
                chatView.Append(text);
                break;
            case ChatCode.Leave:
                AccountDto leaveDto = JsonUtility.FromJson<AccountDto>(response.Parameters[0].ToString());
                chatView.LeaveRoom(leaveDto);
                break;
            default:
                break;
        }
    }

}
