using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class Title : MonoBehaviour
{
    [SerializeField] private UIDocument _document;

    private VisualElement _root;
    private VisualElement _version;
    private VisualElement _userId;
    private VisualElement _productLogo;
    private Button _tapToStart;
    private VisualElement _copyright;
    private VisualElement _rootContainer;

    private bool _isTapToStart = false;
    private bool _isInitialized = false;

    void OnEnable()
    {
        ReInitialize();
        Show();
    }

    public void ReInitialize()
    {
        InitializeIfNeed();
        MyTween.HideX(_userId, true, true);
        MyTween.HideX(_version, false, true);
        MyTween.HideAlpha(_productLogo, true);
        MyTween.HideAlpha(_tapToStart, true);
        MyTween.HideX(_copyright, false, true);
        _tapToStart.clicked += () => Hide();
    }

    public async Task Show()
    {
        await Task.Delay(1000);
        MyTween.ShowX(_userId);
        MyTween.ShowX(_version);
        MyTween.ShowX(_copyright);

        await Task.Delay(500);

        MyTween.ShowAlpha(_productLogo, false, 1.2f, Ease.OutSine);

        await Task.Delay(1000);

        MyTween.ShowAlpha(_tapToStart, false, 1f);
    }

    public async Task Hide()
    {
        MyTween.HideAlpha(_rootContainer, false, 0.8f);
        await Task.Delay(8000);
        gameObject.SetActive(false);
    }


    private void InitializeIfNeed()
    {
        //if (_isInitialized) return;
        _isInitialized = true;
        _root = _document.rootVisualElement;
        _rootContainer = _root.Q<VisualElement>("RootContainer");
        _version = _root.Q<VisualElement>("Version");
        _userId = _root.Q<VisualElement>("UserId");
        _productLogo = _root.Q<VisualElement>("ProductLogo");
        _tapToStart = _root.Q<Button>("BtnTapToStart");
        _copyright = _root.Q<VisualElement>("Copyright");
    }
}