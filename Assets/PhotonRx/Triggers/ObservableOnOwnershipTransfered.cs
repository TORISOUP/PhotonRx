using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnOwnershipTransfered : ObservableTriggerBase
    {

        private Subject<OwnershipTransferedObject> onChanged;

        public void OnOwnershipTransfered(object[] viewAndPlayers)
        {
            if (onChanged == null) return;

            var view = viewAndPlayers[0] as PhotonView;

            var newOwner = viewAndPlayers[1] as PhotonPlayer;

            var oldOwner = viewAndPlayers[2] as PhotonPlayer;

            onChanged.OnNext(new OwnershipTransferedObject(view, newOwner, oldOwner));
        }

        /// <summary>
        /// 自身が部屋から出たことを通知する
        /// </summary>
        public IObservable<OwnershipTransferedObject> OnOwnershipTransferedAsObservable()
        {
            return onChanged ?? (onChanged = new Subject<OwnershipTransferedObject>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onChanged != null)
            {
                onChanged.OnCompleted();
            }
        }
    }

    public struct OwnershipTransferedObject
    {
        public PhotonView View { get; private set; }
        public PhotonPlayer OldOwner { get; private set; }
        public PhotonPlayer NewOwner { get; private set; }

        public OwnershipTransferedObject(PhotonView view, PhotonPlayer oldOwner, PhotonPlayer newOwner) : this()
        {
            View = view;
            OldOwner = oldOwner;
            NewOwner = newOwner;
        }
    }
}
