﻿using System;
namespace HotUI {
	public class Button : View {
		public Button ()
		{

		}
		public Button (string text)
		{
			Text = text;
		}
		public Button (Func<string> formatedText)
		{
			TextBinding = formatedText;
		}
		private string text;
		public string Text {
			get => text;
			set => this.SetValue (State, ref text, value, ViewPropertyChanged);
		}

		public Action OnClick { get; set; }

		public Func<string> TextBinding { get; set; }

		protected override void WillUpdateView ()
		{
			base.WillUpdateView ();
			if (TextBinding != null) {
				State.StartProperty ();
				var text = TextBinding.Invoke ();
				var props = State.EndProperty ();
				var propCount = props.Length;
				if (propCount > 0) {
					State.BindingState.AddViewProperty (props, (s, o) => Text = TextBinding.Invoke ());
				}
				Text = text;
			}
		}

	}
}
