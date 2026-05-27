using UnityEngine;
using DG.Tweening;
using TMPro;

public class TweenSample : MonoBehaviour
{
    [Header("효과를 위한 UI/Object 타겟")]
    public RectTransform UITarget;                            //UI 타겟
    public GameObject ObjectTarget;                           //오브젝트 타겟

    [Header("글자 연출 타겟")]
    public TMP_Text countText;
    public int currentValue = 0;
    public int addValue = 100;

    private int targetValue;

    [Header("색 변형 연출 예시")]
    public Color flachColor = Color.yellow;

    private Color originalColor;

    [Header("페이드 UI 그룹")]
    public CanvasGroup fadeTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        countText.text = currentValue.ToString();

        originalColor = countText.color;

        fadeTarget.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayPunchUIScale();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayPunchObjectScale();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayUIShake();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayCountUp();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayColorFlash();
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            PlayFade();
        }
    }

    public void PlayPunchUIScale()
    {
        if (UITarget == null) return;              //없으면 리턴
        UITarget.DOKill();                         //이전 실행 중이던 Tween 효과가 있으면 정리한다.
        UITarget.localScale = Vector3.one;         //크기가 이상하게 남아 있을 수 있으므로 기본 크기로 초기화
        UITarget.DOPunchScale(Vector3.one * 0.3f, 0.25f, 8, 1.0f);     //방향, 크기, 시간, 진동 횟수, 탄성
    }

    public void PlayPunchObjectScale()
    {
        if (ObjectTarget ==  null) return;          //없으면 리턴
        ObjectTarget.transform.DOKill();            //이전 실행 중이던 Tween 효과가 있으면 정리한다.
        ObjectTarget.transform.localScale = Vector3.one;      //크기가 이상하게 남아 있을 수 있으므로 기본 크기로 초기화
        ObjectTarget.transform.DOPunchScale(Vector3.one * 0.3f, 0.25f, 8, 1.0f);    //방향, 크기, 시간, 진동 횟수, 탄성
    }

    public void PlayUIShake()
    {
        if (UITarget == null) return;             //없으면 리턴
        UITarget.DOKill();
        UITarget.DOShakeAnchorPos(0.3f, 20f, 20, 90f);  //시간, 강도, 진동 횟수, 랜덤성
    }

    public void PlayCountUp()
    {
        if (countText == null) return;

        targetValue += addValue;                 //목표 숫자
        DOTween.Kill("CountTween", true);        //기존 숫자 Tween 이 남아 있으면 완료 된 후 종료 처리

        DOTween.To(
            () => currentValue,                    //현재 값
            value =>                               //중간 값이 바뀔때 실행 되는 부분
            {
                currentValue = value;
                countText.text = currentValue.ToString();
            },
            targetValue,                                  //목표값
            0.5f                                          //걸리는 시간
            
        )   
        .SetEase(Ease.OutQuad)
        .SetId("CountTween");
    }

    public void PlayColorFlash()
    {
        if(countText == null) return;
        countText.DOKill();               //이전 Tween 정리
        countText.color = originalColor;           //이전 Tween 중간 색상이 남았을 수 있으므로 원래 색으로 초기화

        countText.DOColor(flachColor, 0.1f)
        .OnComplete(() =>
        {
            countText.DOColor(originalColor, 0.2f);            //완료 되면 원래 색으로 되돌린다.
        });

    }

    public void PlayFade()
    {
        if(fadeTarget == null) return;
        fadeTarget.DOKill();                //이전 연출 정리
        fadeTarget.alpha = 0;               //처음에는 안보이게 설정

        Sequence seq = DOTween.Sequence();              //여러 Tween 을 순서대로 실행 할 때 사용 한다.

        seq.Append(fadeTarget.DOFade(1f, 0.2f));        //0.2초 동안 나타단다.
        seq.AppendInterval(0.5f);                       //0.5초 동안 유지
        seq.Append(fadeTarget.DOFade(0f, 0.3f));        //0.3초 동안 사라진다. 
    }
}
