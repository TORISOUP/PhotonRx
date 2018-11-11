#if ( NET_4_6 || NET_STANDARD_2_0)
using System;
using System.Threading.Tasks;
using ExitGames.Client.Photon;
using UnityEngine;

namespace PhotonRx
{
    public static class PhotonTask
    {

        #region Connect
        public static Task<IResult<DisconnectCause, bool>> ConnectToBestCloudServer(string gameVersion)
        {
            return Connect(() => PhotonNetwork.ConnectToBestCloudServer(gameVersion));
        }

        public static Task<IResult<DisconnectCause, bool>> ConnectToMaster(string masterServerAddress, int port, string appID, string gameVersion)
        {
            return Connect(() => PhotonNetwork.ConnectToMaster(masterServerAddress, port, appID, gameVersion));
        }

        public static Task<IResult<DisconnectCause, bool>> ConnectToRegion(CloudRegionCode region, string gameVersion)
        {
            return Connect(() => PhotonNetwork.ConnectToRegion(region, gameVersion));
        }

        public static Task<IResult<DisconnectCause, bool>> ConnectUsingSettings(string gameVersion)
        {
            return Connect(() => PhotonNetwork.ConnectUsingSettings(gameVersion));
        }

        private static Task<IResult<DisconnectCause, bool>> Connect(Action connectAction)
        {
            var eventHook = GetOrAddComponent<ConnectionEventHook>(PhotonEventManager.Instance.gameObject);
            return eventHook.Connect(connectAction);
        }

        #endregion

        #region JoinRoom

        public static Task<IResult<FailureReason, bool>> CreateRoom(string roomName)
        {
            return JoinRoom(() => PhotonNetwork.CreateRoom(roomName));
        }

        public static Task<IResult<FailureReason, bool>> CreateRoom(string roomName, RoomOptions roomOptions, TypedLobby typedLobby)
        {
            return JoinRoom(() => PhotonNetwork.CreateRoom(roomName, roomOptions, typedLobby));
        }

        public static Task<IResult<FailureReason, bool>> CreateRoom(string roomName, RoomOptions roomOptions, TypedLobby typedLobby, string[] expectedUsers)
        {
            return JoinRoom(() => PhotonNetwork.CreateRoom(roomName, roomOptions, typedLobby, expectedUsers));
        }

        public static Task<IResult<FailureReason, bool>> JoinRoom(string roomName)
        {
            return JoinRoom(() => PhotonNetwork.JoinRoom(roomName));
        }

        public static Task<IResult<FailureReason, bool>> JoinOrCreateRoom(string roomName, RoomOptions roomOptions, TypedLobby typedLobby, string[] expectedUsers = null)
        {
            return JoinRoom(() => PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby, expectedUsers));
        }

        public static Task<IResult<FailureReason, bool>> JoinRandomRoom()
        {
            return JoinRoom(() => PhotonNetwork.JoinRandomRoom());
        }

        public static Task<IResult<FailureReason, bool>> JoinRandomRoom(Hashtable expectedCustomRoomProperties, byte expectedMaxPlayers)
        {
            return JoinRoom(() => PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, expectedMaxPlayers));
        }

        public static Task<IResult<FailureReason, bool>> JoinRandomRoom(Hashtable expectedCustomRoomProperties, byte expectedMaxPlayers, MatchmakingMode matchingType, TypedLobby typedLobby, string sqlLobbyFilter, string[] expectedUsers = null)
        {
            return JoinRoom(() => PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, expectedMaxPlayers, matchingType, typedLobby, sqlLobbyFilter, expectedUsers));
        }

        public static Task<IResult<FailureReason, bool>> ReJoinRoom(string roomName)
        {
            return JoinRoom(() => PhotonNetwork.ReJoinRoom(roomName));
        }

        private static Task<IResult<FailureReason, bool>> JoinRoom(Action joinAction)
        {
            var eventHook = GetOrAddComponent<RoomEventHook>(PhotonEventManager.Instance.gameObject);
            return eventHook.Join(joinAction);
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

#endif