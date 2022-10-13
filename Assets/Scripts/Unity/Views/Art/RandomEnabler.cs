using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RandomEnabler : MonoBehaviour
{
    [SerializeField] GameObject[] _list;

    protected void OnEnable()
    {
        if (!Application.isEditor)
            return;

        if (_list.Length <= 0)
            return;

        var selectedIndex = UnityEngine.Random.Range(0, _list.Length);

        for (int i = 0; i < _list.Length; i++)
        {
            if (i == selectedIndex)
                _list[i].SetActive(true);
            else
                _list[i].SetActive(false);
        }
    }
}

