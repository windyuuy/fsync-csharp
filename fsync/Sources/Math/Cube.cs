using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fsync.amath
{
	using number = System.Double;

	public class ICubeSpec
	{
		public number x;
		public number y;
		public number z;
		public number width;
		public number height;
		public number depth;
	}

	public class Cube: ICubeSpec
	{

		public Cube()
		{
			this.x = 0;
			this.y = 0;
			this.z = 0;
			this.width = 0;
			this.height = 0;
			this.depth = 0;
		}

		public virtual Vector3 center()
        {
			return Cube.center(this);
        }

		public static Vector3 center(ICubeSpec self)
		{
			return new Vector3(self.x, self.y,self.z);
		}

	}
}
