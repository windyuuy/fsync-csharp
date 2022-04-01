namespace kitten.gamepad
{
	using fsync.amath;

	using Vector3 = fsync.amath.Vector3;
	using UserInput = fsync.UserInput;

	/**
	 * 基础控制器视图
	 */
	public class CircleStickView
	{
		/**
		 * 对应控制器ID
		 */
		public string ctrlId;
		protected graphengine.ISprite circleView;

		public CircleStickView init()
		{
			return this;
		}

		public void setupView(CircleStick ctrl, string color)
		{
			//var length = Vector.len(UserInput.inst.clientSize);

			this.ctrlId = ctrl.identity;
			this.circleView = graph.Graph.graph.createSprite();
			this.circleView.setColor(color);
			this.circleView.setRadius(ctrl.getCircleRadius());
			var center = ctrl.getCtrlCenterPos();
			this.circleView.setPos(center.x, center.y);
		}

		public void updateView(CircleStick ctrl)
        {
			var center = ctrl.getCtrlCenterPos();
			this.circleView.setPos(center.x, center.y);
		}

		public bool visible
        {
			get { return circleView.visible; }
			set { circleView.visible = value; }
        }

	}
}