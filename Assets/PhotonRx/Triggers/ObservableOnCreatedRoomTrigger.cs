using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnCreatedRoomTrigger : ObservableTriggerBase
    {
        private Subject<Unit> onCreatedRoom;

        private void OnCreatedRoom()
        {
            if (onCreatedRoom != null) onCreatedRoom.OnNext(Unit.Default);
        }

        /// <summary>
        /// PhotonNetwork.CreateRoomが成功したことを通知する
        /// </summary>
        public IObservable<Unit> OnCreatedRoomAsObservabvle()
        {
            return onCreatedRoom ?? (onCreatedRoom = new Subject<Unit>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onCreatedRoom != null)
            {
                onCreatedRoom.OnCompleted();
            }
        }
    }
}
