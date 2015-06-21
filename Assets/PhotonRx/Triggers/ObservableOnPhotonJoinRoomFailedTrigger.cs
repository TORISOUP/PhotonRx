using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnPhotonJoinRoomFailedTrigger : ObservableTriggerBase
    {
        private Subject<Unit> onPhotonJoinRoomFailed;
        private Subject<object[]> onPhotonJoinRoomFailedWithLog;

        void OnPhotonJoinRoomFailed()
        {
            if (onPhotonJoinRoomFailed != null) onPhotonJoinRoomFailed.OnNext(Unit.Default);
            
        }
        void OnPhotonJoinRoomFailed(object[] log)
        {
            if(onPhotonJoinRoomFailedWithLog!=null) onPhotonJoinRoomFailedWithLog.OnNext(log);
        }

        /// <summary>
        /// JoinRoomに失敗したことを通知する
        /// </summary>
        public IObservable<Unit> OnPhotonJoinRoomFailedAsObservable()
        {
            return onPhotonJoinRoomFailed ?? (onPhotonJoinRoomFailed = new Subject<Unit>());
        }

        /// <summary>
        /// JoinRoomに失敗したことを通知する
        /// PhotonNetwork.logLevel を PhotonLogLevel.Informational以上に設定している場合はこちらを使う
        /// </summary>
        public IObservable<object[]> OnPhotonJoinRoomFailedWithLogAsObservable()
        {
            return onPhotonJoinRoomFailedWithLog ?? (onPhotonJoinRoomFailedWithLog = new Subject<object[]>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onPhotonJoinRoomFailed != null) onPhotonJoinRoomFailed.OnCompleted();
            if (onPhotonJoinRoomFailedWithLog != null) onPhotonJoinRoomFailedWithLog.OnCompleted();
        }
    }
}
