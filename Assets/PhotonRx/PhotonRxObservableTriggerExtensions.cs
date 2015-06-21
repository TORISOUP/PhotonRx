using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using PhotonRx.Triggers;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace PhotonRx
{
    public static class PhotonRxObservableTriggerExtensions
    {
        #region ObservableOnConnectedToMasterTrigger

        /// <summary>
        /// MasterServerのロビーに参加できたことを通知する
        /// （PhotonNetwork.autoJoinLobbyがfalseの時のみ）
        /// </summary>
        public static IObservable<Unit> OnConnectedToMasterAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<Unit>();
            return GetOrAddComponent<ObservableOnConnectedToMasterTrigger>(component.gameObject).OnConnectedToMasterAsObservabvle();
        }
        #endregion

        #region ObservableOnConnectedToPhotonTrigger
        /// <summary>
        /// サーバへ初期接続が成功したことを通知する
        /// </summary>
        public static IObservable<Unit> OnConnectedToPhotonAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<Unit>();
            return GetOrAddComponent<ObservableOnConnectedToPhotonTrigger>(component.gameObject).OnConnectedToPhotonAsObservable();
        }
        #endregion

        #region ObservableOnConnectionFailTrigger

        /// <summary>
        /// 接続が失敗したことを通知する
        /// </summary>
        public static IObservable<DisconnectCause> OnConnectionFailAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<DisconnectCause> ();
            return GetOrAddComponent<ObservableOnConnectionFailTrigger>(component.gameObject).OnConnectionFailAsObservabvle();
        }
        #endregion

        #region ObservableOnCreatedRoomTrigger

        /// <summary>
        /// PhotonNetwork.CreateRoomが成功したことを通知する
        /// </summary>
        public static IObservable<Unit> OnCreatedRoomAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<Unit>();
            return GetOrAddComponent<ObservableOnCreatedRoomTrigger>(component.gameObject).OnCreatedRoomAsObservabvle();
        }

        #endregion

        #region ObservableOnCustomAuthenticationFailedTrigger

        /// <summary>
        /// カスタム認証に失敗したことを通知する
        /// </summary>
        public static IObservable<string> OnCustomAuthenticationFailedAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<string>();
            return GetOrAddComponent<ObservableOnCustomAuthenticationFailedTrigger>(component.gameObject).OnCustomAuthenticationFailedAsObservabvle();
        }

        #endregion

        #region ObservableOnDisconnectedFromPhotonTrigger
        /// <summary>
        /// サーバから切断されたことを通知する
        /// </summary>
        public static IObservable<Unit> OnDisconnectedFromPhotonAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<Unit>();
            return GetOrAddComponent<ObservableOnDisconnectedFromPhotonTrigger>(component.gameObject).OnDisconnectedFromPhotonAsObservable();
        }
        #endregion

        #region ObservableOnFailedToConnectToPhotonTrigger
        /// <summary>
        ///  Photonサーバーへの接続が確立するより前に失敗したことを通知する
        /// </summary>
        public static IObservable<DisconnectCause> OnFailedToConnectToPhotonAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<DisconnectCause>();
            return GetOrAddComponent<ObservableOnFailedToConnectToPhotonTrigger>(component.gameObject).OnFailedToConnectToPhotonAsObservabvle();
        }
        #endregion

        #region ObservableOnJoinedLobbyTrigger
        /// <summary>
        /// MasterServerのロビーに参加できたことを通知する
        /// PhotonNetwork.autoJoinLobbyがtrueのときのみ
        /// </summary>
        public static IObservable<Unit> OnJoinedLobbyAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<Unit>();
            return GetOrAddComponent<ObservableOnJoinedLobbyTrigger>(component.gameObject).OnJoinedLobbyAsObservabvle();
        }
        #endregion

        #region ObservableOnJoinedRoomTrigger
        /// <summary>
        /// 自身が部屋に参加したことを通知する
        /// </summary>
        public static IObservable<Unit> OnJoinedRoomAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<Unit>();
            return GetOrAddComponent<ObservableOnJoinedRoomTrigger>(component.gameObject).OnJoinedRoomAsObservabvle();
        }
        #endregion

        #region ObservableOnLeftLobbyTrigger
        /// <summary>
        /// ロビーから退出したことを通知する
        /// </summary>
        public static IObservable<Unit> OnLeftLobbyAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<Unit>();
            return GetOrAddComponent<ObservableOnLeftLobbyTrigger>(component.gameObject).OnLeftLobbyAsObservabvle();
        }
        #endregion

        #region ObservableOnLeftRoomTrigger
        /// <summary>
        /// 自信が部屋から退出したことを通知する
        /// </summary>
        public static IObservable<Unit> OnLeftRoomAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<Unit>();
            return GetOrAddComponent<ObservableOnLeftRoomTrigger>(component.gameObject).OnLeftRoomAsObservabvle();
        }
        #endregion

        #region ObservableOnMasterClientSwitchedTrigger
        /// <summary>
        /// マスタークライアントが退室し、新しいマスタークライアントに切り替わったことを通知する
        /// </summary>
        public static IObservable<PhotonPlayer> OnMasterClientSwitchedAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<PhotonPlayer>();
            return GetOrAddComponent<ObservableOnMasterClientSwitchedTrigger>(component.gameObject).OnMasterClientSwitchedAsObservable();
        }
        #endregion

        #region ObservableOnOwnershipRequestTrigger
        /// <summary>
        /// PhotonViewの所有権の譲渡リクエストがきたことを通知する
        /// </summary>
        public static IObservable<Tuple<PhotonView, PhotonPlayer>> OnOwnershipRequestAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<Tuple<PhotonView, PhotonPlayer>>();
            return GetOrAddComponent<ObservableOnOwnershipRequestTrigger>(component.gameObject).OnOwnershipRequestAsObservabvle();
        }
        #endregion

        #region ObservableOnPhotonCreateRoomFailedTrigger

        /// <summary>
        /// CreateRoomが失敗したことを通知する
        /// </summary>
        public static IObservable<Unit> OnPhotonCreateRoomFailedAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<Unit>();
            return GetOrAddComponent<ObservableOnPhotonCreateRoomFailedTrigger>(component.gameObject).OnPhotonCreateRoomFailedAsObservable();
        }

        /// <summary>
        /// CreateRoomが失敗したことを通知する
        /// </summary>
        public static IObservable<object[]> OnPhotonCreateRoomFailedWithLogAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<object[]>();
            return GetOrAddComponent<ObservableOnPhotonCreateRoomFailedTrigger>(component.gameObject).OnPhotonCreateRoomFailedWithLogAsObservable();
        }

        #endregion

        #region ObservableOnPhotonCustomRoomPropertiesChangedTrigger
        /// <summary>
        /// 部屋のカスタムプロパティが変更されたことを通知する
        /// </summary>
        public static IObservable<Hashtable> OnPhotonCustomRoomPropertiesChangedAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<Hashtable>();
            return GetOrAddComponent<ObservableOnPhotonCustomRoomPropertiesChangedTrigger>(component.gameObject).OnPhotonPlayerConnectedAsObservabvle();
        }
        #endregion

        #region ObservableOnPhotonInstantiateTrigger
        /// <summary>
        /// PhotonNetwork.InstantiateによってGameObjectが生成されたことを通知する
        /// </summary>
        public static IObservable<PhotonMessageInfo> OnPhotonInstantiateAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<PhotonMessageInfo>();
            return GetOrAddComponent<ObservableOnPhotonInstantiateTrigger>(component.gameObject).OnPhotonInstantiateAsObservabvle();
        }
        #endregion

        #region ObservableOnPhotonJoinRoomFailedTrigger

        /// <summary>
        /// JoinRoomに失敗したことを通知する
        /// </summary>
        public static IObservable<Unit> OnPhotonJoinRoomFailedAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<Unit>();
            return GetOrAddComponent<ObservableOnPhotonJoinRoomFailedTrigger>(component.gameObject).OnPhotonJoinRoomFailedAsObservable();
        }

        /// <summary>
        /// JoinRoomに失敗したことを通知する
        /// </summary>
        public static IObservable<object[]> OnPhotonJoinRoomFailedWithLogAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<object[]>();
            return GetOrAddComponent<ObservableOnPhotonJoinRoomFailedTrigger>(component.gameObject).OnPhotonJoinRoomFailedWithLogAsObservable();
        }

        #endregion

        #region ObservableOnPhotonMaxCccuReachedTrigger
        /// <summary>
        /// CCUの上限に達したため接続が切断されたことを通知する
        /// </summary>
        public static IObservable<Unit> OnPhotonMaxCccuReachedAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<Unit>();
            return GetOrAddComponent<ObservableOnPhotonMaxCccuReachedTrigger>(component.gameObject).OnPhotonMaxCccuReachedAsObservabvle();
        }
        #endregion

        #region ObservableOnPhotonPlayerConnectedTrigger
        /// <summary>
        /// リモートプレイヤが部屋に参加したことを通知する
        /// </summary>
        public static IObservable<PhotonPlayer> OnPhotonPlayerConnectedAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<PhotonPlayer>();
            return GetOrAddComponent<ObservableOnPhotonPlayerConnectedTrigger>(component.gameObject).OnPhotonPlayerConnectedAsObservabvle();
        }
        #endregion

        #region ObservableOnPhotonPlayerDisconnectedTrigger
        /// <summary>
        /// リモートプレイヤが部屋から退出したことを通知する
        /// </summary>
        public static IObservable<PhotonPlayer> OnPhotonPlayerDisconnectedAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<PhotonPlayer>();
            return GetOrAddComponent<ObservableOnPhotonPlayerDisconnectedTrigger>(component.gameObject).OnPhotonPlayerDisconnectedAsObservabvle();
        }
        #endregion

        #region ObservableOnPhotonPlayerPropertiesChangedTrigger
        /// <summary>
        /// リモートプレイヤが部屋から退出したことを通知する
        /// </summary>
        public static IObservable<Tuple<PhotonPlayer, Hashtable>> OnPhotonPlayerPropertiesChangedAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<Tuple<PhotonPlayer, Hashtable>>();
            return GetOrAddComponent<ObservableOnPhotonPlayerPropertiesChangedTrigger>(component.gameObject).OnPhotonPlayerPropertiesChangedAsObservabvle();
        }
        #endregion

        #region ObservableOnPhotonRandomJoinFailedTrigger

        /// <summary>
        /// JoinRandomに失敗したことを通知する
        /// </summary>
        public static IObservable<Unit> OnPhotonRandomJoinFailedAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<Unit>();
            return GetOrAddComponent<ObservableOnPhotonRandomJoinFailedTrigger>(component.gameObject).OnPhotonRandomJoinFailedFailedAsObservable();
        }

        /// <summary>
        /// JoinRandomに失敗したことを通知する
        /// </summary>
        public static IObservable<object[]> OnPhotonRandomJoinFailedWithLogAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<object[]>();
            return GetOrAddComponent<ObservableOnPhotonRandomJoinFailedTrigger>(component.gameObject).OnPhotonRandomJoinFailedWithLogAsObservable();
        }

        #endregion

        #region ObservableOnPhotonSerializeViewTrigger

        /// <summary>
        /// PhotonViewがデータの同期を行うタイミングを通知する
        /// </summary>
        public static IObservable<Tuple<PhotonStream, PhotonMessageInfo>> OnPhotonSerializeViewAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<Tuple<PhotonStream, PhotonMessageInfo>>();
            return
                GetOrAddComponent<ObservableOnPhotonSerializeViewTrigger>(component.gameObject)
                    .OnPhotonSerializeViewAsObservabvle();
        }

        #endregion

        #region ObservableOnReceivedRoomListUpdateTrigger
        /// <summary>
        /// 部屋リストが更新されたことを通知する
        /// </summary>
        public static IObservable<Unit> OnReceivedRoomListUpdateAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<Unit>();
            return GetOrAddComponent<ObservableOnReceivedRoomListUpdateTrigger>(component.gameObject).OnReceivedRoomListUpdateAsObservabvle();
        }
        #endregion

        #region ObservableOnUpdatedFriendListTrigger
        /// <summary>
        /// PhotonNetwork.Firendsが更新されたことを通知する
        /// </summary>
        public static IObservable<Unit> OnUpdatedFriendListAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<Unit>();
            return GetOrAddComponent<ObservableOnUpdatedFriendListTrigger>(component.gameObject).OnUpdatedFriendListAsObservabvle();
        }
        #endregion

        #region ObservableOnWebRpcResponseTrigger
        /// <summary>
        /// WebRPCを受信したことを通知する
        /// </summary>
        public static IObservable<OperationResponse> OnWebRpcResponseAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<OperationResponse>();
            return GetOrAddComponent<ObservableOnWebRpcResponseTrigger>(component.gameObject).OnWebRpcResponseAsObservabvle();
        }
        #endregion


        private static T GetOrAddComponent<T>(GameObject gameObject)
            where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }
    }
}
