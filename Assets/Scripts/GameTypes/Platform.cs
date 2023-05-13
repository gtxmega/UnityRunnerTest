using UnityEngine;

namespace GameTypes
{
    public class Platform : MonoBehaviour
    {
        public Transform SelftTransform => _selfTransform;

        [SerializeField] private Transform _selfTransform;
        [SerializeField] private Collider _selfCollider;

        public float GetSizeZ() => _selfCollider.bounds.extents.z * 2.0f;

        public Vector3 GetPosition() => _selfTransform.position;
        public void SetPosition(Vector3 position) => _selfTransform.position = position;
    }
}