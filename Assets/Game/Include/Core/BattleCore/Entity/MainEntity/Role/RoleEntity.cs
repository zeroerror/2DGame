using UnityEngine;

namespace Game.CoreBattle {

    public class RoleEntity : PhysicsEntity {

        LocomotionComponent lc;
        public LocomotionComponent LC => lc;

        FSMComponent fsm;
        public FSMComponent FSM => fsm;

        BoxCollider2D boxCollider2D;

        public bool isGrounded;

        public void Ctor() {
            var rb = GetComponent<Rigidbody2D>();
            boxCollider2D = GetComponent<BoxCollider2D>();
            lc = new LocomotionComponent();
            lc.Ctor(rb);
            fsm = new FSMComponent();
        }

        public void Move(int horDir) {
            lc.Move(horDir);
        }

        public void Jump() {
            if (isGrounded) {
                lc.Jump();
            }
        }

    }

}