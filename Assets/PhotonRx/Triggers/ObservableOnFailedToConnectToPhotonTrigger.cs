using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnFailedToConnectToPhotonTrigger : ObservableTriggerBase
    {
        private Subject<DisconnectCause> onFailedToConnectToPhoton;

        private void OnFailedToConnectToPhoton(DisconnectCause cause)
        {
            if (onFailedToConnectToPhoton != null) onFailedToConnectToPhoton.OnNext(cause);
        }

        /// <summary>
        /// Photonサーバーへの接続が確立するより前に失敗したときに、原因とともに通知する
        /// </summary>
        public IObservable<DisconnectCause> OnFailedToConnectToPhotonAsObservable()
        {
            return onFailedToConnectToPhoton ?? (onFailedToConnectToPhoton = new Subject<DisconnectCause>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onFailedToConnectToPhoton != null)
            {
                onFailedToConnectToPhoton.OnCompleted();
            }
        }
    }
}
