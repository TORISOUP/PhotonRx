using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnLeftRoomTrigger : ObservableTriggerBase
    {
        private Subject<Unit> onLeftRoom;

        private void OnLeftRoom()
        {
            if (onLeftRoom != null) onLeftRoom.OnNext(Unit.Default);
        }

        /// <summary>
        /// 自身が部屋から出たことを通知する
        /// </summary>
        public IObservable<Unit> OnLeftRoomAsObservabvle()
        {
            return onLeftRoom ?? (onLeftRoom = new Subject<Unit>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onLeftRoom != null)
            {
                onLeftRoom.OnCompleted();
            }
        }
    }
}
