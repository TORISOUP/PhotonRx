using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnOwnershipRequestTrigger : ObservableTriggerBase
    {
        private Subject<Tuple<PhotonView,PhotonPlayer>> onOwnershipRequest;

        private void OnOwnershipRequest(object[] data)
        {
            if (onOwnershipRequest != null)
                onOwnershipRequest.OnNext(
                    new Tuple<PhotonView, PhotonPlayer>(
                        data[0] as PhotonView,
                        data[1] as PhotonPlayer
                        ));
        }

        /// <summary>
        /// PhotonViewの所有権の譲渡リクエストがきたことを通知する
        /// </summary>
        public IObservable<Tuple<PhotonView, PhotonPlayer>> OnOwnershipRequestAsObservable()
        {
            return onOwnershipRequest ?? (onOwnershipRequest = new Subject<Tuple<PhotonView, PhotonPlayer>>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onOwnershipRequest != null)
            {
                onOwnershipRequest.OnCompleted();
            }
        }
    }
}
