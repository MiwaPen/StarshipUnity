using System;
using Main.Scripts.Services;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main.Scripts.Player
{
  public class PlayerInputController : InputService, IPointerDownHandler, IDragHandler, IPointerUpHandler
  {
    public void EnableInputHandle() => Enabled = true;
    public void DisableInputHandle() => Enabled = false;
    
    public bool Enabled = true;

    [SerializeField] private float _maxDistanceValue;
    [SerializeField][Range(0f,1f)] private float _threshold;
    private Vector2 _startPosData;

    public void OnPointerDown(PointerEventData eventData)
    {
      if(!Enabled) return;
      _startPosData = eventData.position;
      _isInputLogin = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
      if(!Enabled) return;
      
      var xDist =  _startPosData.x - eventData.position.x;
      var yDist =  _startPosData.y - eventData.position.y;
      _horizontal =  Math.Abs(xDist) >= _maxDistanceValue ? xDist<0?1:-1 : xDist<0?  Math.Abs(xDist) / _maxDistanceValue:  (Math.Abs(xDist) / _maxDistanceValue)*-1;
      _vertical =  Math.Abs(yDist) >= _maxDistanceValue ? xDist<0?1:-1 : yDist<0?  Math.Abs(yDist) / _maxDistanceValue:  (Math.Abs(yDist) / _maxDistanceValue)*-1;

      _horizontal = Math.Abs(_horizontal) < _threshold ? 0 : _horizontal;
      _vertical = Math.Abs(_vertical) < _threshold ? 0 : _vertical;
      
      _axis = new Vector2(_horizontal, _vertical);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      if(!Enabled) return;
      _isInputLogin = false;
      _startPosData = Vector2.zero;
      _axis = Vector2.zero;
      _horizontal = 0;
      _vertical = 0;
    }
  }
}