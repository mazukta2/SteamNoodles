using UnityEngine;
using System.Collections;
using TMPro;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FlickeringText : MonoBehaviour
{
	[SerializeField] bool _isEnabled;

	[SerializeField] float _minTime;
	[SerializeField] float _maxTime;
    [SerializeField] float _flickeringTime;
    [SerializeField] Color _flickeringColor;

    private TextMeshProUGUI _text;
    private Color _originalColor;
    private float _time;
	private bool _isFlicke;

    // Use this for initialization
    void Awake()
	{
		_text = GetComponent<TextMeshProUGUI>();
        _originalColor = _text.color;
		_time = UnityEngine.Random.Range(_minTime, _maxTime);
	}

	// Update is called once per frame
	void Update()
	{
		if (!_isEnabled)
        {
			SwitchToUnchangedColor();
        }
        else
        {
            _time = Mathf.MoveTowards(_time, 0, Time.deltaTime);

            if (_time <= 0)
            {
                if (_isFlicke)
                {
                    _isFlicke = false;
                    _time = UnityEngine.Random.Range(_minTime, _maxTime);
                    SwitchToUnchangedColor();
                }
                else
                {
                    _isFlicke = true;
                    _time = _flickeringTime;
                    SwitchToChangedColor();
                }
            }
        }

	}

    private void SwitchToUnchangedColor()
    {
        _text.color = _originalColor;
    }

	private void SwitchToChangedColor()
    {
        _text.color = _flickeringColor;
    }
}

