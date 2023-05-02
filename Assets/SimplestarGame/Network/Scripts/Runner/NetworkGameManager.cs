using Fusion;
using System.Collections.Generic;
using UnityEngine;

namespace SimplestarGame
{
    [RequireComponent(typeof(NetworkRunner))]
    public class NetworkGameManager : SimulationBehaviour, IPlayerJoined, IPlayerLeft
    {
        [SerializeField] NetworkGame networkGame;
        [SerializeField] NetworkPlayer networkPlayer;

        Dictionary<PlayerRef, NetworkPlayer> NetworkPlayers { get; set; } = new Dictionary<PlayerRef, NetworkPlayer>(200);

        void IPlayerJoined.PlayerJoined(PlayerRef playerRef)
        {
            if (!Runner.IsServer)
            {
                return;
            }
            if (0 == FindObjectsOfType<NetworkGame>().Length)
            {
                Runner.Spawn(this.networkGame);
            }
            var networkPlayer = Runner.Spawn(this.networkPlayer, inputAuthority: playerRef);
            this.NetworkPlayers.Add(playerRef, networkPlayer);
            Runner.SetPlayerObject(playerRef, networkPlayer.Object);
        }

        void IPlayerLeft.PlayerLeft(PlayerRef playerRef)
        {
            if (!Runner.IsServer)
            {
                return;
            }
            if (this.NetworkPlayers.TryGetValue(playerRef, out NetworkPlayer player))
            {
                Runner.Despawn(player.Object);
            }
        }
    }
}
