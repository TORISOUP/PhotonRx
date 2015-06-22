using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnPhotonPlayerDisconnectedTrigger : ObservableTriggerBase
    {
        private Subject<PhotonPlayer> onPhotonPlayerDisconnected;

        private void OnPhotonPlayerDisconnected(PhotonPlayer leftPlayer)
        {
            if (onPhotonPlayerDisconnected != null) onPhotonPlayerDisconnected.OnNext(leftPlayer);
        }

        /// <summary>
        /// リモートプレイヤが部屋から退出したことを通知する
        /// </summary>
        public IObservable<PhotonPlayer> OnPhotonPlayerDisconnectedAsObservable()
        {
            return onPhotonPlayerDisconnected ?? (onPhotonPlayerDisconnected = new Subject<PhotonPlayer>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onPhotonPlayerDisconnected != null)
            {
                onPhotonPlayerDisconnected.OnCompleted();
            }
        }
    }
}
