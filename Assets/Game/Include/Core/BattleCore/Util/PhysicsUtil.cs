using System.Collections.Generic;
using Game.CoreBattle.Generic;
using UnityEngine;

namespace Game.CoreBattle.Util {

    public class PhysicsUtil {

        public static List<CollisionExtra> FetchCollisionExtraList_Field(PhysicsEntity physicsEntity) => FetchCollisionExtraList(physicsEntity, "Field");

        public static List<CollisionExtra> FetchCollisionExtraList(PhysicsEntity physicsEntity, string layerName) {
            List<CollisionExtra> collisionList = new List<CollisionExtra>();
            List<CollisionExtra> removeList = new List<CollisionExtra>();
            physicsEntity.CollisionExtraListForeach((collisionExtra) => {

                if (collisionExtra.status == CollisionStatus.Exit) {
                    // Exit -> Remove.
                    removeList.Add(collisionExtra);
                }

                Collider2D collider = collisionExtra.GetCollider();
                if (collider == null || collider.enabled == false) {
                    // Destroy or Disabled -> Remove.
                    collisionExtra.status = CollisionStatus.Exit;
                    removeList.Add(collisionExtra);
                }

                if (collisionExtra.layerName == layerName) {
                    collisionList.Add(collisionExtra); 
                }
            });

            removeList.ForEach((ce) => {
                physicsEntity.RemoveHitCollisionExtra(ce);
            });

            return collisionList;
        }

    }

}