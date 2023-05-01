using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YATE.AI;

namespace YATE
{
    public class Billboard3D : MonoBehaviour
    {
        [SerializeField] private CharacterController character;
        [SerializeField] private EnemyAIAgent agent;

        private Camera cam;
        private Vector3 cameraDirection;

        private Vector3 defaultLocalScale;

        // Start is called before the first frame update
        void Start()
        {
            cam = Camera.main;
            defaultLocalScale = transform.localScale;
        }

        // Update is called once per frame
        void Update()
        {
            HandleFlip();

            cameraDirection = cam.transform.forward;
            cameraDirection.y = 0f;

            transform.rotation = Quaternion.LookRotation(cameraDirection);
        }

        private void HandleFlip()
        {
            Vector3 moveDir = Vector3.zero;
            if (character != null)
            {
                moveDir = character.velocity;
            }
            else if (agent != null)
            {
                moveDir = agent.Navigator.velocity;
            }

            if (moveDir.x < 0f)
            {
                transform.localScale = new Vector3(Mathf.Abs(defaultLocalScale.x), defaultLocalScale.y, defaultLocalScale.z);
            }
            else if (moveDir.x > 0f)
            {
                transform.localScale = new Vector3(-Mathf.Abs(defaultLocalScale.x), defaultLocalScale.y, defaultLocalScale.z);
            }
        }
    }
}