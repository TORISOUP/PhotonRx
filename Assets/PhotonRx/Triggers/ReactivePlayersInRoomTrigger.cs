using UnityEngine;
using System;
using System.Linq;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ReactivePhotonPlayersTriggers : ObservableTriggerBase
    {
        private ReactiveCollection<PhotonPlayer> _playersReactiveCollection; 

        /// <summary>
        /// PhotonNetwork.Firendsが更新されたことを通知する
        /// </summary>
        public ReactiveCollection<PhotonPlayer> PhotonPlayersReactiveCollection()
        {
            return _playersReactiveCollection ??
                   (_playersReactiveCollection = new ReactiveCollection<PhotonPlayer>(PhotonNetwork.playerList.ToList()));
        }

        private void OnJoinedRoom()
        {
            if (_playersReactiveCollection == null) return;

            if (_playersReactiveCollection.Count > 0)
            {
                _playersReactiveCollection.Clear();
            }
            foreach (var photonPlayer in PhotonNetwork.playerList)
            {
                _playersReactiveCollection.Add(photonPlayer);
            }
        }

        private void OnLeftRoom()
        {
            if (_playersReactiveCollection != null)
            {
                _playersReactiveCollection.Clear();
            }
        }

        private void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
        {
            if (_playersReactiveCollection != null)
            {
                _playersReactiveCollection.Add(newPlayer);
            }
        }

        private void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
        {
            if (_playersReactiveCollection != null)
            {
                _playersReactiveCollection.Remove(otherPlayer);
            }
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (_playersReactiveCollection != null)
            {
                _playersReactiveCollection.Dispose();
            }
        }
    }
}
