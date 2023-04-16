using UnityEngine;

public class CartInputController : MonoBehaviour
{
    [SerializeField] private Cart _cart;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Turret _turret;    
    
    private void Update()
    {
        _cart.SetMovementTarget(_mainCamera.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetMouseButton(0))
            _turret.Fire();
    }
}
