using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelInputFieldRightClickMenu : MonoBehaviour
{
    public Button btnDestroy;
    public Button btnCut;
    public Button btnCopy;
    public Button btnPaste;
    public Button btnDelete;
    public Button btnSelectAll;

    private dynamic inputFieldComponent;
    private int startIndex;
    private int length;

    public void Init( dynamic inputFieldComponent )
    {
        this.inputFieldComponent = inputFieldComponent;
        startIndex = Mathf.Min( inputFieldComponent.selectionAnchorPosition , inputFieldComponent.selectionFocusPosition );
        length = Mathf.Max( inputFieldComponent.selectionAnchorPosition , inputFieldComponent.selectionFocusPosition ) - startIndex;
        SetEnabled( btnCut , !string.IsNullOrEmpty( inputFieldComponent.text.Substring( startIndex , length ) ) );
        SetEnabled( btnCopy , !string.IsNullOrEmpty( inputFieldComponent.text.Substring( startIndex , length ) ) );
        SetEnabled( btnPaste , !string.IsNullOrEmpty( UniClipboard.GetText( ) ) );
        SetEnabled( btnDelete , !string.IsNullOrEmpty( inputFieldComponent.text.Substring( startIndex , length ) ) );
    }

    public void SetEnabled( Button button , bool isEnabled )
    {
        for ( int i = 0 ; i < button.transform.childCount ; i++ )
        {
            button.transform.GetChild( i ).GetComponent<Graphic>( ).color = isEnabled ? Color.black : Color.gray;
        }
        button.interactable = isEnabled;
    }

    public void OnClickBtnSelectAll( )
    {
        inputFieldComponent.Select( );
        Destroy( gameObject );
    }

    public void OnClickBtnDelete( )
    {
        inputFieldComponent.text = inputFieldComponent.text.Remove( startIndex , length );
        Destroy( gameObject );
    }

    public void OnClickBtnPaste( )
    {
        string text = inputFieldComponent.text.Remove( startIndex , length );
        inputFieldComponent.text = text.Insert( startIndex , UniClipboard.GetText( ) );
        Destroy( gameObject );
    }

    public void OnClickBtnCopy( )
    {
        UniClipboard.SetText( inputFieldComponent.text.Substring( startIndex , length ) );
        Destroy( gameObject );
    }

    public void OnClickBtnCut( )
    {
        UniClipboard.SetText( inputFieldComponent.text.Substring( startIndex , length ) );
        inputFieldComponent.text = inputFieldComponent.text.Remove( startIndex , length );
        Destroy( gameObject );
    }

    public void OnClickBtnDestroy( )
    {
        Destroy( gameObject );
    }
}