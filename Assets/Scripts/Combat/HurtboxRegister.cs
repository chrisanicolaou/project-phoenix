using System;
using ChiciStudios.ProjectPhoenix.Utils;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Combat
{
    public class HurtboxRegister : MonoBehaviour
    {
        [SerializeField, RequireInterface(typeof(IHurtboxReceiver))]
        private UnityEngine.Object _receiverObject;

        private IHurtboxReceiver _receiver;

        private void Awake()
        {
            _receiver = _receiverObject as IHurtboxReceiver;
        }

        public void RegisterHit(HitInfo hit)
        {
            _receiver.OnHit(hit);
        }
    }
}