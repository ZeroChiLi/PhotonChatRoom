using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Code.Common;

public class AccountReceiver : MonoBehaviour, IReceiver
{
    public void OnReceive(byte subCode, OperationResponse response)
    {
        AccountCode code = (AccountCode)subCode;
        switch (code)
        {
            case AccountCode.Register:
                if (response.ReturnCode == 0)
                {
                    //关闭注册面板。
                    SendMessage("OnHideRegisterPanel");
                }
                break;
            case AccountCode.Login:
                if(response.ReturnCode == 0)
                {
                    //显示聊天UI
                }
                break;
            default:
                break;
        }
    }
}
