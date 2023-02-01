using System;
using System.Collections.Generic;
using UnityEngine;
using Game.CoreBattle.Generic;

namespace Game.CoreBattle {

    public class PhysicsEntity : MonoBehaviour {

        List<CollisionExtra> collisionList;

        void Awake() {
            // Debug.Log($"PhysicsEntity Created! {gameObject.name}");
            collisionList = new List<CollisionExtra>();
        }

        public void CollisionExtraListForeach(Action<CollisionExtra> action) {
            collisionList.ForEach((collider) => {
                action.Invoke(collider);
            });
        }

        public bool RemoveHitCollider(Collider2D collider) {
            var e = collisionList.GetEnumerator();
            while (e.MoveNext()) {
                if (e.Current.collision.Equals(collider)) {
                    break;
                }
            }
            return collisionList.Remove(e.Current);
        }

        public bool RemoveHitCollisionExtra(CollisionExtra colliderExtra) {
            return collisionList.Remove(colliderExtra);
        }

        #region [Unity Physics ]

        void OnTriggerEnter2D(Collider2D collider) {
            if (Exist(collider)) {
                return;
            }

            CollisionExtra ce = new CollisionExtra();
            ce.status = CollisionStatus.Enter;
            ce.gameObject = collider.gameObject;
            ce.layerName = LayerMask.LayerToName(collider.gameObject.layer);
            ce.fieldType = FieldType.Ground;
            collisionList.Add(ce);
        }

        void OnTriggerStay2D(Collider2D collider) {
        }

        void OnTriggerExit2D(Collider2D collider) {
            var ce = Find(collider);
            if (ce == null) {
                return;
            }

            ce.status = CollisionStatus.Exit;
        }

        void OnCollisionEnter2D(Collision2D collision) {
            if (Exist(collision.collider)) {
                return;
            }

            FieldType fieldType = FieldType.None;
            string layerName = LayerMask.LayerToName(collision.gameObject.layer);
            Vector2 hitDir = Vector2.zero;
            if (layerName == "Field") {
                var contactPoint = collision.GetContact(0).point;
                hitDir = collision.GetContact(0).normal;
                if (hitDir.y > 0) {
                    fieldType = FieldType.Ground;
                } else {
                    fieldType = FieldType.Wall;
                }
            } else {
                fieldType = FieldType.None;
            }

            CollisionExtra collisionExtra = new CollisionExtra();
            collisionExtra.status = CollisionStatus.Enter;
            collisionExtra.collision = collision;
            collisionExtra.gameObject = collision.gameObject;
            collisionExtra.fieldType = fieldType;
            collisionExtra.layerName = LayerMask.LayerToName(collision.gameObject.layer);
            collisionExtra.hitDir = hitDir;
            collisionList.Add(collisionExtra);
        }

        void OnCollisionStay2D(Collision2D collision) {
        }

        void OnCollisionExit2D(Collision2D collision) {
            var ce = Find(collision.collider);
            if (ce == null) {
                return;
            }

            ce.status = CollisionStatus.Exit;
        }

        #endregion

        CollisionExtra Find(Collider2D collider) {
            return collisionList.Find((colliderExtra) => colliderExtra.gameObject.Equals(collider.gameObject));
        }

        bool Exist(Collider2D collider) {
            CollisionExtra colliderExtra = collisionList.Find((ce) => ce.gameObject.Equals(collider.gameObject));
            return colliderExtra != null;
        }

    }

}