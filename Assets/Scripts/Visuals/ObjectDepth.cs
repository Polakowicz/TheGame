using UnityEngine;

namespace Visuals
{
    public class ObjectDepth : MonoBehaviour
    {
        public int _spriteSortingLayer;
        public float _spriteYValue;
        public float _playerYValue;
        public bool _isNear;

        private SpriteRenderer _sprite;
        public GameObject player;
        void Start()
        {
            _sprite = GetComponent<SpriteRenderer>();

            _isNear = false;
            _spriteSortingLayer = _sprite.sortingOrder;
            _spriteYValue = transform.position.y;
        }
    
        void Update()
        {
            _playerYValue = player.transform.position.y;
            if (_isNear)
            {
                if (_spriteYValue > _playerYValue)
                    _sprite.sortingOrder = 2;
                else
                    _sprite.sortingOrder = 4;
            }
        }

        private void OnTriggerStay2D(Collider2D collider2D)
        {
            if (collider2D.gameObject.CompareTag("Player"))
                _isNear = true;
        }

        private void OnTriggerExit2D(Collider2D collider2D)
        {
            if (collider2D.gameObject.CompareTag("Player"))
            {
                _isNear = false;
                _sprite.sortingOrder = _spriteSortingLayer;
            }
        }
    }
}
