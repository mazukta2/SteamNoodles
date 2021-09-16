
using System;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class GameMonoBehaviour : MonoBehaviour
    {
        public bool IsDestoyed { get; set; }

        public void Destroy()
        {
            IsDestoyed = true;
            GameObject.Destroy(this);
        }
    }
}