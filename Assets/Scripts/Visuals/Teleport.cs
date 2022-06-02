using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Visuals
{
    public class Teleport : MonoBehaviour
    {

        public CinemachineVirtualCamera camera;
        private CinemachineConfiner _confiner;
        public GameObject cameraBounds;
        private Collider2D _cameraBoundsCollider;

        public GameObject space;
        public GameObject player;

        void Start()
        {
            _confiner = camera.GetComponent<CinemachineConfiner>();
            _cameraBoundsCollider = cameraBounds.GetComponent<Collider2D>();
        }
    
        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            if (collider2D.gameObject.CompareTag("Player"))
            {
                StartCoroutine(TeleportPlayer());
            }
        }
    
        IEnumerator TeleportPlayer()
        {
            yield return new WaitForSeconds(0);
            player.transform.position = space.transform.position;
            _confiner.m_BoundingShape2D = _cameraBoundsCollider;
            _confiner.m_Damping = 0;
        }
    }
}
