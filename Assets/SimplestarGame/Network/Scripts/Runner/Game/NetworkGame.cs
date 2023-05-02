using Fusion;
using System;
using System.Linq;
using UnityEngine;

namespace SimplestarGame
{
    public class NetworkGame : NetworkBehaviour
    {
        [SerializeField] Vector3[] spawnPoints;
        [SerializeField] Color[] playerColors;

        [SerializeField] PlayerAgent agentPrefab;
        [Networked] int TotalPlayerCount { get; set; }

        public void Join(NetworkPlayer player)
        {
            if (!HasStateAuthority)
            {
                return;
            }
            var playerRef = player.Object.InputAuthority;

            int token = new Guid(Runner.GetPlayerConnectionToken(playerRef)).GetHashCode();
            var agentList = FindObjectsOfType<PlayerAgent>();
            var sceneAgent = agentList.FirstOrDefault(agent => agent.Token == token);
            if (null != sceneAgent)
            {
                sceneAgent.Object.AssignInputAuthority(playerRef);
                player.ActiveAgent = sceneAgent;
            }
            else
            {
                this.SpawnPlayerAgent(player, token);
            }
        }

        public void Leave(NetworkPlayer player)
        {
            if (!HasStateAuthority)
            {
                return;
            }
            this.DespawnPlayerAgent(player);
        }

        public override void Spawned()
        {
            this.name = "[Network]Game";
            SceneContext.Instance.Game = this;
            if (null != SceneContext.Instance.PlayerInput)
            {
                Runner.AddCallbacks(SceneContext.Instance.PlayerInput);
            }
            if (null != SceneContext.Instance.hostClientText)
            {
                SceneContext.Instance.hostClientText.text = HasStateAuthority ? "Host" : "Client";
            }
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            SceneContext.Instance.Game = null;
        }

        void SpawnPlayerAgent(NetworkPlayer player, int token)
        {
            this.DespawnPlayerAgent(player);
            int pointIndex = this.TotalPlayerCount % this.spawnPoints.Length;
            int colorIndex = this.TotalPlayerCount % this.playerColors.Length;
            player.ActiveAgent = Runner.Spawn(this.agentPrefab, this.spawnPoints[pointIndex], Quaternion.identity, 
                inputAuthority: player.Object.InputAuthority, onBeforeSpawned: (runner, newNO) =>
            {
                if (newNO.TryGetBehaviour(out PlayerAgent agent))
                {
                    agent.SetPlayerColor(this.playerColors[colorIndex]);
                    agent.Token = token;
                    this.TotalPlayerCount++;
                }
            });
        }

        void DespawnPlayerAgent(NetworkPlayer player)
        {
            if (null != player.ActiveAgent)
            {
                Runner.Despawn(player.ActiveAgent.Object);
                player.ActiveAgent = null;
            }
        }
    }
}
