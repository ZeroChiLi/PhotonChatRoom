using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;

interface IReceiver
{
    //介绍服务器响应
    void OnReceive(byte subCode, OperationResponse response);
}
