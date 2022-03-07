using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputFieldRightClickMenu : MonoBehaviour, IPointerClickHandler
{
    public async void OnPointerClick( PointerEventData eventData )
    {
        if ( eventData.button != PointerEventData.InputButton.Right )
        {
            return;
        }
        dynamic inputFieldComponent = null;
        var inputField = GetComponent<InputField>( );
        if ( inputField )
        {
            inputFieldComponent = inputField;
        }
        var tMP_InputField = GetComponent<TMP_InputField>( );
        if ( tMP_InputField )
        {
            inputFieldComponent = tMP_InputField;
        }
        if ( !inputFieldComponent )
        {
            throw new System.NotSupportedException( );
        }
        var obj = await Addressables.InstantiateAsync( "PanelInputFieldRightClickMenu" , GetComponentInParent<Canvas>( ).transform ).Task;
        obj.transform.position = transform.position;
        obj.GetComponent<PanelInputFieldRightClickMenu>( ).Init( inputFieldComponent );
    }
}