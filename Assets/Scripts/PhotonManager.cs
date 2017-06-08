using UnityEngine;
using ExitGames.Client.Photon;
using System.Collections.Generic;
using Common.Code;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviour, IPhotonPeerListener
{
    private static PhotonManager instance = null;
    public static PhotonManager Instance { get { return instance; } }

    private PhotonPeer peer;                                        //连接服务器
    private ConnectionProtocol protocol = ConnectionProtocol.Udp;   //协议是UDP，这是可靠的UDP
    private string serverAddress = "127.0.0.1:5055";                //本机IP，5055端口
    private string applicationName = "MyGameServer";                //服务器应用名称，PhotonServer配置的

    private bool connected = false;                                 //是否正在连接

    public AccountReceiver accountReceiver;                         //帐号处理
    public ChatReceiver chatRceiver;                                //聊天信息接收      

    public string MasterName;

    public Text tipText;      

    private void Awake()
    {
        instance = this;
        peer = new PhotonPeer(this, protocol);

        DontDestroyOnLoad(accountReceiver);
    }

    private void Update()
    {
        if (!connected)
            peer.Connect(serverAddress, applicationName);           //连接服务器
        peer.Service();                                             //获取服务，持续调用才能接受信息
    }

    private void OnDestroy()
    {
        peer.Disconnect();
    }

    //向服务器发请求
    public void OnOperationRequest(byte opCode, Dictionary<byte, object> parameters = null, byte SubCode = 0)
    {
        //规定'80'对应的是子操作码
        parameters[80] = SubCode;
        peer.OpCustom(opCode, parameters, true);
    }

    //输出调试信息
    public void DebugReturn(DebugLevel level, string message)
    {
        if (connected)
            Debug.Log(message);
    }

    //事件响应
    public void OnEvent(EventData eventData)
    {
        Debug.Log(eventData.ToStringFull());
    }

    //服务端发送过来的响应
    public void OnOperationResponse(OperationResponse response)
    {
        Debug.Log(response.ToStringFull());
        tipText.text = response.DebugMessage;
        byte subCode = (byte)response.Parameters[80];

        switch ((OpCode)response.OperationCode)         //判断发过来操作码
        {
            case OpCode.Account:
                accountReceiver.OnReceive(subCode, response);
                break;
            case OpCode.Room:
                chatRceiver.OnReceive(subCode,response); 
                break;
            default:
                break;
        }
    }

    //状态改变时调用
    public void OnStatusChanged(StatusCode statusCode)
    {
        Debug.Log(statusCode.ToString());
        switch (statusCode)
        {
            case StatusCode.Connect:
                connected = true;
                if(tipText != null)
                    tipText.text = "连接服务器成功。";
                break;
            case StatusCode.Disconnect:
                connected = false;
                if (tipText != null)
                    tipText.text = "断开服务器连接。";
                break;
        }
    }

}
