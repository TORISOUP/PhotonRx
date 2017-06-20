using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace PhotonRx.Sample
{
    public class CatchFailureReason : MonoBehaviour
    {
        [SerializeField]
        private Button RandomJoineButton;

        [SerializeField]
        private Text ResultText;

        void Start()
        {
            Debug.Log("サーバに接続中");
            RandomJoineButton.interactable = false;

            //部屋への参加に失敗した時に理由が取れることを確認する
            Observable.Create<Unit>(observer =>
            {
                //PhotonNetwork.ConnectUsingSettingsは非同期処理なことを考慮し、
                //確実に結果ストリームを受信できる状態にしてからConnectを実行する
                //(先にConnectしてからSubscribeだと値を取りこぼす可能性がありうる）
                var disposable = this.OnJoinedLobbyAsObservable().FirstOrDefault().Subscribe(observer);
                PhotonNetwork.ConnectUsingSettings("0.1");
                return disposable;
            }).Subscribe(_ =>
            {
                Debug.Log("サーバに接続完了");
                SetupUIs();
            });

        }

        void SetupUIs()
        {
            //部屋がないので絶対失敗するはず
            RandomJoineButton.OnClickAsObservable()
                .Subscribe(_ => PhotonNetwork.JoinRandomRoom());

            RandomJoineButton.interactable = true;

            this.OnPhotonRandomJoinFailedAsObservable()
                .Subscribe(reason =>
                {
                    var reasonText = string.Format("部屋に接続できませんでした　{0}\t{1}", reason.ErrorCode, reason.Message);
                    Debug.Log(reasonText);
                    ResultText.text = reasonText;
                });
        }

    }
}
