using System;
using UnityEngine;
using System.Collections;
using PhotonRx;
using UniRx;
using UnityEngine.UI;

namespace PhotonRx.Sample
{

    public class LoginAndJoinRoom : MonoBehaviour
    {
        [SerializeField] private Text logText;
        [SerializeField] private Button connectToServerButton;
        [SerializeField] private Button joinToRoomButton;

        private void Start()
        {
            PhotonNetwork.autoJoinLobby = true;
            Observable.FromCoroutine<string>(observer => ConnectAndJoinCoroutine(observer))
                .Retry(3) //3回までリトライ
                .Subscribe(x => logText.text += String.Format("[{0}] {1}\n", DateTime.Now, x));
        }

        private IEnumerator ConnectAndJoinCoroutine(IObserver<string> observer)
        {
            observer.OnNext("Ready...");

            //開始ボタンが押されるまで待機
            yield return connectToServerButton.OnClickAsObservable().FirstOrDefault().StartAsCoroutine();

            observer.OnNext("ConnectToServer...");

            //-----------ここからロビーへの接続------------

            //ロビーへの接続結果を通知するストリームを作成
            var loginStream = CreateConnectLobbyObservable();
            loginStream.Connect();

            //接続開始
            PhotonNetwork.ConnectUsingSettings("0.1");

            //ロビーへの参加通知を待ち受ける
            object result = default(object);
            yield return loginStream.StartAsCoroutine(x => result = x, ex => { });

            //接続結果が失敗だった場合は処理終了
            if (result is DisconnectCause)
            {
                observer.OnNext(((DisconnectCause) result).ToString());
                observer.OnNext("ConnectionFailed");
                observer.OnError(null);
                yield break;
            }

            observer.OnNext("ConnectSuccess");

            //-----------ここから部屋の接続------------

            //ボタンクリックの待受
            yield return joinToRoomButton.OnClickAsObservable().FirstOrDefault().StartAsCoroutine();

            //部屋への接続を待機するストリーム
            var joinRandomRoomStream = CreateJoinRoomObservable();
            joinRandomRoomStream.Connect();

            observer.OnNext("JoinRandomRoom...");

            //ランダムな部屋に参加
            PhotonNetwork.JoinRandomRoom();

            //部屋の参加を待ち受ける
            bool isJoined = false;
            yield return joinRandomRoomStream.StartAsCoroutine(x => isJoined = x);

            if (isJoined)
            {
                //参加成功したら終了
                observer.OnNext("JoinRandomRoomSuccess");
                PhotonNetwork.Disconnect();
                yield break;
            }

            observer.OnNext("JoinRandomRoomFailed");

            //-----------ここから部屋の新規作成------------

            //部屋への参加に失敗した場合は新しく部屋を作る
            var joinRoom = CreateJoinRoomObservable();
            joinRoom.Connect();

            observer.OnNext("CreateNewRoom");
            PhotonNetwork.CreateRoom(null);

            //部屋を作って参加を待機する
            isJoined = false;
            yield return joinRoom.StartAsCoroutine(x => isJoined = x);

            observer.OnNext(isJoined ? "JoinRoomSuccess" : "JoinRoomFailed");
            PhotonNetwork.Disconnect();
            observer.OnCompleted();
        }

        /// <summary>
        /// ロビーへの接続を監視するストリーム
        /// </summary>
        private IConnectableObservable<object> CreateConnectLobbyObservable()
        {
            return this.OnJoinedLobbyAsObservable().Cast(default(object))
                    .Merge(this.OnFailedToConnectToPhotonAsObservable().Cast(default(object)))
                    .FirstOrDefault() //OnCompletedを発火させるため
                    .PublishLast();   //PhotonNetwork.Connectを呼び出すより前にストリームを稼働させるため
        }

        /// <summary>
        /// 部屋への参加を監視するストリーム
        /// </summary>
        private IConnectableObservable<bool> CreateJoinRoomObservable()
        {
            return Observable.Merge(
                this.OnPhotonRandomJoinFailedAsObservable().Select(_ => false),
                this.OnPhotonJoinRoomFailedAsObservable().Select(_ => false),
                this.OnJoinedRoomAsObservable().Select(_ => true))
                .FirstOrDefault()
                .PublishLast();
        }

    }
}