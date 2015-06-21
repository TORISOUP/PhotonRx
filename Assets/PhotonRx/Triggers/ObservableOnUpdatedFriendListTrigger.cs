using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnUpdatedFriendListTrigger : ObservableTriggerBase
    {
        private Subject<Unit> onUpdatedFriendList;

        private void OnUpdatedFriendList()
        {
            if (onUpdatedFriendList != null) onUpdatedFriendList.OnNext(Unit.Default);
        }

        /// <summary>
        /// PhotonNetwork.Firendsが更新されたことを通知する
        /// </summary>
        public IObservable<Unit> OnUpdatedFriendListAsObservabvle()
        {
            return onUpdatedFriendList ?? (onUpdatedFriendList = new Subject<Unit>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onUpdatedFriendList != null)
            {
                onUpdatedFriendList.OnCompleted();
            }
        }
    }
}
