using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorController : Controller
{
    private const float DIACTIVATE_EXCEPTION_TIME = 1.5f;

    [Header("UI")] 
    [SerializeField] private GameObject _textInteract;
    [SerializeField] private GameObject _textInteractException;

    [Header("Modules")]
    [SerializeField] private PlayerController _playerController;
    
    private List<IPuzzle> _interactableObjects = new ();

    private Coroutine _distanceChecker;

    private bool _isDiactivatingException = false;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnInteract();
        }        
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_interactableObjects.Count <= 0)
            {
                return;
            }
            
            _interactableObjects[0].DiactivatePuzzle();
        }
    }

    public void OnInteract()
    {
        if (_interactableObjects.Count <= 0)
        {
            InteractException();
            return;
        }

        if (_interactableObjects[0].ActivatePuzzle())
        {
            InteractSuccessfully();
        }
        else
        {
            InteractException();
        }
    }

    public void InteractSuccessfully()
    {
        _playerController.IsMove = false;
    }

    public void InteractException()
    {
        _textInteractException.SetActive(true);

        if (!_isDiactivatingException)
        {
            _isDiactivatingException = true;
            Invoke(nameof(DiactivateException), DIACTIVATE_EXCEPTION_TIME);
        }
    }

    private void DiactivateException()
    {
        _textInteractException.SetActive(false);
        _isDiactivatingException = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IPuzzle interactable))
        {
            _interactableObjects.Add(interactable);

            if (_distanceChecker == null)
            {
                _distanceChecker = StartCoroutine(CheckDistance());
                _textInteract.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IPuzzle interactable))
        {
            _interactableObjects.Remove(interactable);
            
            if ((_distanceChecker != null) && (_interactableObjects.Count <= 0))
            {
                StopCoroutine(CheckDistance());
                _distanceChecker = null;
                _textInteract.SetActive(false);
            }
        }
    }

    private IEnumerator CheckDistance()
    {
        while (_interactableObjects.Count > 0)
        {
            _interactableObjects.Sort((i1, i2) =>
            {
                float distance1 = Vector2.Distance((i1 as MonoBehaviour).transform.position, transform.position);
                float distance2 = Vector2.Distance((i2 as MonoBehaviour).transform.position, transform.position);

                return distance1 < distance2 ? -1 : 1;
            });

            yield return null;
        }
    }
}
