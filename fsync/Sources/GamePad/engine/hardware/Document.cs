using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WTC.DOM
{
	using number = System.Double;

	public class UIEvent
	{

	}

	public class MouseEvent : UIEvent
	{
		public number clientX;
		public number clientY;
	}
	public class KeyboardEvent : UIEvent
	{
		public string key;
		public int keyCode;
	}

	public class Touch
	{
		public number clientX;
		public number clientY;
		public number force;
		public number identifier;
		// public number pageX;
		// public number pageY;
		// public number radiusX;
		// public number radiusY;
		// public number rotationAngle;
		// public number screenX;
		// public number screenY;
	}

	public class TouchEvent : UIEvent
	{
		public Touch[] changedTouches;
	}

	public class Document
	{
		public static readonly Document document = new Document();

		public Action<KeyboardEvent>? onkeydown = null;
		public Action<KeyboardEvent>? onkeyup = null;
		public Action<MouseEvent>? onmousedown = null;
		public Action<MouseEvent>? onmouseup = null;
		public Action<MouseEvent>? onmousecancel = null;
		public Action<MouseEvent>? onmousemove = null;
		public Action<TouchEvent>? ontouchstart = null;
		public Action<TouchEvent>? ontouchend = null;
		public Action<TouchEvent>? ontouchcancel = null;
		public Action<TouchEvent>? ontouchmove = null;
		public Action<UIEvent>? onresize = null;

		protected Dictionary<KeyCode, bool> pressedKeys = new Dictionary<KeyCode, bool>();

		/// <summary>
		/// 记录左键已经按下
		/// </summary>
		protected bool IsMouse0Pressed = false;
		public virtual void Update()
		{
			#region Mouse
			// 左键
			if (Input.GetMouseButtonDown(0))
			{
				if (IsMouse0Pressed)
				{
					IsMouse0Pressed = false;

					var e = new MouseEvent();
					e.clientX = Input.mousePosition.x;
					e.clientY = Input.mousePosition.y;

					this.onmousecancel(e);
				}

				if (this.onmousedown != null)
				{
					IsMouse0Pressed = true;

					var e = new MouseEvent();
					e.clientX = Input.mousePosition.x;
					e.clientY = Input.mousePosition.y;

					this.onmousedown(e);
				}
			}
			if (Input.GetMouseButtonUp(0))
			{
				if (this.onmouseup != null)
				{
					IsMouse0Pressed = false;

					var e = new MouseEvent();
					e.clientX = Input.mousePosition.x;
					e.clientY = Input.mousePosition.y;

					this.onmouseup(e);
				}
			}

			if (Input.GetMouseButton(0))
			{
				if (this.onmousemove != null)
				{
					var e = new MouseEvent();
					e.clientX = Input.mousePosition.x;
					e.clientY = Input.mousePosition.y;

					this.onmousemove(e);
				}
			}
			else
			{
				if (IsMouse0Pressed)
				{
					IsMouse0Pressed = false;

					var e = new MouseEvent();
					e.clientX = Input.mousePosition.x;
					e.clientY = Input.mousePosition.y;

					this.onmousecancel(e);
				}
			}
			#endregion

			#region Keyboard
			if (Input.anyKeyDown)
			{
				var inputString = Input.inputString;
				foreach (var key in inputString)
				{
					var keyCode = (KeyCode)key;

					if (Input.GetKeyDown(keyCode))
					{
						if (this.pressedKeys.ContainsKey(keyCode))
						{
							this.pressedKeys.Remove(keyCode);

							var e1 = new KeyboardEvent();
							e1.key = Enum.GetName(typeof(UnityEngine.KeyCode), keyCode);
							e1.keyCode = (int)keyCode;

							this.onkeyup(e1);
						}

						var e = new KeyboardEvent();
						e.key = Enum.GetName(typeof(UnityEngine.KeyCode), keyCode);
						e.keyCode = (int)keyCode;

						this.pressedKeys[keyCode] = true;

						this.onkeydown(e);
					}
					//Debug.LogWarning("input string: " + inputString);
				}
			}

			foreach (var keyCode in pressedKeys.Keys.ToArray())
			{
				if (Input.GetKeyUp(keyCode))
				{
					this.pressedKeys.Remove(keyCode);

					var e = new KeyboardEvent();
					e.key = Enum.GetName(typeof(UnityEngine.KeyCode), keyCode);
					e.keyCode = (int)keyCode;

					this.onkeyup(e);
				}
			}
			#endregion

			#region Touch
			if (Input.touchCount > 0)
			{
				Func<UnityEngine.Touch, Touch> convTouch = (UnityEngine.Touch t) =>
				{
					var touch = new Touch();
					touch.identifier = t.fingerId;
					touch.clientX = t.position.x;
					touch.clientY = t.position.y;
					touch.force = t.pressure;
					return touch;
				};

				{
					Touch[] beginTouches = Input.touches
						.Where(t => t.phase == UnityEngine.TouchPhase.Began)
						.Select(convTouch).ToArray();
					var touchInfo = new TouchEvent();
					touchInfo.changedTouches = beginTouches;
					this.ontouchstart(touchInfo);
				}
				{
					Touch[] beginTouches = Input.touches
						.Where(t => t.phase == UnityEngine.TouchPhase.Moved)
						.Select(convTouch).ToArray();
					var touchInfo = new TouchEvent();
					touchInfo.changedTouches = beginTouches;
					this.ontouchmove(touchInfo);
				}
				{
					Touch[] beginTouches = Input.touches
						.Where(t => t.phase == UnityEngine.TouchPhase.Ended)
						.Select(convTouch).ToArray();
					var touchInfo = new TouchEvent();
					touchInfo.changedTouches = beginTouches;
					this.ontouchend(touchInfo);
				}
				{
					Touch[] beginTouches = Input.touches
						.Where(t => t.phase == UnityEngine.TouchPhase.Canceled)
						.Select(convTouch).ToArray();
					var touchInfo = new TouchEvent();
					touchInfo.changedTouches = beginTouches;
					this.ontouchcancel(touchInfo);
				}
			}
			#endregion
		}
	}

}
