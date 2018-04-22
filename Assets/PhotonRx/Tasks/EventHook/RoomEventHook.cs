#if (NET_4_6)
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace PhotonRx
{
    public class RoomEventHook : MonoBehaviour
    {
        private object gate = new object();

        private List<TaskCompletionSource<IResult<FailureReason, bool>>> observers
            = new List<TaskCompletionSource<IResult<FailureReason, bool>>>();

        public Task<IResult<FailureReason, bool>> Join(Action joinAction)
        {
            var tcs = new TaskCompletionSource<IResult<FailureReason, bool>>();
            lock (gate)
            {
                observers.Add(tcs);
            }
            joinAction();
            return tcs.Task;
        }


        void OnPhotonRandomJoinFailed(object[] log)
        {
            var code = (short)log[0];
            var message = log[1] as string;
            var reason = new FailureReason(code, message);
            lock (gate)
            {
                var targets = observers.ToArray();
                observers.Clear();
                foreach (var t in targets)
                {
                    t.SetResult(Failure.Create<FailureReason, bool>(reason));
                }
            }
        }

        void OnPhotonJoinRoomFailed(object[] log)
        {
            var code = (short)log[0];
            var message = log[1] as string;
            var reason = new FailureReason(code, message);
            lock (gate)
            {
                var targets = observers.ToArray();
                observers.Clear();
                foreach (var t in targets)
                {
                    t.SetResult(Failure.Create<FailureReason, bool>(reason));
                }
            }
        }

        private void OnJoinedRoom()
        {
            lock (gate)
            {
                var targets = observers.ToArray();
                observers.Clear();
                foreach (var t in targets)
                {
                    t.SetResult(Success.Create<FailureReason, bool>(true));
                }
            }
        }
    }
}
#endif