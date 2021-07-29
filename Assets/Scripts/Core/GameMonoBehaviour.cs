using Assets.Scripts.Models;
using Assets.Scripts.Models.Requests;
using System;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class GameMonoBehaviour : MonoBehaviour
    {
        protected IMessageBroker Messages => MessageBroker.Default;

    }
}