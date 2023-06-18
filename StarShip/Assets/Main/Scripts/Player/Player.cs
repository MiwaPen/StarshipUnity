using Main.Scripts.Level;
using Main.Scripts.Services;
using Main.Scripts.Ship;
using UnityEngine;

namespace Main.Scripts.Player
{
   public class Player : MonoBehaviour
   {
      [SerializeField] private ShipMovementComponent _movementComponent;
      [SerializeField] private ShipCollisionDetector _collisionDetector;
      [SerializeField] private ParticleSystem _fireParticleSystem;
      private LevelBorder _borders;
      private InputService _inputController;
      private bool enable;
      
      public void Initialize(InputService inputController, LevelBorder border)
      {
         _inputController = inputController;
         _borders = border;
         SetupShip();
      }

      public void SetupShip()
      {
         _movementComponent.Initialize(_inputController,_borders);
         _collisionDetector.OnCollisionDetect += OnPlayerCatchCollision;
      }

      public void EnableShip()
      {
         enable = true;
         _collisionDetector.isEnabled = true;
         _movementComponent.isEnabled = true;
      }

      private void Update()
      {
         if(enable==false) return;
         
         _movementComponent.Move();
         _movementComponent.ApplyRotation();
         /*if (_inputController.IsInputLogin)
            _fireParticleSystem.Play();*/
      }

      private void OnPlayerCatchCollision()
      {
         _collisionDetector.OnCollisionDetect -= OnPlayerCatchCollision;
         Debug.Log("GameOver");
      }
   }
}
