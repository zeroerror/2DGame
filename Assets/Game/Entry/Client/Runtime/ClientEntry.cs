using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameArki.FreeInput;
using GameArki.PlatformerCamera;

namespace Game.Client {

    public class ClientEntry : MonoBehaviour {

        // = Core
        FreeInputCore inputCore;
        PFCore camCore;
        RoleEntity role;
        bool isAllLoaded;
        PhysicsScene physicsScene;

        void Awake() {
            DontDestroyOnLoad(gameObject);
            Init();
        }

        void Update() {
            if (!isAllLoaded) {
                return;
            }

            // - Physics
            var dt = Time.deltaTime;
            PhysicsUpdate(dt);

            camCore.Tick(dt);

            // - Control
            int horDir = 0;
            var getter = inputCore.Getter;
            if (getter.GetPressing(3)) {
                horDir--;
            }
            if (getter.GetPressing(4)) {
                horDir++;
            }
            if (horDir != 0) {
                role.Move(horDir);
            }
            if (getter.GetDown(5)) {
                role.Jump();
            }
        }

        float resTime;
        float fixedDt;
        void PhysicsUpdate(float dt) {
            resTime += dt;
            while (resTime >= fixedDt) {
                physicsScene.Simulate(fixedDt);
                resTime -= fixedDt;
            }
        }

        async void Init() {
            // - Assets
            await LoadAssets();

            // - Physics
            physicsScene = SceneManager.GetActiveScene().GetPhysicsScene();
            fixedDt = 0.02f;

            // - Role
            role = GameObject.FindObjectOfType<RoleEntity>();
            role.Ctor();
            role.LC.SetSpeed(5);
            role.LC.SetJumpSpeed(5);
            Debug.Assert(role);

            // - Input
            inputCore = new FreeInputCore();
            var setter = inputCore.Setter;
            setter.Bind(1, KeyCode.W);
            setter.Bind(1, KeyCode.UpArrow);
            setter.Bind(2, KeyCode.S);
            setter.Bind(2, KeyCode.DownArrow);
            setter.Bind(3, KeyCode.A);
            setter.Bind(3, KeyCode.LeftArrow);
            setter.Bind(4, KeyCode.D);
            setter.Bind(4, KeyCode.RightArrow);
            setter.Bind(5, KeyCode.Space);
            setter.Bind(5, KeyCode.Keypad0);

            // - Camera
            var mainCam = Camera.main;
            Debug.Assert(mainCam);
            camCore = new PFCore();
            camCore.Initialize(Camera.main);
            _ = camCore.SetterAPI.SpawnByMain(1);
            camCore.SetterAPI.Confiner_Set_Current(true, new Vector2(-20, -20), new Vector2(20, 20));
            camCore.SetterAPI.Follow_Current(
            role.transform, new Vector3(0, 3, -10),
            GameArki.FPEasing.EasingType.Linear, 0.5f,
            GameArki.FPEasing.EasingType.OutExpo, 0.8f);

            isAllLoaded = true;
        }

        async Task LoadAssets() {
        }

    }

}