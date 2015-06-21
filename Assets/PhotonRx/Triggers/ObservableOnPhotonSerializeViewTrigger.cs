using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnPhotonSerializeViewTrigger : ObservableTriggerBase
    {
        private Subject<Tuple<PhotonStream, PhotonMessageInfo>> onPhotonSerializeView;

        private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (onPhotonSerializeView != null)
            {
                onPhotonSerializeView.OnNext(new Tuple<PhotonStream, PhotonMessageInfo>(stream, info));
            }
        }

        /// <summary>
        /// PhotonViewがデータの同期を行うタイミングを通知する
        /// </summary>
        public IObservable<Tuple<PhotonStream, PhotonMessageInfo>> OnPhotonSerializeViewAsObservabvle()
        {
            return onPhotonSerializeView ??
                   (onPhotonSerializeView = new Subject<Tuple<PhotonStream, PhotonMessageInfo>>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onPhotonSerializeView != null)
            {
                onPhotonSerializeView.OnCompleted();
            }
        }
    }
}
