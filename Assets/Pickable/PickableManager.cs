using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickableManager : MonoBehaviour
{
    private List<Pickable> _pickableList = new List<Pickable>();
    [SerializeField]
    private Player _player;
    [SerializeField]
    private ScoreManager _scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        InitPickableList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitPickableList()
    {
        Pickable[] pickableObjects = GameObject.FindObjectsOfType<Pickable>();
        for (int i = 0; i < pickableObjects.Length; i++)
        {
            _pickableList.Add(pickableObjects[i]);
            pickableObjects[i].OnPicked += OnPickablePicked;
        }

        _scoreManager.SetMaxScore(_pickableList.Count);

        Debug.Log("Pickable List: " +  _pickableList.Count);
    }

    private void OnPickablePicked(Pickable pickable)
    {
        if (pickable.pickableType == PickableType.PowerUp)
        {
            _player?.PickPowerUp();
        }

        _pickableList.Remove(pickable);
        Destroy(pickable.gameObject);
        Debug.Log("Pickable List: " + _pickableList.Count);
        
        if (_scoreManager != null)
        {
            _scoreManager.AddScore(1);
        }

        if (_pickableList.Count <= 0 ) 
        {
            Debug.Log("Win");
            SceneManager.LoadScene("WinScreen");
        }
    }
}
