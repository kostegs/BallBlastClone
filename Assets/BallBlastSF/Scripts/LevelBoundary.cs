using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
    public static LevelBoundary Instance;

    [SerializeField] private Vector2 _screenResolution;
    [SerializeField] private GameObject _leftEdge;
    [SerializeField] private GameObject _rightEdge;

    public float LeftBorder
    {
        get => Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
    }

    public float RightBorder
    {
        get => Camera.main.ScreenToWorldPoint(new Vector3(_screenResolution.x, 0, 0)).x;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (Application.isEditor == false && Application.isPlaying)
        {
            _screenResolution.x = Screen.width;
            _screenResolution.y = Screen.height;
        }

        SetEdgesPosition();        
    }

    private void SetEdgesPosition()
    {
        SetEdgePosition(_leftEdge, LeftBorder);
        SetEdgePosition(_rightEdge, RightBorder);    
    }

    private void SetEdgePosition(GameObject edge, float Border)
    {
        BoxCollider2D boxCollider;

        if (edge.TryGetComponent<BoxCollider2D>(out boxCollider))
            edge.transform.position = new Vector2(Border - boxCollider.size.x / 2, edge.transform.position.y);
    }    

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(LeftBorder, -10, 0), new Vector3(LeftBorder, 10, 0));
        Gizmos.DrawLine(new Vector3(RightBorder, -10, 0), new Vector3(RightBorder, 10, 0));
    }
#endif
}
