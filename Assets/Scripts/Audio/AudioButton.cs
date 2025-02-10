using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
	[SerializeField] private AudioType _audioType;

	private void Awake()
	{
		if (gameObject.TryGetComponent(out Button button))
		{
			button.onClick.AddListener(Play);

		}
		else if (gameObject.TryGetComponent(out EventTrigger eventTrigger))
		{
			var entry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
			entry.callback.AddListener(data => { OnPointerDownDelegate((PointerEventData)data); });
			eventTrigger.triggers.Add(entry);

		}
		else
		{
			button = gameObject.AddComponent<Button>();
			button.onClick.AddListener(Play);
		}
	}

	public void Play()
	{
		AudioController.Play(_audioType, false);
	}

	public void OnPointerDownDelegate(PointerEventData _)
	{
		AudioController.Play(_audioType, false);
	}
}