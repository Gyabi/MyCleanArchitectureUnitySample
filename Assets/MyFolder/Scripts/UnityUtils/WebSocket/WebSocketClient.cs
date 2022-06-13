using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Net;
using UniRx;

public class WebSocketClient : MonoBehaviour
{
    // プレイヤー生成通知
    private Subject<int> _onPlayerCreateSubject = new Subject<int>();
    public Subject<int> OnPlayerCreateSubject {
        get { return _onPlayerCreateSubject; }
    }

    bool _canCreatePlayer = false;
    PacketJsonData _createPlayerPacket;

    // string url = "ws://fastapi-websocket-server:8000/ws";
    string url = "ws://localhost:8000/ws";
    WebSocket webSocket;
    public void Connect()
    {
        webSocket = new WebSocket(url);
        webSocket.OnOpen += (sender, e) =>
        {
            Debug.Log("Connected");
        };

        webSocket.OnMessage += (sender, e) =>
        {
            Debug.Log("Received: " + e.Data);
            // json文字列→Dict変換
            PacketJsonData packetJsonData = JsonUtility.FromJson<PacketJsonData>(e.Data);
            
            // 初回通信か判定
            if(packetJsonData.connection_message)
            {
                this._canCreatePlayer = true;
                this._createPlayerPacket = packetJsonData;
            }
        };
        webSocket.OnError += (sender, e) =>
        {
            Debug.Log("Error: " + e.Message);
        };

        webSocket.OnClose += (sender, e) =>
        {
            Debug.Log("Closed");
        };

        webSocket.Connect();
    }

    public void SendJson(PacketJsonData packetJsonData)
    {
        Debug.Log("SendJson");
        string json = JsonUtility.ToJson(packetJsonData);
        Debug.Log(json);
        webSocket.Send(json);
    }

    private void Update()
    {
        if(this._canCreatePlayer)
        {
            // プレイヤー生成通知(メッセージの受信が別スレッドなせいでnextを発火できない)
            // https://befool.co.jp/blog/8823-scholar/unirx-from-event-args/
            // 上記で解決できるかも。。。？
            OnPlayerCreateSubject.OnNext(this._createPlayerPacket.id);
            this._canCreatePlayer = false;
            this._createPlayerPacket = null;
        }
    }

    void OnDestroy()
    {
        webSocket.Close();
    }
}
