using System.Threading;
using PhotonRx;
using UnityEngine;
#if ( NET_4_6 || NET_STANDARD_2_0)
using System.Threading.Tasks;
#endif

public class LoginTaskSample : MonoBehaviour
{
    void Start()
    {
        LoginAndJoinRoom();
    }

#if ( NET_4_6 || NET_STANDARD_2_0)

    CancellationTokenSource _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource();

    private void OnDestroy()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
    }

    async void LoginAndJoinRoom()
    {
        var token = _cancellationTokenSource.Token;
        PhotonNetwork.autoJoinLobby = true;

        var connect = await PhotonTask.ConnectUsingSettings("v1", token);

        // 失敗
        if (connect.IsFailure)
        {
            Debug.LogError(connect.ToFailure.Value);
            return;
        }

        var randomJoined = await PhotonTask.JoinRandomRoom(token);

        //成功なら終わり
        if (randomJoined.IsSuccess) return;

        Debug.Log("Create Room");

        var created = await PhotonTask.JoinOrCreateRoom("test", null, null, null, token);

        if (created.IsSuccess)
        {
            Debug.Log("部屋に参加した");
        }
        else
        {
            Debug.LogError(created.ToFailure.Value);
        }
    }
#else
    void LoginAndJoinRoom()
    {
        Debug.LogWarning("Can only use .NET 4.6 mode.");
    }
#endif
}