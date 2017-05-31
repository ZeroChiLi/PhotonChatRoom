using UnityEngine;
using ExitGames.Client.Photon;
using System.Collections.Generic;

public class PhotonTest : MonoBehaviour, IPhotonPeerListener
{
    //连接服务器
    private PhotonPeer peer;
    private ConnectionProtocol protocol = ConnectionProtocol.Udp;
    private string serverAddress = "127.0.0.1:5055";
    private string applicationName = "MyGameServer";

    private bool connected = false;

    void Awake()
    {
        peer = new PhotonPeer(this, protocol);
    }

    void Update()
    {
        if (!connected)
            peer.Connect(serverAddress, applicationName);
        peer.Service();
    }

    private void OnDestroy()
    {
        if (connected)
            peer.Disconnect();
    }


    public void DebugReturn(DebugLevel level, string message)
    {
        if (connected)
            Debug.Log(message);
    }

    public void OnEvent(EventData eventData)
    {
        Debug.Log("OnEvent() Fucking Called : " + eventData.ToStringFull());
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        Debug.Log("OnOperationResponse() Fucking Called : " + operationResponse.ToStringFull());
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        Debug.Log("OnOperationResponse() Fucking Called : " + statusCode.ToString());
        switch (statusCode)
        {
            case StatusCode.Connect:
                connected = true;
                break;
            case StatusCode.Disconnect:
                connected = false;
                break;
            case StatusCode.Exception:
                connected = false;
                break;
        }
    }

}
