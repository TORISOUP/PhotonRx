using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnPhotonInstantiateTrigger : ObservableTriggerBase
    {
        private Subject<PhotonMessageInfo> onPhotonInstantiate;

        private void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            if (onPhotonInstantiate != null) onPhotonInstantiate.OnNext(info);
        }

        /// <summary>
        /// PhotonNetwork.InstantiateによってGameObjectが生成されたことを通知する
        /// </summary>
        public IObservable<PhotonMessageInfo> OnPhotonInstantiateAsObservabvle()
        {
            return onPhotonInstantiate ?? (onPhotonInstantiate = new Subject<PhotonMessageInfo>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onPhotonInstantiate != null)
            {
                onPhotonInstantiate.OnCompleted();
            }
        }
    }
}
