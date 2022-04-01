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
	public class MoveStick : MouseStick
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

		protected void updateKeyboardInputStatus()
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
				this.ctrlStatusRaw.strength = 0;
				this.ctrlStatusRaw.isStrengthInvalid = true;
			}
			else
			{
				this.ctrlStatusRaw.pressed = true;
				this.ctrlStatusRaw.strength = 1;
				this.ctrlStatusRaw.isStrengthInvalid = true;
			}
			this.isKeyPressing = this.ctrlStatusRaw.pressed;
		}

		/**
		 * 检测键盘输入控制
		 * @param data 
		 */
		protected bool detectKeyboardMoveInput(fsync.UserInputData data)
		{
			if (data.action == "onkeydown")
			{
				var key = data.event1.key;

				this.pressingKeys[key] = true;
				this.updateKeyboardInputStatus();
				this.updateStatus();

				if (key == "a")
				{
					this.ctrlStatusRaw.dir.x = -1;
					//this.ctrlStatusRaw.dir.y = 0;
				}
				else if (key == "d")
				{
					this.ctrlStatusRaw.dir.x = 1;
					//this.ctrlStatusRaw.dir.y = 0;
				}
				else if (key == "w")
				{
					//this.ctrlStatusRaw.dir.x = 0;
					this.ctrlStatusRaw.dir.y = 1;
				}
				else if (key == "s")
				{
					//this.ctrlStatusRaw.dir.x = 0;
					this.ctrlStatusRaw.dir.y = -1;
				}
				Vector.addUp(Vector.merge(this.ctrlStatusRaw.touchPoint, this.ctrlStatusRaw.dir), this.ctrlStatusRaw.ctrlPos);

				this.updateKeyboardInputStatus();

			}
			else if (data.action == "onkeyup")
			{
				var key = data.event1.key;

				this.pressingKeys[key] = false;

				this.updateKeyboardInputStatus();

			}
			else
			{
				return false;
			}
			return true;
		}

	}
}