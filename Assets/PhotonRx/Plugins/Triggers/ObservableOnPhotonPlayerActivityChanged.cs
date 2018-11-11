using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnPhotonPlayerActivityChanged : ObservableTriggerBase
    {
        private Subject<PhotonPlayer> onChanged;

        private void OnPhotonPlayerActivityChanged(PhotonPlayer otherPlayer)
        {
            if (onChanged != null) onChanged.OnNext(otherPlayer);
        }

        public IObservable<PhotonPlayer> OnPhotonPlayerActivityChangedAsObservable()
        {
            return onChanged ?? (onChanged = new Subject<PhotonPlayer>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onChanged != null)
            {
                onChanged.OnCompleted();
            }
        }
    }
}
