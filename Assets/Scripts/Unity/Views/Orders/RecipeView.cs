using Assets.Scripts.Core;
using TMPro;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Orders
{
    public class RecipeView : ViewMonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _count;
        [SerializeField] TextMeshProUGUI _name;

        //private int _currentCount;
        //private int _maxCount;
        //public void SetCount(int count)
        //{
        //    _currentCount = count;
        //    UpdateCount();
        //}

        //public void SetMaxCount(int max)
        //{
        //    _maxCount = max;
        //    UpdateCount();
        //}

        //public void SetName(string name)
        //{
        //    _name.text = name;
        //}

        //private void UpdateCount()
        //{
        //    _count.text = $"{_currentCount}/{_maxCount}";
        //}

    }
}