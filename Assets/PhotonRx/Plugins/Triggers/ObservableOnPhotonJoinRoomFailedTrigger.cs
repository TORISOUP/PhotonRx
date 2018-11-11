using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnPhotonJoinRoomFailedTrigger : ObservableTriggerBase
    {
        private Subject<FailureReason> onPhotonJoinRoomFailed;

        void OnPhotonJoinRoomFailed(object[] log)
        {
            if (onPhotonJoinRoomFailed != null)
            {
                var code = (short)log[0];
                var message = log[1] as string;
                onPhotonJoinRoomFailed.OnNext(new FailureReason(code, message));
            }
        }

        /// <summary>
        /// JoinRoomに失敗したことを通知する
        /// </summary>
        public IObservable<FailureReason> OnPhotonJoinRoomFailedAsObservable()
        {
            return onPhotonJoinRoomFailed ?? (onPhotonJoinRoomFailed = new Subject<FailureReason>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onPhotonJoinRoomFailed != null) onPhotonJoinRoomFailed.OnCompleted();
        }
    }
}
