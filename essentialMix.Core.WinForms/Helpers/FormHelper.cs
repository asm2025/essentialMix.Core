﻿using System;
using System.Windows.Forms;
using JetBrains.Annotations;

namespace essentialMix.Core.WinForms.Helpers;

public static class FormHelper
{
	public static bool Enable = true;

	public static void AddHotKey([NotNull] Form thisValue, [NotNull] Action function, Keys key, bool ctrl = false, bool shift = false, bool alt = false)
	{
		thisValue.KeyPreview = true;

		thisValue.KeyDown += (_, e) =>
		{
			if (!IsHotKey(e, key, ctrl, shift, alt)) return;
			function();
		};
	}

	public static bool IsHotKey([NotNull] KeyEventArgs eventData, Keys key, bool ctrl = false, bool shift = false, bool alt = false)
	{
		return eventData.KeyCode == key && eventData.Control == ctrl && eventData.Shift == shift && eventData.Alt == alt;
	}
}