using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUIController : MonoBehaviour
{
    [SerializeField] private Button testButton;

    public delegate void FuncDelegate();

    public event FuncDelegate SomeDelegate;

    // public event Action onDeath;

    private void Awake()
    {
        testButton.onClick.AddListener(OnButtonPressed);
    }
    public void OnButtonPressed()
    {
        Debug.Log("버튼이 눌렸다.");
        SomeDelegate.Invoke();
    }
}
