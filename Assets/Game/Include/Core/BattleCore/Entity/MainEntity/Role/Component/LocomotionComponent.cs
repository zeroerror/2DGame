using UnityEngine;

public class LocomotionComponent {

    Rigidbody2D rb;
    public Rigidbody2D Rb => rb;

    float speed;
    public float Speed => speed;
    public void SetSpeed(float value) => speed = value;

    float jumpSpeed;
    public float JumpSpeed => jumpSpeed;
    public void SetJumpSpeed(float value) => jumpSpeed = value;

    public LocomotionComponent() { }

    public void Ctor(Rigidbody2D rb) {
        this.rb = rb;
    }

    public void Move(int horDir) {
        var v = rb.velocity;
        if (horDir == 0) {
            v.x = 0;
        } else {
            v.x = horDir > 0 ? speed : -speed;
        }
        rb.velocity = v;
    }

    public void Jump() {
        var v = rb.velocity;
        v.y = speed;
        rb.velocity = v;
    }

}
