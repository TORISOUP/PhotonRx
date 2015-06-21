using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnDisconnectedFromPhotonTrigger : ObservableTriggerBase
    {
        private Subject<Unit> onDisocnnectedFromPhoton;

        private void OnDisconnectedFromPhoton()
        {
            if (onDisocnnectedFromPhoton != null) onDisocnnectedFromPhoton.OnNext(Unit.Default);
        }

        /// <summary>
        /// サーバから切断されたことを通知する
        /// </summary>
        public IObservable<Unit> OnDisconnectedFromPhotonAsObservable()
        {
            return onDisocnnectedFromPhoton ?? (onDisocnnectedFromPhoton = new Subject<Unit>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onDisocnnectedFromPhoton != null)
            {
                onDisocnnectedFromPhoton.OnCompleted();
            }
        }
    }
}
