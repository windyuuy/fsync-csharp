
/**
 * 手势分析
 */
namespace kitten.uievent
{
	using System;
	using System.Collections.Generic;

	using number = System.Double;
	using UserInputData = fsync.UserInputData;
	using fsync.amath;
	using WTC.DOM;

	public class UIEventHandler
	{
		public static readonly UIEventHandler uiEventHandler = new UIEventHandler();

		protected virtual void handleEvent(UserInputData data)
		{
			// data.event1.clientX=this

			foreach (var handler in fsync.Device.device.userEventHandlers.ToArray())
			{
				handler(data);
			}
		}

		protected Func<number, number> convertDesignX;
		protected Func<number, number> convertDesignY;

		protected bool isEnabled = false;
		public virtual void enableUIEvent()
		{
			if (this.isEnabled)
			{
				return;
			}
			this.isEnabled = true;


			var clientSize = fsync.Device.device.clientSize;

			Func<number, number>? convertDesignX = null;
			Func<number, number>? convertDesignY = null;

			Action _initGraph = () =>
			{
				// const screenWidth = document.body.clientWidth
				// const screenHeight = document.body.clientHeight
				var designWidth = clientSize.x;
				var designHeight = clientSize.y;

				// var screenWidth = UnityEngine.Screen.width;
				// var screenHeight = UnityEngine.Screen.height;
				var screenWidth = clientSize.x;
				var screenHeight = clientSize.y;
				// console.log(screenWidth, screenHeight)
				var scaleX = screenWidth / designWidth;
				var scaleY = screenHeight / designHeight;
				var scaleMin = Math.Min(scaleX, scaleY);
				var width = designWidth * scaleMin;
				var height = designHeight * scaleMin;

				// console.log("screenSize:", screenWidth, screenHeight)
				// console.log("deviceSize:", width, height)

				convertDesignX = (number x) =>
				{
					return (x - (screenWidth - width) / 2) / scaleMin;
				};
				convertDesignY = (number y) =>
				{
					return (y - (screenHeight - height) / 2) / scaleMin;
				};

			};
			_initGraph();

			this.convertDesignX = convertDesignX;
			this.convertDesignY = convertDesignY;


			Action<UserInputData> handleEvent = (UserInputData data) =>
			{
				data.event1.clientX = this.convertDesignX(data.event1.clientX);
				data.event1.clientY = this.convertDesignY(data.event1.clientY);
				if (data.event1.touches != null)
				{
					foreach (var t in data.event1.touches)
					{
						t.clientX = this.convertDesignX(t.clientX);
						t.clientY = this.convertDesignY(t.clientY);
					}
				}
				// UnityEngine.Debug.Log(data);

				this.handleEvent(data);
			};

			var document = WTC.DOM.Document.document;

			document.onkeydown = (KeyboardEvent ev) =>
			{
				var data = new UserInputData()
				{
					action = "onkeydown",
					clientSize = new fsync.Size()
					{
						width = clientSize.x,
						height = clientSize.y,
					},
					event1 = new UserInputData.InputEvent()
					{
						keyCode = ev.keyCode,
						key = ev.key,
					},
				};
				handleEvent(data);
			};

			document.onkeyup = (KeyboardEvent ev) =>
				{
					var data = new UserInputData()
					{
						action = "onkeyup",
						clientSize = new fsync.Size()
						{
							width = clientSize.x,
							height = clientSize.y,
						},
						event1 = new UserInputData.InputEvent()
						{
							keyCode = ev.keyCode,
							key = ev.key,
						},
					};
					handleEvent(data);
				};

			document.onmousedown = (MouseEvent ev) =>
			{
				var data = new UserInputData()
				{
					action = "onmousedown",
					clientSize = new fsync.Size()
					{
						width = clientSize.x,
						height = clientSize.y,
					},
					event1 = new UserInputData.InputEvent()
					{
						clientX = ev.clientX,
						clientY = ev.clientY,
					},
				};
				handleEvent(data);
			};

			document.onmouseup = (MouseEvent ev) =>
			{
				var data = new UserInputData()
				{
					action = "onmouseup",
					clientSize = new fsync.Size()
					{
						width = clientSize.x,
						height = clientSize.y,
					},
					event1 = new UserInputData.InputEvent()
					{
						clientX = ev.clientX,
						clientY = ev.clientY,
					}
				};
				handleEvent(data);
			};
			document.onmousecancel = document.onmouseup;

			document.onmousemove = (MouseEvent ev) =>
			{
				var data = new UserInputData()
				{
					action = "onmousemove",
					clientSize = new fsync.Size()
					{
						width = clientSize.x,
						height = clientSize.y,
					},
					event1 = new UserInputData.InputEvent()
					{
						clientX = ev.clientX,
						clientY = ev.clientY,
					}
				};
				handleEvent(data);
			};

			document.ontouchstart = (TouchEvent ev) =>
			{
				var data = new UserInputData()
				{
					action = "ontouchstart",
					clientSize = new fsync.Size()
					{
						width = clientSize.x,
						height = clientSize.y,
					},
					event1 = new UserInputData.InputEvent()
					{
						touches = new List<UserInputData.InputTouchInfo>(),
					},
				};
				for (var i = 0; i < ev.changedTouches.Length; i++)
				{
					var t = ev.changedTouches[i];
					data.event1.touches.Add(new UserInputData.InputTouchInfo()
					{
						identifier = $"{t.identifier}",
						clientX = t.clientX,
						clientY = t.clientY,
					});
				}
				handleEvent(data);
			};

			document.ontouchend = (TouchEvent ev) =>
			{
				var data = new UserInputData()
				{
					action = "ontouchend",
					clientSize = new fsync.Size()
					{
						width = clientSize.x,
						height = clientSize.y,
					},
					event1 = new UserInputData.InputEvent()
					{
						touches = new List<UserInputData.InputTouchInfo>(),
					},
				};
				for (var i = 0; i < ev.changedTouches.Length; i++)
				{
					var t = ev.changedTouches[i];
					data.event1.touches.Add(new UserInputData.InputTouchInfo()
					{
						identifier = $"{t.identifier}",
						clientX = t.clientX,
						clientY = t.clientY,
					});
				}
				handleEvent(data);
			};

			document.ontouchcancel = (TouchEvent ev) =>
			{
				var data = new UserInputData()
				{
					action = "ontouchcancel",
					clientSize = new fsync.Size()
					{
						width = clientSize.x,
						height = clientSize.y,
					},
					event1 = new UserInputData.InputEvent()
					{
						touches = new List<UserInputData.InputTouchInfo>(),
					},
				};
				for (var i = 0; i < ev.changedTouches.Length; i++)
				{
					var t = ev.changedTouches[i];
					data.event1.touches.Add(new UserInputData.InputTouchInfo()
					{
						identifier = $"{t.identifier}",
						clientX = t.clientX,
						clientY = t.clientY,
					});
				}
				handleEvent(data);
			};

			document.ontouchmove = (TouchEvent ev) =>
			{
				var data = new UserInputData()
				{
					action = "ontouchmove",
					clientSize = new fsync.Size()
					{
						width = clientSize.x,
						height = clientSize.y,
					},
					event1 = new UserInputData.InputEvent()
					{
						touches = new List<UserInputData.InputTouchInfo>(),
					},
				};
				for (var i = 0; i < ev.changedTouches.Length; i++)
				{
					var t = ev.changedTouches[i];
					data.event1.touches.Add(new UserInputData.InputTouchInfo()
					{
						identifier = $"{t.identifier}",
						clientX = t.clientX,
						clientY = t.clientY,
					});
				}
				handleEvent(data);
			};

			document.onresize = (UIEvent ev) =>
			{
				var data = new UserInputData()
				{
					action = "u=dateclientsize",
					clientSize = new fsync.Size()
					{
						width = clientSize.x,
						height = clientSize.y,
					},
					event1 = {
					}
				};
				handleEvent(data);
			};

		}



		public virtual void postInitEvent(Vector3? size = null)
		{
			var clientSize = fsync.Device.device.clientSize;
			if (size != null)
			{
				clientSize.x = size.x;
				clientSize.y = size.y;
			}

			Action<UserInputData> handleEvent = (UserInputData data) =>
			{
				this.handleEvent(data);
			};

			{
				var data = new UserInputData()
				{
					action = "updateclientsize",
					clientSize = new fsync.Size()
					{
						width = clientSize.x,
						height = clientSize.y,
					},
					event1 = new UserInputData.InputEvent()
					{
					},
				};
				handleEvent(data);
			}
			{
				var data = new UserInputData()
				{
					action = "onsetup",
					clientSize = new fsync.Size()
					{
						width = clientSize.x,
						height = clientSize.y,
					},
					event1 = new UserInputData.InputEvent()
					{
					},
				};
				handleEvent(data);
			}
		}
	}
}
