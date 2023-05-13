using Logics.Interactables;
using Logics.Movements;
using UnityEngine;

namespace GameTypes
{
    public class Actor : MonoBehaviour
    {
        public Transform SelftTransform => _selfTransform;
        public ETeam Team => _team;
        public bool IsMainActor => _isMainActor;
        public PlayerMovement PlayerMovement => _playerMovement;

        [SerializeField] private Transform _selfTransform;
        [SerializeField] private ETeam _team;
        [SerializeField] private bool _isMainActor;
        
        [Header("References")]
        [SerializeField] private PlayerMovement _playerMovement;

        public void SwitchTeam(ETeam team)
            => _team = team;

        public Vector3 GetPosition() => _selfTransform.position;


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IInteractive>(out var interactive))
            {
                interactive.OnCollideAt(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IInteractive>(out var interactive))
            {
                interactive.OnOverlapExit(this);
            }
        }
    }
}