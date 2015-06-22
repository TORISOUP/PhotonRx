using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnPhotonMaxCccuReachedTrigger : ObservableTriggerBase
    {
        private Subject<Unit> onPhotonMaxCccuReached;

        private void OnPhotonMaxCccuReached()
        {
            if (onPhotonMaxCccuReached != null) onPhotonMaxCccuReached.OnNext(Unit.Default);
        }

        /// <summary>
        /// CCUの上限に達したため接続が切断されたことを通知する
        /// </summary>
        public IObservable<Unit> OnPhotonMaxCccuReachedAsObservable()
        {
            return onPhotonMaxCccuReached ?? (onPhotonMaxCccuReached = new Subject<Unit>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onPhotonMaxCccuReached != null)
            {
                onPhotonMaxCccuReached.OnCompleted();
            }
        }
    }
}
