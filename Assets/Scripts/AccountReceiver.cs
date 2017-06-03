using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Common.Code;

//登录注册的信息处理
public class AccountReceiver : MonoBehaviour, IReceiver
{
    public AccountView accountView;

    public void OnReceive(byte subCode, OperationResponse response)
    {
        switch ((AccountCode)subCode)
        {
            case AccountCode.Register:
                if (response.ReturnCode == 0)               //返回码正确时
                {
                    accountView.OnHideRegisterPanel();      //关闭注册面板。
                }
                break;
            case AccountCode.Login:
                if (response.ReturnCode == 0)               //返回码正确时
                {
                    //显示聊天UI
                    PhotonManager.Instance.OnOperationRequest((byte)OpCode.Chat, new Dictionary<byte, object>(), (byte)ChatCode.Enter);                     
                }
                break;
            default:
                break;
        }
    }
}
