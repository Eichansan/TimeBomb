using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingAnimPlayer : MonoBehaviour
{
    private const float DURATION = 1f;

    void Start()
    {
        Image[] circles = GetComponentsInChildren<Image>();
        for (var i = 0; i < circles.Length; i++)
        {
            circles[i].rectTransform.anchoredPosition = new Vector2((i - circles.Length / 2) * 50f, 0);
            Sequence sequence = DOTween.Sequence()
                .SetLoops(-1, LoopType.Restart)
                .SetDelay((DURATION / 2) * ((float)i / circles.Length))
                .Append(circles[i].rectTransform.DOAnchorPosY(30f, DURATION / 4))
                .Append(circles[i].rectTransform.DOAnchorPosY(0f, DURATION / 4))
                .AppendInterval((DURATION / 2) * ((float)(1 - i) / circles.Length));
            sequence.Play();
        }
    }
}