#if (NET_4_6)
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace PhotonRx
{
    public class ConnectionEventHook : MonoBehaviour
    {
        private object gate = new object();

        private List<TaskCompletionSource<IResult<DisconnectCause, bool>>> observers
            = new List<TaskCompletionSource<IResult<DisconnectCause, bool>>>();

        public Task<IResult<DisconnectCause, bool>> Connect(Action connectAction)
        {
            var tcs = new TaskCompletionSource<IResult<DisconnectCause, bool>>();
            lock (gate)
            {
                observers.Add(tcs);
            }
            connectAction();
            return tcs.Task;
        }

        private void OnConnectedToPhoton()
        {
            if (PhotonNetwork.autoJoinLobby) return;
            lock (gate)
            {
                var targets = observers.ToArray();
                observers.Clear();
                foreach (var t in targets)
                {
                    t.SetResult(Success.Create<DisconnectCause, bool>(true));
                }
            }
        }

        private void OnJoinedLobby()
        {
            if (!PhotonNetwork.autoJoinLobby) return;
            lock (gate)
            {
                var targets = observers.ToArray();
                observers.Clear();
                foreach (var t in targets)
                {
                    t.SetResult(Success.Create<DisconnectCause, bool>(true));
                }
            }
        }

        private void OnFailedToConnectToPhoton(DisconnectCause cause)
        {
            lock (gate)
            {
                var targets = observers.ToArray();
                observers.Clear();
                foreach (var t in targets)
                {
                    t.SetResult(Failure.Create<DisconnectCause, bool>(cause));
                }
            }
        }
    }
}
#endif