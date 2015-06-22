using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnConnectionFailTrigger : ObservableTriggerBase
    {
        private Subject<DisconnectCause> onConnectionFail;

        private void OnConnectionFail(DisconnectCause cause)
        {
            if (onConnectionFail != null) onConnectionFail.OnNext(cause);
        }

        /// <summary>
        /// 接続が失敗したことを原因とともに通知する
        /// </summary>
        public IObservable<DisconnectCause> OnConnectionFailAsObservable()
        {
            return onConnectionFail ?? (onConnectionFail = new Subject<DisconnectCause>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onConnectionFail != null)
            {
                onConnectionFail.OnCompleted();
            }
        }
    }
}
