using UnityEngine;

namespace NOC
{
    public class RandomWalker : MonoBehaviour
    {
        [SerializeField] private float stepSize = 5.0f;
        [SerializeField] private float stepSpeed = 5.0f;
        [SerializeField] private bool updateTargetPosWhenReach = true;
        [SerializeField] private bool smoothlyMovement = true;

        private Vector2 targetPosition;

        private void Start()
        {
            UpdateTargetPosition();
        }

        private void Update()
        {
            if (smoothlyMovement == true)
            {
                MoveSmoothly();
            }            
            else
            {
                MoveDirectly();
            }

            if (updateTargetPosWhenReach == true && IsReachTargetPosition() == true)
            {
                UpdateTargetPosition();
            }
        }

        private void MoveSmoothly()
        {
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * stepSpeed);
        }

        private void MoveDirectly()
        {
            this.transform.position = targetPosition;
        }

        private Vector2 GetTargetPosition()
        {
            return VectorHelper.GetRandom2DDir() * stepSize;
        }

        private bool IsReachTargetPosition()
        {
            return Vector3.Distance(this.transform.position, targetPosition) < 1.0f;
        }

        private void UpdateTargetPosition()
        {
            targetPosition = GetTargetPosition();
        }
    }
}