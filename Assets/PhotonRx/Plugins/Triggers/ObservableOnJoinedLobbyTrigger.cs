using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnJoinedLobbyTrigger : ObservableTriggerBase
    {
        private Subject<Unit> onJoinedLobby;

        private void OnJoinedLobby()
        {
            if (onJoinedLobby != null) onJoinedLobby.OnNext(Unit.Default);
        }

        /// <summary>
        /// PhotonNetwork.autoJoinLobbyがtrueのときにMasterServerのロビーに参加できたことを通知する
        /// </summary>
        public IObservable<Unit> OnJoinedLobbyAsObservable()
        {
            return onJoinedLobby ?? (onJoinedLobby = new Subject<Unit>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onJoinedLobby != null)
            {
                onJoinedLobby.OnCompleted();
            }
        }
    }
}
