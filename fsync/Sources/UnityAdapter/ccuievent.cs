
using UnityEngine;

using number = System.Double;

namespace kitten.UnityAdapter
{
	public class ScreenTool
	{

		public static readonly ScreenTool screenTool = new ScreenTool().init();

		public virtual float designWidth
		{
			get
			{
				return UnityEngine.Screen.width;
			}
		}
		public virtual float designHeight
		{
			get
			{
				return UnityEngine.Screen.height;
			}
		}

		public virtual ScreenTool init()
		{
			return this;
		}


		/**
		 * 从中央坐标系坐标转换到左下角坐标系坐标
		 * @param x
		 */
		public virtual number convertDesignXReverse(number x)
		{
			return x - this.designWidth / 2;
		}

		/**
		 * 从中央坐标系坐标转换到左下角坐标系坐标
		 * @param x
		 */
		public virtual number convertDesignYReverse(number y)
		{
			// return this.designHeight / 2 - y;
			return y - this.designHeight / 2;
		}

		/**
		 * 从左下角坐标系坐标转换到中央坐标系坐标
		 * @param x 
		 */
		public virtual number convertDesignX(number x)
		{
			return x + this.designWidth / 2;
		}
		/**
		 * 从左下角坐标系坐标转换到中央坐标系坐标
		 * @param x
		 */
		public virtual number convertDesignY(number y)
		{
			// return this.designHeight / 2 - y;
			return y + this.designHeight / 2;
		}

	}
}
