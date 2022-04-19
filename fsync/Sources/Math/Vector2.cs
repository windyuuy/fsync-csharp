
namespace fsync.amath
{
	using number = System.Double;

	public class Vector2 : CommonVector
	{

		public Vector2()
		{
		}

		public override number[] getBinData()
		{
			return this.data;
		}
		public virtual void setBinData(number[] data)
		{
			this.data = data;
		}

		public Vector2(number x, number y)
		{
			this.x = x;
			this.y = y;
		}
		protected number[] data = new number[] { 0, 0, };
		public number x
		{
			get => data[0];
			set => data[0] = value;
		}
		public number y
		{
			get => data[1];
			set => data[1] = value;
		}
	}


}

