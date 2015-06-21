using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnPhotonRandomJoinFailedTrigger : ObservableTriggerBase
    {
        private Subject<Unit> onPhotonRandomJoinRoomFailed;
        private Subject<object[]> onPhotonRandomJoinRoomFailedWithLog;

        void OnPhotonRandomJoinFailed()
        {
            if (onPhotonRandomJoinRoomFailed != null) onPhotonRandomJoinRoomFailed.OnNext(Unit.Default);
            
        }
        void OnPhotonRandomJoinFailed(object[] log)
        {
            if(onPhotonRandomJoinRoomFailedWithLog!=null) onPhotonRandomJoinRoomFailedWithLog.OnNext(log);
        }

        /// <summary>
        /// JoinRandomに失敗したことを通知する
        /// </summary>
        public IObservable<Unit> OnPhotonRandomJoinFailedFailedAsObservable()
        {
            return onPhotonRandomJoinRoomFailed ?? (onPhotonRandomJoinRoomFailed = new Subject<Unit>());
        }

        /// <summary>
        /// JoinRandomに失敗したことを通知する
        /// PhotonNetwork.logLevel を PhotonLogLevel.Informational以上に設定している場合はこちらを使う
        /// </summary>
        public IObservable<object[]> OnPhotonRandomJoinFailedWithLogAsObservable()
        {
            return onPhotonRandomJoinRoomFailedWithLog ?? (onPhotonRandomJoinRoomFailedWithLog = new Subject<object[]>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onPhotonRandomJoinRoomFailed != null) onPhotonRandomJoinRoomFailed.OnCompleted();
            if (onPhotonRandomJoinRoomFailedWithLog != null) onPhotonRandomJoinRoomFailedWithLog.OnCompleted();
        }
    }
}
