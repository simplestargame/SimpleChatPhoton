using Fusion;

namespace SimplestarGame
{
    public class NetworkPlayer : NetworkBehaviour, IBeforeTick
    {
        [Networked] internal PlayerAgent ActiveAgent { get; set; }

        public override void Spawned()
        {
            this.name = "[Network]Player:" + this.Object.InputAuthority;
            SceneContext.Instance.Game?.Join(this);
        }

        void IBeforeTick.BeforeTick()
        {
            if (this.GetInput(out PlayerInput input))
            {
                this.ActiveAgent?.ApplyInput(input);
            }
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            if (!hasState)
            {
                return;
            }

            SceneContext.Instance.Game?.Leave(this);
        }
    }
}
