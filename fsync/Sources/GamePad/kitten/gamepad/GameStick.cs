
namespace kitten.gamepad
{

	/**
	 * 自动重定位的摇杆
	 */
	public class GameStick : CircleStick
	{

		/**
		 * 玩家放开触摸摇杆时,摇杆中心点和当前触摸点复位
		 */
		protected bool needResetAfterLoose;

		public override CircleStick init(string id, StickSharedState sharedState)
		{
			this.needResetAfterLoose = false;
			base.init(id, sharedState);
			return this;
		}

		public override void updateStatus()
		{
			this.updateTouchAction();

			/**
			* 游戏摇杆的特性:
			* - 当玩家第一次触摸摇杆时,摇杆的触摸起点要设置为当前触摸点
			* - 玩家放开触摸摇杆时,摇杆中心点和当前触摸点复位
			*/
			if (this.ctrlStatusRaw.touchAction == "begin")
			{
				this.needResetAfterLoose = false;
				this.setStartPos(this.ctrlStatusRaw.touchPoint);
				this.calcTouchVector();
			}
			else if (this.ctrlStatusRaw.touchAction == "end")
			{
				this.needResetAfterLoose = true;
				this.calcTouchVector();
			}
			else
			{
				if (this.needResetAfterLoose)
				{
					this.needResetAfterLoose = false;
					this.resetStartPos();
					this.resetTouchPoint();
				}
				this.calcTouchVector();
			}

			// this.exportTouchStatus()
		}

	}

}
