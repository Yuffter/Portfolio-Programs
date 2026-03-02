using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using System;

public class TitleContentModel : MonoBehaviour
{
    readonly ReactiveProperty<int> _currentContentNumber = new ReactiveProperty<int>();
    readonly ReactiveProperty<bool> _isDecided = new ReactiveProperty<bool>(false);
    public IReadOnlyReactiveProperty<int> CurrentContentNumber => _currentContentNumber;
    public IReadOnlyReactiveProperty<bool> IsDecided => _isDecided;
    [SerializeField] int maxContentNumber = 1, minContentNumber = 0;

    public enum ContentType
    {
        START,
        CONTINUE
    }
    public ContentType contentType = ContentType.START;
    bool canControll = false;
    // Start is called before the first frame update
    void Start()
    {
        this.UpdateAsObservable()
            .Where(_ => canControll ==true)
            .Where(_ => Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            .Subscribe(_ =>
            {
                _currentContentNumber.Value = Mathf.Min(_currentContentNumber.Value + 1, maxContentNumber);
                contentType = (ContentType)Enum.ToObject(typeof(ContentType), _currentContentNumber.Value);
            });

        this.UpdateAsObservable()
            .Where(_ => canControll ==true)
            .Where(_ => Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            .Subscribe(_ =>
            {
                _currentContentNumber.Value = Mathf.Max(_currentContentNumber.Value - 1, minContentNumber);
                contentType = (ContentType)Enum.ToObject(typeof(ContentType), _currentContentNumber.Value);
            });

        this.UpdateAsObservable()
            .Where(_ => canControll ==true)
            .Where(_ => Input.GetKeyDown(KeyCode.Return))
            // .First()
            .Subscribe(_ =>
            {
                _isDecided.Value = true;
            });
    }

    // Update is called once per frame
    void Update()
    {
        print(_isDecided.Value);
    }

    public void AbleToControll() {
        canControll = true;
        _isDecided.Value = false;
    }

    public void UnableToControll() {
        canControll = false;
    }

    private void OnDestroy()
    {
        _currentContentNumber.Dispose();
    }
}
