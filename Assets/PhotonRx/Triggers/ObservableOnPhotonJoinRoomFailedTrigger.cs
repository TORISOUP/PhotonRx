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
        private Subject<FailureReason> onPhotonJoinRoomFailedWithLog;

        void OnPhotonJoinRoomFailed()
        {
            if (onPhotonJoinRoomFailed != null) onPhotonJoinRoomFailed.OnNext(Unit.Default);

        }
        void OnPhotonJoinRoomFailed(object[] log)
        {
            if (onPhotonJoinRoomFailedWithLog != null)
            {
                var code = (short)log[0];
                var message = log[1] as string;
                onPhotonJoinRoomFailedWithLog.OnNext(new FailureReason(code, message));
            }
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
        public IObservable<FailureReason> OnPhotonJoinRoomFailedWithLogAsObservable()
        {
            return onPhotonJoinRoomFailedWithLog ?? (onPhotonJoinRoomFailedWithLog = new Subject<FailureReason>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onPhotonJoinRoomFailed != null) onPhotonJoinRoomFailed.OnCompleted();
            if (onPhotonJoinRoomFailedWithLog != null) onPhotonJoinRoomFailedWithLog.OnCompleted();
        }
    }
}
