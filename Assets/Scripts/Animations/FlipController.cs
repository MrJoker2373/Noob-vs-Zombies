namespace Game
{
    using UnityEngine;
    public class FlipController : MonoBehaviour
    {
        public void SetFlip(Vector3 direction)
        {
            if (direction.x < 0 && transform.localScale.x > 0 ||
                direction.x > 0 && transform.localScale.x < 0)
                Flip();
        }
        private void Flip()
        {
            Vector3 scale = transform.localScale;
            scale.x = -scale.x;
            transform.localScale = scale;
        }
    }
}