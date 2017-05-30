using UnityEngine;
using ExitGames.Client.Photon;
using System.Collections.Generic;

public class PhotonTest : MonoBehaviour, IPhotonPeerListener
{
    PhotonPeer peer;

    void Start()
    {
        peer = new PhotonPeer(this, ConnectionProtocol.Udp);
        peer.Connect("127.0.0.1:5055", "Lite");
    }

    void Update()
    {
        peer.Service();
    }

    void SendMessage()
    {
        Dictionary<byte, object> para = new Dictionary<byte, object>();
        para[255] = "1";
        peer.OpCustom(255, para, true);

    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 50, 100, 100, 30), "Set Game ID"))
        {
            SendMessage();
        }
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log(message);
    }

    public void OnEvent(EventData eventData)
    {
        Debug.Log("OnEvent() Fucking Called : " + eventData.ToStringFull());
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        Debug.Log("Server Reply You,Mother Fucker : " + operationResponse.ToStringFull());
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        switch (statusCode)
        {
            case StatusCode.Connect:
                Debug.Log("Connect~~~~ ");
                break;
            case StatusCode.Disconnect:
                Debug.Log("Disconnect~~~~~");
                break;
            case StatusCode.Exception:
                Debug.Log("Exception~~~~~");
                break;
        }
    }

}
