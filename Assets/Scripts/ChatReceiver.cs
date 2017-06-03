using UnityEngine;
using ExitGames.Client.Photon;
using Common.Code;
using Common.Dto;

//聊天房间的信息处理
public class ChatReceiver : MonoBehaviour ,IReceiver
{
    public GameObject loginCanvas;      //登录面板
    public GameObject chatCanvas;       //聊天面板

    public ChatView chatView;

    public void OnReceive(byte subCode, OperationResponse response)
    {
        switch ((ChatCode)subCode)
        {
            case ChatCode.Enter:                    //本人进入房间处理
                if (response.ReturnCode == 0)
                {
                    loginCanvas.SetActive(false);   //隐藏登录面板
                    chatCanvas.SetActive(true);     //显示聊天面板

                    //获取房间信息并初始化
                    chatView.Init(GetResponseFromJson<RoomDto>(response));
                }
                break;
            case ChatCode.Add:                      //房间有新用户处理
                chatView.AddAccount(GetResponseFromJson<AccountDto>(response));
                break;
            case ChatCode.Talk:                     //房间有人说话处理
                string text = response.Parameters[0].ToString();
                chatView.Append(text);
                break;
            case ChatCode.Leave:                    //房间有人离开处理
                chatView.LeaveRoom(GetResponseFromJson<AccountDto>(response));
                break;
            default:
                break;
        }
    }

    //从获取到的信息中提取出Dto
    private T GetResponseFromJson<T>(OperationResponse response)
    {
        return JsonUtility.FromJson<T>(response.Parameters[0].ToString());
    } 

}
