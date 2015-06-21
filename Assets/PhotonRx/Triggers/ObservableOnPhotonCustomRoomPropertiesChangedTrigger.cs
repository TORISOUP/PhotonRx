using UnityEngine;
using System;
using ExitGames.Client.Photon;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnPhotonCustomRoomPropertiesChangedTrigger : ObservableTriggerBase
    {
        private Subject<Hashtable> onPhotonCustomRoomPropertiesChanged;

        private void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
        {
            if (onPhotonCustomRoomPropertiesChanged != null) onPhotonCustomRoomPropertiesChanged.OnNext(propertiesThatChanged);
        }

        /// <summary>
        /// 部屋のカスタムプロパティが変更されたことを通知する
        /// </summary>
        public IObservable<Hashtable> OnPhotonPlayerConnectedAsObservabvle()
        {
            return onPhotonCustomRoomPropertiesChanged ?? (onPhotonCustomRoomPropertiesChanged = new Subject<Hashtable>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onPhotonCustomRoomPropertiesChanged != null)
            {
                onPhotonCustomRoomPropertiesChanged.OnCompleted();
            }
        }
    }
}
