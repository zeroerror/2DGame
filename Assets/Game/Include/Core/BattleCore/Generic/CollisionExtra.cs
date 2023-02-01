using UnityEngine;

namespace Game.CoreBattle.Generic
{
    
    public enum CollisionStatus
    {
        None,
        Enter,
        Stay,
        Exit
    }

    public class CollisionExtra
    {

        public CollisionStatus status;

        public Collision2D collision;
        public GameObject gameObject;
        public Collider2D GetCollider() => gameObject != null ? gameObject.GetComponent<Collider2D>() : null;

        public string layerName;

        public FieldType fieldType;

        public Vector2 hitDir;

    }

}