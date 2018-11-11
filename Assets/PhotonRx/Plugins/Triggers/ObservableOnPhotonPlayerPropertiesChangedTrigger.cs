using UnityEngine;
using System;
using ExitGames.Client.Photon;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnPhotonPlayerPropertiesChangedTrigger : ObservableTriggerBase
    {
        private Subject<Tuple<PhotonPlayer,Hashtable>> onPhotonPlayerPropertiesChanged;

        private void OnPhotonPlayerPropertiesChanged(object[] data)
        {
            if (onPhotonPlayerPropertiesChanged != null)
            {
                onPhotonPlayerPropertiesChanged.OnNext(new Tuple<PhotonPlayer, Hashtable>(
                    data[0] as PhotonPlayer,
                    data[1] as Hashtable
                    ));
            }
        }

        /// <summary>
        /// プレイヤのカスタムプロパティが変更されたことを通知する
        /// </summary>
        public IObservable<Tuple<PhotonPlayer, Hashtable>> OnPhotonPlayerPropertiesChangedAsObservable()
        {
            return onPhotonPlayerPropertiesChanged ?? (onPhotonPlayerPropertiesChanged = new Subject<Tuple<PhotonPlayer, Hashtable>>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onPhotonPlayerPropertiesChanged != null)
            {
                onPhotonPlayerPropertiesChanged.OnCompleted();
            }
        }
    }
}
