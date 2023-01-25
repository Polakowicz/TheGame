using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ArenaTeleport : MonoBehaviour
{
    private AudioManager audioManager;
    private SpriteRenderer _spriteRenderer;
    private int numberOfTriggers;

    public CinemachineVirtualCamera _camera;
    private CinemachineConfiner _confiner;
    public GameObject _cameraBounds;
    private Collider2D _cameraBoundsCollider;

    public PlayableDirector timeline;
    public PlayableAsset cutscene;
    public bool hasPlayed = false;
    
    
    public Image _image;
    
    public GameObject arena;
    public GameObject player;
    public List<Sprite> _sprites;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _confiner = _camera.GetComponent<CinemachineConfiner>();
        _cameraBoundsCollider = _cameraBounds.GetComponent<Collider2D>();
        numberOfTriggers = 0;
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Player"))
        {
            switch (numberOfTriggers)
            {
                case 0:
                    audioManager.Play("CaveHole");
                    _spriteRenderer.sprite = _sprites[0];
                    numberOfTriggers += 1;
                    break;
                case 1:
                    audioManager.Play("CaveHole");
                    _spriteRenderer.sprite = _sprites[1];
                    numberOfTriggers += 1;
                    if (!hasPlayed)
                    {
                        hasPlayed = true;
                        timeline.playableAsset = cutscene;
                        timeline.Play();
                    }
                    StartCoroutine(Teleport());
                    audioManager.Play("CaveFall");
                    break;
                case 2:
                    _spriteRenderer.sprite = _sprites[2];
                    numberOfTriggers += 1;
                    break;
            }
        }
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(2f);
        player.transform.position = arena.transform.position;
        _confiner.m_BoundingShape2D = _cameraBoundsCollider;
        _confiner.m_Damping = 0;
        _image.enabled = false;
    }
}
