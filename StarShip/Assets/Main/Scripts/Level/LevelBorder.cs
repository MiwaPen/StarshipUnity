using UnityEngine;

namespace Main.Scripts.Level
{
    public class LevelBorder : MonoBehaviour
    {
        [SerializeField][Min(0f)] private float _heigh;
        [SerializeField][Min(0f)] private float _width;
        private Vector2 _center;
        
        private void Awake() => _center = transform.position;
        
        public float GetEdgePos(EdgeType type)
        {
            float result = 0 ;
            switch (type)
            {
                case EdgeType.LEFT:result= _center.x - _width / 2; break;
                case EdgeType.RIGHT:result= _center.x + _width / 2; break;
                case EdgeType.TOP:result= _center.y + _heigh / 2; break;
                case EdgeType.BOTTOM:result= _center.y - _heigh / 2; break;
            }

            return result;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            var size = new Vector3(_width, _heigh, 1);
            Gizmos.DrawCube(transform.position,size);
        }
    }

    public enum EdgeType
    {
        LEFT,
        RIGHT,
        TOP,
        BOTTOM
    }
}
