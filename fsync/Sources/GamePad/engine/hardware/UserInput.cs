
using System;
using System.Collections.Generic;
using fsync.amath;
using lang;

namespace fsync
{
	using number = System.Double;
	using UserInputHandler = Action<UserInputData>;

	public class Size
	{
		public number width;
		public number height;
	}

	public class UserInputData
	{
		public class InputTouchInfo
		{
			public string identifier;
			public number clientX;
			public number clientY;
		}
		public class InputEvent
		{
			public number clientX;
			public number clientY;
			public number? keyCode;
			public string key;
			public List<InputTouchInfo> touches;
		}
		public string action;// "onsetup" | "updateclientsize" | "onkeydown" | "onkeyup" | "onmousemove" | "onmousedown" | "onmouseup" | "ontouchmove" | "ontouchstart" | "ontouchend"|"ontouchcancel",
		public InputEvent event1;
		public Size clientSize;
	}

	public class UserInput
	{
		static Dictionary<number, string> keyCodeMap = new Dictionary<number, string>()
		{
			[65] = "a",
			[87] = "w",
			[83] = "s",
			[68] = "d",
			[97] = "a",
			[119] = "w",
			[115] = "s",
			[100] = "d",
			[32] = "space",
		};
		public static readonly UserInput inst = new UserInput().init();

		protected Dictionary<string, UserInputHandler> eventHandlerMap;

		protected Action<UserInputData> eventHandler;

		public bool enable = true;

		public virtual Vector3 clientSize
		{
			get
			{
				return Device.device.clientSize;
			}
		}
		public virtual UserInput init()
		{
			this.eventHandlerMap = new Dictionary<string, UserInputHandler>();



			this.eventHandler = (UserInputData data) =>
			{

				if (!this.enable)
				{
					return;
				}
				if (data.action == "updateclientsize")
				{
					this.clientSize.x = data.clientSize.width;
					this.clientSize.y = data.clientSize.height;
				}

				if (data.event1.keyCode != null)
				{
					keyCodeMap.TryGetValue(data.event1.keyCode.Value, out data.event1.key);
					//data.event1.key = keyCodeMap[data.event1.keyCode.Value];
				}
				//data.event1.key = keyCodeMap[data.event1.keyCode.Value];
				foreach (var handler in this.eventHandlerMap.Values)
				{
					try
					{
						handler(data);
					}
					catch (Exception e)
					{
						console.error(e);
					}
				}
			};
			Device.device.userEventHandlers.Add(this.eventHandler);
			return this;
		}

		public virtual void addHandler(string name, UserInputHandler handler)
		{
			this.eventHandlerMap[name] = handler;
		}
	}

}