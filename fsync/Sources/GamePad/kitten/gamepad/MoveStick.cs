namespace kitten.gamepad
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using fsync.amath;
	using UserInput = fsync.UserInput;

	/**
	 * 移动摇杆
	 */
	public class MoveStick : GameStick
	{

		public override bool handlerInput(fsync.UserInputData data)
		{
			if (base.handlerInput(data))
			{
				// pass
			}
			else if (this.detectKeyboardMoveInput(data))
			{
				// pass
			}
			else
			{
				return false;
			}
			return true;
		}

		/**
		* 获取输入端口列表
		*/
		public override List<string> getInputPorts()
		{
			var keys = base.getInputPorts();
			if (this.isKeyPressing)
			{
				keys.Add("keyboard1");
			}
			return keys;
		}

		protected Dictionary<string, bool> pressingKeys = new Dictionary<string, bool>();
		protected bool isKeyPressing = false;

		protected virtual void updateKeyboardInputStatus()
		{
			var k = (new string[] { "a", "d", "w", "s" }).Where((kx) =>
			{
				if (this.pressingKeys.ContainsKey(kx))
				{
					return this.pressingKeys[kx];
				}
				return false;
			}).ToArray();
			if (k.Count() <= 0)
			{
				this.ctrlStatusRaw.pressed = false;
				this.ctrlStatusRaw.isStrengthInvalid = true;
				this.ctrlStatusRaw.strength = 0;
			}
			else
			{
				this.ctrlStatusRaw.pressed = true;
				this.ctrlStatusRaw.isStrengthInvalid = true;
				this.ctrlStatusRaw.strength = 1;
			}
			this.isKeyPressing = this.ctrlStatusRaw.pressed;

			isKeyboardMoveInput = this.ctrlStatusRaw.pressed;

			//this.updateStatus();
		}

		protected bool isKeyboardMoveInput = false;

		protected bool hasKeyDown(string key)
        {
			bool v;
			if(pressingKeys.TryGetValue(key,out v))
            {
				return v;
			}
			return false;
		}

		/// <summary>
		/// 模拟键盘附带的初始偏移信息
		/// </summary>
        protected override void updateTouchPoint()
        {
			base.updateTouchPoint();
            if (isKeyboardMoveInput)
			{
				Vector.addUp(Vector.merge(this.ctrlStatusRaw.touchPoint, this.ctrlStatusRaw.ctrlPos), this.ctrlStatusRaw.dir);
			}
		}

		protected virtual void injectKeyboardOffset()
        {
			this.ctrlStatusRaw.dir.x = 0;
			this.ctrlStatusRaw.dir.y = 0;
			//this.ctrlStatusRaw.dir.z = 0;
			if (hasKeyDown("a"))
			{
				this.ctrlStatusRaw.dir.x -= 1;
			}
			if (hasKeyDown("d"))
			{
				this.ctrlStatusRaw.dir.x += 1;
			}
			if (hasKeyDown("w"))
			{
				this.ctrlStatusRaw.dir.y += 1;
			}
			if (hasKeyDown("s"))
			{
				this.ctrlStatusRaw.dir.y -= 1;
			}
		}

		/**
		 * 检测键盘输入控制
		 * @param data 
		 */
		protected virtual bool detectKeyboardMoveInput(fsync.UserInputData data)
		{
			if (isKeyboardMoveInput==false && data.action != "onkeydown")
            {
				return false;
            }

			if (data.action == "onkeydown")
			{
				var key = data.event1.key;

				this.pressingKeys[key] = true;
				this.updateKeyboardInputStatus();
				this.injectKeyboardOffset();
				//this.updateKeyboardInputStatus();
				//this.updateStatus();

			}
			else if (data.action == "onkeyup")
			{
				var key = data.event1.key;

				this.pressingKeys[key] = false;
				this.updateKeyboardInputStatus();
				this.injectKeyboardOffset();
				//this.updateStatus();

			}
			else
			{
				return false;
			}
			return true;
		}

	}
}