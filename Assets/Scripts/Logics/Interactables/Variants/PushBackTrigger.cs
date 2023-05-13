using GameTypes;
using System.Collections;
using UnityEngine;

namespace Logics.Interactables.Variants
{
    public class PushBackTrigger : MonoBehaviour, IInteractive
    {
        [SerializeField] private SkinnedMeshRenderer _skinnedMesh;
        [SerializeField] private float _bounceSpeed;
        private Coroutine _bounceCoroutine;

        public void OnCollideAt(Actor actor)
        {
            actor.PlayerMovement.OnPushBack();
            PlayerBounceAnim();
        }

        public void OnOverlapExit(Actor actor)
        {
            
        }

        private void PlayerBounceAnim()
        {
            if (_skinnedMesh != null)
            {
                _bounceCoroutine ??= StartCoroutine(BounceAnim());
            }
        }

        private IEnumerator BounceAnim()
        {
            float blendValue = 0.0f;

            while(blendValue < 100.0f)
            {
                blendValue += _bounceSpeed * Time.deltaTime;

                blendValue = Mathf.Clamp(blendValue, 0.0f, 100.0f);

                _skinnedMesh.SetBlendShapeWeight(0, blendValue);

                yield return null;
            }

            while (blendValue > 0.0f)
            {
                blendValue -= _bounceSpeed * Time.deltaTime;

                blendValue = Mathf.Clamp(blendValue, 0.0f, 100.0f);

                _skinnedMesh.SetBlendShapeWeight(0, blendValue);

                yield return null;
            }

            _bounceCoroutine = null;
        }
    }
}