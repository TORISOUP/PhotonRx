using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnConnectedToMasterTrigger : ObservableTriggerBase
    {
        private Subject<Unit> onConnectedToMaster;

        private void OnConnectedToMaster()
        {
            if (onConnectedToMaster != null) onConnectedToMaster.OnNext(Unit.Default);
        }

        /// <summary>
        /// PhotonNetwork.autoJoinLobbyがfalseのときにMasterServerのロビーに参加できたことを通知する
        /// </summary>
        public IObservable<Unit> OnConnectedToMasterAsObservabvle()
        {
            return onConnectedToMaster ?? (onConnectedToMaster = new Subject<Unit>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onConnectedToMaster != null)
            {
                onConnectedToMaster.OnCompleted();
            }
        }
    }
}
