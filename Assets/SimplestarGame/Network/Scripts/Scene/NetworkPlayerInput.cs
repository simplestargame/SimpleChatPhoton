using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimplestarGame
{
    public struct PlayerInput : INetworkInput
    {
        public Vector3 move;
    }
    public class NetworkPlayerInput : MonoBehaviour, INetworkRunnerCallbacks
    {
        [SerializeField] SceneContext sceneContext;
        [SerializeField] Transform mainCamera;

        void INetworkRunnerCallbacks.OnInput(NetworkRunner runner, NetworkInput input)
        {
            input.Set(new PlayerInput
            {
                move = this.MoveInput(),
            });
        }

        Vector3 MoveInput()
        {
            Vector2 move = Vector2.zero;
            if (this.sceneContext.buttonLeft.IsPressed)
            {
                move += new Vector2(-1f, 0);
            }
            if (this.sceneContext.buttonRight.IsPressed)
            {
                move += new Vector2(+1f, 0);
            }
            if (this.sceneContext.buttonUp.IsPressed)
            {
                move += new Vector2(0, +1f);
            }
            if (this.sceneContext.buttonDown.IsPressed)
            {
                move += new Vector2(0, -1f);
            }
            Vector3 moveDirection = (this.mainCamera.forward * move.y + this.mainCamera.right * move.x).normalized;
            Vector3 localMoveDirection = this.transform.InverseTransformDirection(moveDirection);
            localMoveDirection.y = 0f;
            return localMoveDirection;
        }

        #region INetworkRunnerCallbacks
        void INetworkRunnerCallbacks.OnConnectedToServer(NetworkRunner runner)
        {
        }

        void INetworkRunnerCallbacks.OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
        }

        void INetworkRunnerCallbacks.OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        {
        }

        void INetworkRunnerCallbacks.OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
        }

        void INetworkRunnerCallbacks.OnDisconnectedFromServer(NetworkRunner runner)
        {
        }

        void INetworkRunnerCallbacks.OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
        }

        void INetworkRunnerCallbacks.OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
        }

        void INetworkRunnerCallbacks.OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
        }

        void INetworkRunnerCallbacks.OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
        }

        void INetworkRunnerCallbacks.OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
        {
        }

        void INetworkRunnerCallbacks.OnSceneLoadDone(NetworkRunner runner)
        {
        }

        void INetworkRunnerCallbacks.OnSceneLoadStart(NetworkRunner runner)
        {
        }

        void INetworkRunnerCallbacks.OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
        }

        void INetworkRunnerCallbacks.OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            if (shutdownReason != ShutdownReason.HostMigration)
            {
            }
        }

        void INetworkRunnerCallbacks.OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
        }
        #endregion
    }
}