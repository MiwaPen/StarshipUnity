using UnityEngine;

namespace Main.Scripts.Services
{
   public class InputService: MonoBehaviour
   {
      protected Vector2 _axis;
      protected float _horizontal;
      protected float _vertical;
      protected bool _isInputLogin;

      public Vector2 Axis => _axis;
      public float Horizontal => _horizontal;
      public float Vertical => _vertical;
      public bool IsInputLogin => _isInputLogin;
   }
}
