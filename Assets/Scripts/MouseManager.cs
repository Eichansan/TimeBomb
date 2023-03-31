using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour
{
    //画像
    [SerializeField] Image mouseClosedImage;
    [SerializeField] Image mouseOpenedImage;
    //Canvasの変数
    [SerializeField] Canvas canvas;
    //キャンバス内のレクトトランスフォーム
    private RectTransform canvasRect;
    //マウスの位置の最終的な格納先
    private Vector2 mousePos;
    private Vector2 mouseImageSize = new Vector2 (-61,107);

    // Start is called before the first frame update
    void Start()
    {
        canvasRect = canvas.GetComponent<RectTransform>();
    }
    public void SetClosedNipper()
    {
        Cursor.visible = false;
        mouseClosedImage.gameObject.SetActive(true);
        mouseOpenedImage.gameObject.SetActive(false);
    }
    public void SetOpenedNipper()
    {
        Cursor.visible = false;
        mouseClosedImage.gameObject.SetActive(false);
        mouseOpenedImage.gameObject.SetActive(true);
    }
    public void SetCursor()
    {
        Cursor.visible = true;
        mouseClosedImage.gameObject.SetActive(false);
        mouseOpenedImage.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect,
                Input.mousePosition, canvas.worldCamera, out mousePos);

        mouseClosedImage.GetComponent<RectTransform>().anchoredPosition
             = new Vector2(mousePos.x - mouseImageSize.x, mousePos.y - mouseImageSize.y);
        mouseOpenedImage.GetComponent<RectTransform>().anchoredPosition
             = new Vector2(mousePos.x - mouseImageSize.x, mousePos.y - mouseImageSize.y);
    }
}
