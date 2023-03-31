using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DecideButton : MonoBehaviour
{
    // [SerializeField] MouseManager mouseManager;
    private void Start() 
    {
        transform.DOScale(0.1f, 1f)
            .SetRelative(true)
            .SetEase(Ease.OutQuart)
            .SetLoops(-1, LoopType.Restart);
    }
    // public void PointerEnterButton()
    // {
    //     mouseManager.SetOpenedNipper();
    // }
    // public void PointerExitButton()
    // {
    //     mouseManager.SetClosedNipper();
    // }
}
