using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageFadeTween : MonoBehaviour
{

	[SerializeField, Min(0f)] private float _startDelay;
	[SerializeField, Range(0f, 1f)] private float _startAlpha = 0f;
	[SerializeField, Range(0f, 1f)] private float _targetAlpha = 0.984f;
	[SerializeField, Min(0.01f)] private float _transitionTime = 0.2f;

	private Image _image;
	private Sequence _tweenSequence;

	private void Awake()
	{
		if (!TryGetComponent(out _image))
			_image = gameObject.AddComponent<Image>();

		_tweenSequence = DOTween.Sequence();

		if (_startDelay > 0)
			_tweenSequence.AppendInterval(_startDelay);

		_tweenSequence.Append(_image.DOFade(_targetAlpha, _transitionTime));
		_tweenSequence.Pause();
		_tweenSequence.SetAutoKill(false);
	}

	private void OnEnable()
	{
		_image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _startAlpha);
		_tweenSequence.Restart();
	}
}