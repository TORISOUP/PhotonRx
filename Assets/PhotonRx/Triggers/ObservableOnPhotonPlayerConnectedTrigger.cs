using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnPhotonPlayerConnectedTrigger : ObservableTriggerBase
    {
        private Subject<PhotonPlayer> onPhotonPlayerConnected;

        private void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
        {
            if (onPhotonPlayerConnected != null) onPhotonPlayerConnected.OnNext(newPlayer);
        }

        /// <summary>
        /// リモートプレイヤが部屋に参加したことを通知する
        /// </summary>
        public IObservable<PhotonPlayer> OnPhotonPlayerConnectedAsObservable()
        {
            return onPhotonPlayerConnected ?? (onPhotonPlayerConnected = new Subject<PhotonPlayer>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onPhotonPlayerConnected != null)
            {
                onPhotonPlayerConnected.OnCompleted();
            }
        }
    }
}
