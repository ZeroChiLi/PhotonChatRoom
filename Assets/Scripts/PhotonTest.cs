using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using System;

public class PhotonTest : MonoBehaviour,IPhotonPeerListener
{
    PhotonPeer peer;

    void Start()
    {
        peer = new PhotonPeer(this, ConnectionProtocol.Udp);
        peer.Connect("127.0.0.1:5055","Lite");
    }

    void Update()
    {
        peer.Service();
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        throw new NotImplementedException();
    }

    public void OnEvent(EventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        throw new NotImplementedException();
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        switch(statusCode)
        {
            case StatusCode.Connect:
                Debug.Log("Connect~~~~");
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
