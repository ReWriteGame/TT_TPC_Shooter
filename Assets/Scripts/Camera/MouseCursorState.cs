using System;
using UnityEngine;

public class MouseCursorState : MonoBehaviour
{
	[SerializeField] private bool cursorState = false;

	public bool CursorFocus => cursorState;

	public Action<bool> OnChangeCursoreState;

	private void OnApplicationFocus() => ApplyCursorState(cursorState);

	public void Focus() => SetCursorState(true);

	public void Unfocus() => SetCursorState(false);

	private void SetCursorState(bool state)
	{
		cursorState = state;
		ApplyCursorState(state);
		OnChangeCursoreState?.Invoke(state);
	}

	private void ApplyCursorState(bool state)
	{
		Cursor.lockState = state ? CursorLockMode.Locked : CursorLockMode.None;
	}
}
