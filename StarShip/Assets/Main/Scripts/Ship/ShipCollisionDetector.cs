using System;
using UnityEngine;

namespace Main.Scripts.Ship
{
    public class ShipCollisionDetector : MonoBehaviour
    {
        public event Action OnCollisionDetect;
        public bool IsCollided => _currentCollidedObjectCount > 0;
        private int _currentCollidedObjectCount;
        public bool isEnabled = false;

        private void OnCollisionEnter(Collision other)
        {
            if(!isEnabled) return;
            _currentCollidedObjectCount++;
            OnCollisionDetect?.Invoke();
        }

        private void OnCollisionExit(Collision other)
        {   
            if(!isEnabled) return;
            _currentCollidedObjectCount++;
        }
    }
}
