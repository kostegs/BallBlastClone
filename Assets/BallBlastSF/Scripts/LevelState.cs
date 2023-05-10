using UnityEngine;
using UnityEngine.Events;

public class LevelState : MonoBehaviour
{
    /* [SerializeField] private Cart _cart;
    [SerializeField] private StoneSpawner _stoneSpawner;

    [Space(5)]
    public UnityEvent Passed;
    public UnityEvent Defeat;

    private float _timer;
    private bool _checkPassed;

    private void Awake()
    {
        _cart.OnStoneCollision.AddListener(OnCartCollisionWithStone);        
    }

    private void OnDestroy()
    {
        _cart.OnStoneCollision.RemoveListener(OnCartCollisionWithStone);        
    }

    public void OnCartCollisionWithStone()
    {
        Defeat.Invoke();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > 0.5f)
        {
            if (_checkPassed == true && FindObjectsOfType<Stone>().Length == 0)
                Passed.Invoke();
            
            _timer= 0f;
        }        
    } */


}
