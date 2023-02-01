using UnityEngine;

public class RoleEntity : MonoBehaviour {

    LocomotionComponent lc;
    public LocomotionComponent LC => lc;

    FSMComponent fsm;
    public FSMComponent FSM => fsm;

    BoxCollider2D boxCollider2D;

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
        var pos = transform.position;
        pos.y -= 0.01f;
        if (Physics2D.Raycast(pos, Vector2.down, 0.01f)) {
            lc.Jump();
        }
    }

    int count = 0;

}
