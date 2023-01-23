using UnityEngine;

namespace E7.IntroloopDemo
{
    internal class IntroloopCubeMove : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        private bool forward, backward, left, right;

        public void Update()
        {
            var pos = transform.position;
            if (forward)
            {
                pos.z += moveSpeed;
            }

            if (backward)
            {
                pos.z -= moveSpeed;
            }

            if (left)
            {
                pos.x -= moveSpeed;
            }

            if (right)
            {
                pos.x += moveSpeed;
            }

            transform.position = pos;
        }

        public void Forward()
        {
            forward = true;
        }

        public void ForwardUp()
        {
            forward = false;
        }

        public void Backward()
        {
            backward = true;
        }

        public void BackwardUp()
        {
            backward = false;
        }

        public void Left()
        {
            left = true;
        }

        public void LeftUp()
        {
            left = false;
        }

        public void Right()
        {
            right = true;
        }

        public void RightUp()
        {
            right = false;
        }
    }
}