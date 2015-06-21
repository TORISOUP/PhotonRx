using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnMasterClientSwitchedTrigger : ObservableTriggerBase
    {
        private Subject<PhotonPlayer> onMasterClientSwitched;

        private void OnMasterClientSwitched(PhotonPlayer masterClient)
        {
            if (onMasterClientSwitched != null) onMasterClientSwitched.OnNext(masterClient);
        }

        /// <summary>
        /// マスタークライアントが退室し、新しいマスタークライアントに切り替わったことを通知する
        /// </summary>
        public IObservable<PhotonPlayer> OnMasterClientSwitchedAsObservable()
        {
            return onMasterClientSwitched ?? (onMasterClientSwitched = new Subject<PhotonPlayer>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onMasterClientSwitched != null)
            {
                onMasterClientSwitched.OnCompleted();
            }
        }
    }
}
