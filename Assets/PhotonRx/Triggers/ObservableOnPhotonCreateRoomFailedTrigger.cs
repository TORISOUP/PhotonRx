using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnPhotonCreateRoomFailedTrigger : ObservableTriggerBase
    {

        private Subject<FailureReason> onPhotonCreateRoomFailed;

        private void OnPhotonCreateRoomFailed(object[] log)
        {
            if (onPhotonCreateRoomFailed != null)
            {
                var code = (short)log[0];
                var message = log[1] as string;
                onPhotonCreateRoomFailed.OnNext(new FailureReason(code, message));
            }
        }

        /// <summary>
        /// CreateRoomの呼び出しが失敗したことを通知する
        /// </summary>
        public IObservable<FailureReason> OnPhotonCreateRoomFailedObservable()
        {
            return onPhotonCreateRoomFailed ?? (onPhotonCreateRoomFailed = new Subject<FailureReason>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onPhotonCreateRoomFailed != null)
            {
                onPhotonCreateRoomFailed.OnCompleted();
            }
        }
    }
}
