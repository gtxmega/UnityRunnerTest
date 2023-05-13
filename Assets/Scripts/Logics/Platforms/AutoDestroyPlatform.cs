using GameTypes;
using Logics.Interactables;
using System.Collections;
using UnityEngine;

namespace Logics.Platforms
{
    public class AutoDestroyPlatform : MonoBehaviour, IInteractive
    {
        [SerializeField] private GameObject _rootObject;
        [SerializeField] private float _delay;

        private Coroutine _destroyCoroutine;

        public void OnCollideAt(Actor actor)
        {
            RefreshCoroutine();
        }

        public void OnOverlapExit(Actor actor)
        {
            _destroyCoroutine = StartCoroutine(DelayDestroy());
        }

        private IEnumerator DelayDestroy()
        {
            yield return new WaitForSeconds(_delay);

            Destroy(_rootObject);
        }


        private void RefreshCoroutine()
        {
            if (_destroyCoroutine != null)
            {
                StopCoroutine(_destroyCoroutine);
                _destroyCoroutine = null;
            }
        }
    }
}