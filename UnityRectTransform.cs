﻿using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Transforms
{
    public class UnityRectTransform : IRectTransform
    {
        private RectTransform _impl;

        public RectTransform Impl
        {
            set
            {
                _impl = value;
                Transform = new UnityTransform
                {
                    Impl = value
                };
            }
        }

        public float Height
        {
            get => _impl.rect.size.y;
            set => _impl.sizeDelta = new UnityEngine.Vector2(_impl.sizeDelta.x, value);
        }

        public ITransform Parent
        {
            set => _impl.SetParent(value.GetComponent<Transform>(), false);
        }

        public Vector2 Size
        {
            get => UnityUtils.Convert(_impl.rect.size);
            set => _impl.sizeDelta = UnityUtils.Convert(value);
        }

        public ITransform Transform { get; private set; }
        public float Width
        {
            get => _impl.rect.size.x;
            set => _impl.sizeDelta = new UnityEngine.Vector2(value, _impl.sizeDelta.y);
        }
    }
}