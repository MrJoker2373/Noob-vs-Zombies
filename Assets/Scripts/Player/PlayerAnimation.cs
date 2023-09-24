namespace Game.Player
{
    using UnityEngine;
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator _animator;
        private Vector2 _direction;
        public Vector2 Direction
        {
            set
            {
                if (_direction == value)
                    return;
                _direction = value;
                SetDirection();
                SetAnimator();
            }
        }
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        private void Start()
        {
            SetOrientation(Orientation.Down);
        }
        private void SetDirection()
        {
            if (_direction.x == 0)
            {
                if (_direction.y < 0)
                    SetOrientation(Orientation.Down);
                else if (_direction.y > 0)
                    SetOrientation(Orientation.Up);
            }
            else
            {
                if (_direction.x < 0)
                    SetOrientation(Orientation.Left);
                else if (_direction.x > 0)
                    SetOrientation(Orientation.Right);
            }
        }
        private void SetAnimator()
        {
            if (_direction.x == 0 && _direction.y == 0)
                _animator.SetBool("IsMoving", false);
            else
                _animator.SetBool("IsMoving", true);
        }
        private void SetOrientation(Orientation orientation)
        {
            if (orientation == Orientation.Left)
            {
                SetLayer(1);
                if (IsRight())
                    Flip();
            }
            else if (orientation == Orientation.Right)
            {
                SetLayer(1);
                if (IsLeft())
                    Flip();
            }
            else if (orientation == Orientation.Down)
                SetLayer(2);
            else
                SetLayer(3);
        }
        private void Flip()
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        private void SetLayer(int index)
        {
            index = Mathf.Clamp(index, 0, _animator.layerCount - 1);
            for (int i = 0; i < _animator.layerCount; i++)
            {
                if (i == index)
                    _animator.SetLayerWeight(i, 1);
                else
                    _animator.SetLayerWeight(i, 0);
            }
        }
        private bool IsRight() { return transform.localScale.x > 0; }
        private bool IsLeft() { return transform.localScale.x < 0; }
    }
    public enum Orientation { Left, Right, Down, Up }
}