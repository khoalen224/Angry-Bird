using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ChangeInput : MonoBehaviour
{
    EventSystem system;
    public Selectable firstSelectedObject;
    public Button sumbitButton;
    private void Start()
    {
        system = EventSystem.current;
        firstSelectedObject.Select();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            Selectable pervious = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (pervious != null)
            {
                pervious.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            sumbitButton.onClick.Invoke();
            Debug.Log("Button clicked!");
        }
    }
}
