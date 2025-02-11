using UnityEngine;
using DG.Tweening;

public class TransformScaleTween : MonoBehaviour
{

	[SerializeField] private float _playDelay = 0.25f;
	[SerializeField] private float _startScale = 0f;
	[SerializeField] private float _pulseScale = 1.1f;
	[SerializeField] private float _targetScale = 1f;
	[SerializeField] private float _pulseTime = 0.25f;
	[SerializeField] private float _transitionTotalTime = 0.35f;

	private Sequence _tweenSequence;

	private void Awake()
	{
		_tweenSequence = DOTween.Sequence();

		if (_playDelay > 0)
			_tweenSequence.AppendInterval(_playDelay);

		_tweenSequence.Append(transform.DOScale(_pulseScale, _pulseTime));
		_tweenSequence.Append(transform.DOScale(_targetScale, _transitionTotalTime - _pulseTime));
		_tweenSequence.Pause();
		_tweenSequence.SetAutoKill(false);
	}

	private void OnEnable()
	{
		transform.localScale = Vector3.zero;
		_tweenSequence.Restart();
	}
}