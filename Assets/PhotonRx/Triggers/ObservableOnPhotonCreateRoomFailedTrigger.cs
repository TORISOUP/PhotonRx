using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnPhotonCreateRoomFailedTrigger : ObservableTriggerBase
    {
        private Subject<Unit> onPhotonCreateRoomFailed;
        private Subject<object[]> onPhotonCreateRoomFailedWithLog;

        private void OnPhotonCreateRoomFailed()
        {
            if (onPhotonCreateRoomFailed != null) onPhotonCreateRoomFailed.OnNext(Unit.Default);
        }

        private void OnPhotonCreateRoomFailed(object[] log)
        {
            if (onPhotonCreateRoomFailedWithLog != null) onPhotonCreateRoomFailedWithLog.OnNext(log);
        }

        /// <summary>
        /// CreateRoomの呼び出しが失敗したことを通知する
        /// </summary>
        public IObservable<Unit> OnPhotonCreateRoomFailedAsObservable()
        {
            return onPhotonCreateRoomFailed ?? (onPhotonCreateRoomFailed = new Subject<Unit>());
        }

        /// <summary>
        /// CreateRoomの呼び出しが失敗したことを通知する
        /// PhotonNetwork.logLevel を PhotonLogLevel.Informational以上に設定している場合はこちらを使う
        /// </summary>
        public IObservable<object[]> OnPhotonCreateRoomFailedWithLogAsObservable()
        {
            return onPhotonCreateRoomFailedWithLog ?? (onPhotonCreateRoomFailedWithLog = new Subject<object[]>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onPhotonCreateRoomFailed != null)
            {
                onPhotonCreateRoomFailed.OnCompleted();
            }
            if (onPhotonCreateRoomFailedWithLog != null)
            {
                onPhotonCreateRoomFailedWithLog.OnCompleted();
            }
        }
    }
}
