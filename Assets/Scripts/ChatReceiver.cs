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

                    RoomDto dto = JsonUtility.FromJson<RoomDto>(response.Parameters[0].ToString());
                    chatView.Init(dto);
                }
                break;
            case ChatCode.Talk:
                break;
            case ChatCode.Leave:
                break;
            default:
                break;
        }
    }

}
