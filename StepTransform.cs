using System.Numerics;
using Times;

namespace Transforms
{
    public class StepTransform : ITransform
    {
        private Vector3 _forward;
        private ITransform _impl;
        private float _rotateCurrentSpeed;
        private float _rotateAccel;
        private float _rotateSpeed;

        public Vector3 Forward
        {
            get => _forward;
            set
            {
                _forward = value;
                _rotateCurrentSpeed = _rotateSpeed;
            }
        }

        public ITransform Impl
        {
            set => _impl = value;
        }

        public Vector3 Position
        {
            get => _impl.Position;
            set => _impl.Position = value;
        }

        public float RotateAccel
        {
            set => _rotateAccel = value;
        }

        public float RotateSpeed
        {
            set => _rotateSpeed = value;
        }

        public Vector3 Up => _impl.Up;

        public void Step(ShortTimeSpan dt)
        {
            var forwardDelta = Forward - _impl.Forward;
            if (forwardDelta.LengthSquared() > 0)
            {
                _rotateCurrentSpeed += _rotateAccel * dt;
                _impl.Forward = TransformUtility.RotateTowards(_impl.Forward, Forward, _rotateCurrentSpeed);
            }
            else
            {
                _rotateCurrentSpeed = 0f;
            }
        }
    }
}