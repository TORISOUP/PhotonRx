using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnPhotonRandomJoinFailedTrigger : ObservableTriggerBase
    {
        private Subject<FailureReason> onPhotonRandomJoinRoomFailedWithLog;

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
        public IObservable<FailureReason> OnPhotonRandomJoinFailedAsObservable()
        {
            return onPhotonRandomJoinRoomFailedWithLog ?? (onPhotonRandomJoinRoomFailedWithLog = new Subject<FailureReason>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onPhotonRandomJoinRoomFailedWithLog != null) onPhotonRandomJoinRoomFailedWithLog.OnCompleted();
        }
    }
}
