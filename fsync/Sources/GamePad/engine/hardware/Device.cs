namespace fsync
{
	using System;
	using System.Collections.Generic;
	using number = System.Double;
	using fsync.amath;

	public class Device
	{
		public static readonly Device device = new Device().init();
		number pixelRatio
		{
			get
			{
				// return window.devicePixelRatio;
				return 1;
			}
		}

		public Vector3 clientSize;

		public virtual amath.Rect clientRect
		{
			get
			{
				var rect = new amath.Rect();
				rect.width = this.clientSize.x;
				rect.height = this.clientSize.y;
				return rect;
			}
		}

		public List<Action<UserInputData>> userEventHandlers;

		public virtual Device init(amath.Vector3? screenSize = null)
		{
			if (this.userEventHandlers == null)
			{
				this.userEventHandlers = new List<Action<UserInputData>>();
			}
			if (this.clientSize == null)
			{
				this.clientSize = new Vector3();
			}

			// 使用unity api初始化默认值
			// this.clientSize.x = UnityEngine.Screen.width;
			// this.clientSize.y = UnityEngine.Screen.height;
			if (screenSize != null)
			{
				this.clientSize.merge(screenSize);
			}

			return this;
		}
	}


}