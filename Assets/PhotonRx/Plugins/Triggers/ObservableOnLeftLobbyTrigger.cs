using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnLeftLobbyTrigger : ObservableTriggerBase
    {
        private Subject<Unit> onLeftLobby;

        private void OnLeftLobby()
        {
            if (onLeftLobby != null) onLeftLobby.OnNext(Unit.Default);
        }

        /// <summary>
        /// ロビーから退出したことを通知する
        /// </summary>
        public IObservable<Unit> OnLeftLobbyAsObservable()
        {
            return onLeftLobby ?? (onLeftLobby = new Subject<Unit>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onLeftLobby != null)
            {
                onLeftLobby.OnCompleted();
            }
        }
    }
}
