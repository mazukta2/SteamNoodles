using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Views.Art
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour
    {
        [SerializeField] Sprite[] _sprites;
        [SerializeField] float _time = 0.3f;
        [SerializeField] float _randomization = 0.1f;

        private SpriteRenderer _spriteRenderer;
        private int _index;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            StartCoroutine(Updater());
        }

        IEnumerator Updater()
        {
            while (true)
            {
                if (_sprites.Length > 0 && _index < _sprites.Length)
                    _spriteRenderer.sprite = _sprites[_index];
                
                yield return new WaitForSeconds(_time + UnityEngine.Random.Range(-_randomization, _randomization));
                _index++;
                if (_index >= _sprites.Length)
                    _index = 0;
            }
        }
    }
}