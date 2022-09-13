// Libraries to use the UI and Event Systems
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
/*
public class Joystick : MonoBehaviour, IDragHandler //OnDrag//, IPointerUpHandler //On Pointer Up//, IPointerDownHandler //On Pointer Down//
{
    // Variables to control the images of the canvas UI
    private Image jsOuter;
    private Image joystick;

    public Vector3 InputDirection;

    // Start is called before the first frame update
    void Start()
    {
        // Access to the Image Component of the joystick Outer
        jsOuter = GetComponent<Image>();
        // Transformation of the child of the Joystick (Handler) and access to this component Image
        joystick = transform.GetChild(0).GetComponent<Image>();
        // Assignation to the input direction movement
        InputDirection = Vector3.zero;
    }

    // Method On Drag Event 
    public void OnDrag(PointerEventData ped)
    {
        // Declaring the positon on the Vector2
        Vector2 position = Vector2.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (jsOuter.rectTransform, ped.position, ped.pressEventCamera, out position);

        position.x = (position.x / jsOuter.rectTransform.sizeDelta.x);
        position.y = (position.y / jsOuter.rectTransform.sizeDelta.y);

        float x = (jsOuter.rectTransform.pivot.x == 1f) ? position.x *2 + 1 : position.x * 2 - 1;
        float y = (jsOuter.rectTransform.pivot.y == 1f) ? position.y * 2 + 1 : position.y * 2 - 1;

        InputDirection = new Vector3(position.x * 2 + 0, position.y * 2);
        InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

        joystick.rectTransform.anchoredPosition = new Vector3(InputDirection.x * (jsOuter.rectTransform.sizeDelta.x / 3), InputDirection.y * (jsOuter.rectTransform.sizeDelta.y / 3));

    }

    public void OnPointerDown (PointerEventData ped)
    {
        // Call to the OnDrag Method with the ped
        OnDrag(ped);
    }

    public void OnPointerUp (PointerEventData ped)
    {
        // Input on the Vector3
        InputDirection = Vector3.zero;
        joystick.rectTransform.anchoredPosition = Vector3.zero;
    }
}
*/