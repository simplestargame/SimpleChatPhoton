using Fusion;
using UnityEngine;

namespace SimplestarGame
{
    public class PlayerAgent : NetworkBehaviour
    {
        [SerializeField] TMPro.TextMeshPro textMeshPro;

        [Networked] internal int Token { get; set; }
        [Networked(OnChanged = nameof(OnChangedMessage), OnChangedTargets = OnChangedTargets.Proxies)] NetworkString<_64> Message { get; set; }
        [Networked(OnChanged = nameof(OnChangedColor), OnChangedTargets = OnChangedTargets.All)] Color Color { get; set; }

        [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority)]
        public void RPC_SendMessage(string message)
        {
            if (null != this.textMeshPro)
            {
                this.textMeshPro.text = message;
            }
            this.Message = message;
        }

        public static void OnChangedMessage(Changed<PlayerAgent> changed)
        {
            changed.Behaviour.ChangeMessage();
        }

        void ChangeMessage()
        {
            if (null != this.textMeshPro)
            {
                this.textMeshPro.text = this.Message.Value;
            }
        }

        public static void OnChangedColor(Changed<PlayerAgent> changed)
        {
            changed.Behaviour.ChangedColor();
        }

        void ChangedColor()
        {
            if (null != this.textMeshPro)
            {
                this.textMeshPro.color = this.Color;
            }
        }

        internal void SetPlayerColor(Color color)
        {
            this.Color = color;
            this.Message = "Hi.";
            if (null != this.textMeshPro)
            {
                this.textMeshPro.text = this.Message.Value;
            }
        }

        public override void Spawned()
        {
            this.name = "[Network]PlayerAgent";
            if (null == this.mainCamera)
            {
                this.mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
            }
            if (null != this.textMeshPro)
            {
                this.textMeshPro.text = this.Message.Value;
            }
            if (null != SceneContext.Instance.buttonSend)
            {
                SceneContext.Instance.buttonSend.onPressed += this.OnSend;
            }
        }

        void OnSend()
        {
            if (!HasInputAuthority)
            {
                return;
            }
            if (null != SceneContext.Instance.inputField)
            {
                if (null != this.textMeshPro)
                {
                    this.textMeshPro.text = SceneContext.Instance.inputField.text;
                }
                this.RPC_SendMessage(SceneContext.Instance.inputField.text);
            }
        }

        internal void ApplyInput(PlayerInput input)
        {
            this.transform.position += input.move * this.moveSpeed;
        }

        float moveSpeed = 0.1f;
        Transform mainCamera;
    }
}
