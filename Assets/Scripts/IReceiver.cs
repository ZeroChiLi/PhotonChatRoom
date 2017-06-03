using ExitGames.Client.Photon;

interface IReceiver
{
    //接受服务器响应
    void OnReceive(byte subCode, OperationResponse response);
}
