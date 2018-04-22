# PhotonRx

PhotonRxは、[Photon Unity Networking](https://www-jp.exitgames.com/ja/PUN)を[UniRx](https://github.com/neuecc/UniRx)のObservableとして扱えるようにしたライブラリです。
Photonのコールバック群をObservableTriggerとして定義しています。

## 導入方法

PhotonRx.unitypackageをプロジェクトにインポートしてください。
[Photon Unity Networking](https://www-jp.exitgames.com/ja/PUN)と[UniRx](https://github.com/neuecc/UniRx)は含んでいないので、これらは自身でインポートする必要があります。

## 使い方

ObservableTriggerと同様に、this.xxxAsObservableでストリームを取得することができます。

```csharp
using System;
using UnityEngine;
using System.Collections;
using PhotonRx;
using UniRx;

public class SubscribeConnection : MonoBehaviour
{
    private void Start()
    {
        this.OnConnectedToPhotonAsObservable()
            .Subscribe(_ => Debug.Log("サーバへ接続成功"));

        this.OnFailedToConnectToPhotonAsObservable()
            .Subscribe(_ => Debug.Log("サーバへの接続失敗"));
    }
}

```

## Task support

`.NET 4.6`モードであればログインおよび部屋に参加する場合に`Task`を使うことができます。
`Photon.Task`以下のメソッドを利用してください。

```cs
using UnityEngine;
using System.Threading.Tasks;
using PhotonRx;

public class TaskSample : MonoBehaviour
{
    async void Start()
    {
        PhotonNetwork.autoJoinLobby = true;

        var isConnected = await Connect();

        if (!isConnected) return;

        var isJoined = await JoinRoom();

        Debug.Log(isJoined);

    }

    private async Task<bool> Connect()
    {
        // サーバに接続
        var connect = await PhotoTask.ConnectUsingSettings("v1");

        if (connect.IsFailure)
        {
            // 失敗
            Debug.LogError(connect.ToFailure.Value);
        }

        return connect.IsSuccess;
    }

    private async Task<bool> JoinRoom()
    {
        // 適当な部屋に参加する
        var randomJoined = await PhotoTask.JoinRandomRoom();

        // 成功なら終わり
        if (randomJoined.IsSuccess) return true;

        // 部屋を作って参加する
        var created = await PhotoTask.CreateRoom("test", null, null, null);

        if (!created.IsSuccess)
        {
            //失敗
            Debug.LogError(created.ToFailure.Value);
        }

        return created.IsSuccess;
    }
}
```

## 配布ライセンス

MIT Licenseで公開します


## 権利表記

UniRx Copyright (c) 2014 Yoshifumi Kawai  https://github.com/neuecc/UniRx/blob/master/LICENSE
