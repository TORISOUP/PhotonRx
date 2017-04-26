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
        private Subject<FailureReason> onPhotonRandomJoinRoomFailedWithLog;

        void OnPhotonRandomJoinFailed()
        {
            if (onPhotonRandomJoinRoomFailed != null) onPhotonRandomJoinRoomFailed.OnNext(Unit.Default);

        }
        void OnPhotonRandomJoinFailed(object[] log)
        {
            if (onPhotonRandomJoinRoomFailedWithLog != null)
            {
                var code = (short)log[0];
                var message = log[1] as string;
                onPhotonRandomJoinRoomFailedWithLog.OnNext(new FailureReason(code, message));
            }
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
        public IObservable<FailureReason> OnPhotonRandomJoinFailedWithLogAsObservable()
        {
            return onPhotonRandomJoinRoomFailedWithLog ?? (onPhotonRandomJoinRoomFailedWithLog = new Subject<FailureReason>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onPhotonRandomJoinRoomFailed != null) onPhotonRandomJoinRoomFailed.OnCompleted();
            if (onPhotonRandomJoinRoomFailedWithLog != null) onPhotonRandomJoinRoomFailedWithLog.OnCompleted();
        }
    }
}
