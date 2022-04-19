
namespace fsync.amath
{
	using number = System.Double;

	public class Vector4 : CommonVector
	{
		public Vector4()
		{
			this.x = 0;
			this.y = 0;
			this.z = 0;
			this.w = 0;
		}
		protected number[] data = new number[] { 0, 0, 0, 0, };

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
		public number z
		{
			get => data[2];
			set => data[2] = value;
		}

		public number w
		{
			get => data[3];
			set => data[3] = value;
		}

		public override number[] getBinData()
		{
			return this.data;
		}

		public virtual void setBinData(number[] data)
		{
			this.data = data;
		}

	}


}

