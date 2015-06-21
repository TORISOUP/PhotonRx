using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnConnectedToPhotonTrigger : ObservableTriggerBase
    {
        private Subject<Unit> onConnectedToPhoton;

        private void OnConnectedToPhoton()
        {
            if (onConnectedToPhoton != null) onConnectedToPhoton.OnNext(Unit.Default);
        }

        /// <summary>
        /// サーバへ初期接続が成功したことを通知する
        /// </summary>
        public IObservable<Unit> OnConnectedToPhotonAsObservable()
        {
            return onConnectedToPhoton ?? (onConnectedToPhoton = new Subject<Unit>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onConnectedToPhoton != null)
            {
                onConnectedToPhoton.OnCompleted();
            }
        }
    }
}
