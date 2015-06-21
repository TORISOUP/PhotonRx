#PhotonRx

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

## 配布ライセンス

MIT Licenseで公開します


## 使用ライセンス表記

PhotonRxはUniRxをベースに作成しています  
Copyright (c) 2014 Yoshifumi Kawai https://github.com/neuecc/UniRx/blob/master/LICENSE