using System;
using Main.Scripts.Level;
using Main.Scripts.Services;
using UnityEngine;

namespace Main.Scripts.Ship
{
    public class ShipMovementComponent : MonoBehaviour
    {
        public Transform CachedTransform { get; private set; }
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _maxAngleDispersion;
        [SerializeField] private float _FlyAcceleration;
        [SerializeField] private float _FallAcceleration;
        [SerializeField] private float _minimalMovementThreshhold;
        [SerializeField] private Transform _model;
        private float _currentAcceleration;
        public bool isEnabled = false;
        private InputService _inputController;
        private LevelBorder _borders;
        public void Initialize(InputService inputController, LevelBorder borders)
        {
            _inputController = inputController;
            _borders = borders;
            CachedTransform = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            if(isEnabled==false) return;
            
            if(_inputController.IsInputLogin)
                _currentAcceleration = Mathf.Clamp(_currentAcceleration+_FlyAcceleration,-1f, 1f);
            else
                _currentAcceleration = Mathf.Clamp(_currentAcceleration-_FallAcceleration,-1f, 1f);
        }

        public void Move()
        {
            if(isEnabled==false) return;
            
            var newPos = CachedTransform.position;
            
            if(_currentAcceleration>0)
                newPos += CachedTransform.up* _moveSpeed*_currentAcceleration * Time.deltaTime;
            else
                newPos.y += _moveSpeed*_currentAcceleration*Time.deltaTime;
            
            newPos.x = Mathf.Clamp(newPos.x, _borders.GetEdgePos(EdgeType.LEFT), _borders.GetEdgePos(EdgeType.RIGHT));
            newPos.y = Mathf.Clamp(newPos.y, _borders.GetEdgePos(EdgeType.BOTTOM), _borders.GetEdgePos(EdgeType.TOP));
      
            CachedTransform.position = newPos;
        }
        
        public void ApplyRotation()
        {
            if(isEnabled==false) return;
            
            if (!_inputController.IsInputLogin)
            {
                _model.localRotation =Quaternion.Euler(_model.localRotation.eulerAngles.x,
                    _model.localRotation.eulerAngles.y+2, _model.localRotation.eulerAngles.x);
                return;
            }
            if(Math.Abs(_inputController.Horizontal) <_minimalMovementThreshhold)
                RotateToDefault();
            else
                ControlledRotation(-_inputController.Horizontal);
        }

        private void ControlledRotation(float acceleration)
        {
            var angle = CachedTransform.rotation.eulerAngles.z;
            angle += _rotationSpeed*acceleration*Time.deltaTime;
            if (angle < 0) angle =Mathf.Clamp( 360 - (angle * -1),0, 360);

            if (0 <= angle && angle < 180)
                angle = Mathf.Clamp(angle, 0, _maxAngleDispersion);
            else
                angle = Mathf.Clamp(angle, 360-_maxAngleDispersion, 360);
            
            transform.rotation = Quaternion.Euler(0,0, angle);
        }

        private void RotateToDefault()
        {
            var angle = CachedTransform.rotation.eulerAngles.z;
            if (angle < 0) angle =Mathf.Clamp( 360 - (angle * -1),0, 360);

            var speed = _rotationSpeed / 2;

            if (angle < 180)
                angle = Mathf.Clamp(angle-speed * Time.deltaTime,0,_maxAngleDispersion);
            else
                angle = Mathf.Clamp(angle+speed * Time.deltaTime,360-_maxAngleDispersion,360);
                
            transform.rotation = Quaternion.Euler(0,0, angle);
        }
    }
}
