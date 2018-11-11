# PhotonRx

PhotonRxは、[Photon Unity Networking](https://www-jp.exitgames.com/ja/PUN)を[UniRx](https://github.com/neuecc/UniRx)のObservableとして扱えるようにしたライブラリです。
Photonのコールバック群をObservableTriggerとして定義しています。

PhotonRx supports to [Photon Unity Networking](https://www-jp.exitgames.com/ja/PUN) as Observable([UniRx](https://github.com/neuecc/UniRx)).  


## 導入方法 (How to install)

PhotonRx.unitypackageをプロジェクトにインポートしてください。
[Photon Unity Networking](https://www-jp.exitgames.com/ja/PUN)と[UniRx](https://github.com/neuecc/UniRx)は含んでいないので、これらは自身でインポートする必要があります。

Include [PhotonRx.unitypackage](https://github.com/TORISOUP/PhotonRx/releases) to your Unity project.  
(PhotonRx dose NOT contain PUN and UniRx assets.)

## 使い方(How to use)

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
            .Subscribe(_ => Debug.Log("Success"));

        this.OnFailedToConnectToPhotonAsObservable()
            .Subscribe(_ => Debug.Log("Failure"));
    }
}

```

## Task support


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
        // Connect to server
        var connect = await PhotonTask.ConnectUsingSettings("v1");

        if (connect.IsFailure)
        {
            // Failure
            Debug.LogError(connect.ToFailure.Value);
        }

        return connect.IsSuccess;
    }

    private async Task<bool> JoinRoom()
    {
        // Join random room
        var randomJoined = await PhotonTask.JoinRandomRoom();

        // Success
        if (randomJoined.IsSuccess) return true;

        // Create new room
        var created = await PhotonTask.CreateRoom("test", null, null, null);

        if (!created.IsSuccess)
        {
            // Failure
            Debug.LogError(created.ToFailure.Value);
        }

        return created.IsSuccess;
    }
}
```

## LICENSE

MIT License.


## 権利表記

UniRx Copyright (c) 2014 Yoshifumi Kawai  https://github.com/neuecc/UniRx/blob/master/LICENSE
