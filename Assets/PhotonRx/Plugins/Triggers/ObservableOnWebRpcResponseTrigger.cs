using UnityEngine;
using System;
using ExitGames.Client.Photon;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnWebRpcResponseTrigger : ObservableTriggerBase
    {
        private Subject<OperationResponse> onWebRpcResponse;

        private void OnWebRpcResponse(OperationResponse response)
        {
            if (onWebRpcResponse != null) onWebRpcResponse.OnNext(response);
        }

        /// <summary>
        /// WebRPCを受信したことを通知する
        /// </summary>
        public IObservable<OperationResponse> OnWebRpcResponseAsObservable()
        {
            return onWebRpcResponse ?? (onWebRpcResponse = new Subject<OperationResponse>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onWebRpcResponse != null)
            {
                onWebRpcResponse.OnCompleted();
            }
        }
    }
}
