using Cinemachine;
using System.Collections;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Level.Common
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class UnityShaker : MonoBehaviour
    {
        public static UnityShaker Instance { get; private set; }
        protected void Awake()
        {
            Instance = this;
        }

        public void Shake()
        {
            GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        }
    }
}