using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnJoinedRoomTrigger : ObservableTriggerBase
    {
        private Subject<Unit> onJoinedRoom;

        private void OnJoinedRoom()
        {
            if (onJoinedRoom != null) onJoinedRoom.OnNext(Unit.Default);
        }

        /// <summary>
        /// 自身が部屋に参加したことを通知する
        /// </summary>
        public IObservable<Unit> OnJoinedRoomAsObservable()
        {
            return onJoinedRoom ?? (onJoinedRoom = new Subject<Unit>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onJoinedRoom != null)
            {
                onJoinedRoom.OnCompleted();
            }
        }
    }
}
