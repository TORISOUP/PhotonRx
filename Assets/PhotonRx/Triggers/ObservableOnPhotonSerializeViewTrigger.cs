using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnPhotonSerializeViewTrigger : ObservableTriggerBase
    {
        private bool isInitalized;

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
        public IObservable<Tuple<PhotonStream, PhotonMessageInfo>> OnPhotonSerializeViewAsObservable()
        {
            if (!isInitalized)
            {
                var view = PhotonView.Get(this);
                if (view == null) throw new Exception("Not found PhotonView.");
                if (!view.ObservedComponents.Contains(this)) view.ObservedComponents.Add(this);
                isInitalized = true;
            }

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
