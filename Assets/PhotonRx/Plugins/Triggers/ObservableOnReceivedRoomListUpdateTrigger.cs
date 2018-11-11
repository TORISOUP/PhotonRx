using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnReceivedRoomListUpdateTrigger : ObservableTriggerBase
    {
        private Subject<Unit> onRoomListUpdate;

        private void OnReceivedRoomListUpdate()
        {
            if (onRoomListUpdate != null) onRoomListUpdate.OnNext(Unit.Default);
        }

        /// <summary>
        /// 部屋リストが更新されたことを通知する
        /// </summary>
        public IObservable<Unit> OnReceivedRoomListUpdateAsObservable()
        {
            return onRoomListUpdate ?? (onRoomListUpdate = new Subject<Unit>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onRoomListUpdate != null)
            {
                onRoomListUpdate.OnCompleted();
            }
        }
    }
}
