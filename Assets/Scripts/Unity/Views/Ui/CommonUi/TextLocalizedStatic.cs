using System;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using TMPro;
using UnityEngine;

namespace GameUnity.Unity.Views.Ui.CommonUi
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextLocalizedStatic : MonoBehaviour
    {
        [SerializeField] private string _tag;
        public void Awake()
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = new LocalizatedString(_tag).Get();
        }
    }
}