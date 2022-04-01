
namespace fsync.amath
{
	using lang.common;
	using number = System.Double;
	using Math = System.Math;

	public interface IClone
	{
		// IClone clone();
	}
	public interface IVector : IClone
	{
		number[] getBinData();
	}
	public abstract class CommonVector : IVector
	{
		public abstract number[] getBinData();
	}

	public class Vector
	{
		protected static Vector3 _fromNumArray3(number[] ns)
		{
			var vec = new Vector3();
			vec.setBinData(ns);
			return vec;
		}

		protected static Vector4 _fromNumArray4(number[] ns)
		{
			var vec = new Vector4();
			vec.setBinData(ns);
			return vec;
		}

		public static number distanceSQ(IVector vec1, IVector vec2)
		{
			var n1 = vec1.getBinData();
			var n2 = vec2.getBinData();
			double total = 0;
			for (var i = 0; i < System.Math.Min(n1.Length, n2.Length); i++)
			{
				total += (n1[i] - n2[i]) * (n1[i] - n2[i]);
			}
			return total;
		}

		public static number distance(IVector vec1, IVector vec2)
		{
			var total = Vector.distanceSQ(vec1, vec2);
			return Math.Sqrt(total);
		}

		public static bool equal(IVector vec1, IVector vec2)
		{
			var n1 = vec1.getBinData();
			var n2 = vec2.getBinData();
			if (n1.Length != n2.Length)
			{
				return false;
			}

			for (var i = 0; i < n1.Length; i++)
			{
				if (n1[i] != n2[i])
				{
					return false;
				}
			}
			return true;
		}

		public static T subDown<T>(T vec1, T vec2) where T : IVector
		{
			var n1 = vec1.getBinData();
			var n2 = vec2.getBinData();
			for (var i = 0; i < Math.Min(n1.Length, n2.Length); i++)
			{
				n1[i] -= n2[i];
			}
			return vec1;
		}

		public static T addUp<T>(T vec1, T vec2) where T : IVector
		{
			var n1 = vec1.getBinData();
			var n2 = vec2.getBinData();
			for (var i = 0; i < Math.Min(n1.Length, n2.Length); i++)
			{
				n1[i] += n2[i];
			}
			return vec1;
		}

		public static T multUp<T>(T vec1, T vec2) where T : IVector
		{
			var n1 = vec1.getBinData();
			var n2 = vec2.getBinData();
			for (var i = 0; i < Math.Min(n1.Length, n2.Length); i++)
			{
				n1[i] *= n2[i];
			}
			return vec1;
		}

		public static T multUpVar<T>(T vec1, number v) where T : IVector
		{
			var n1 = vec1.getBinData();
			for (var i = 0; i < n1.Length; i++)
			{
				n1[i] *= v;
			}
			return vec1;
		}

		public static T multVar<T>(T vec1, number v) where T : class, IVector
		{
			var newVec = new Vector3() as IVector;
			var n1 = newVec.getBinData();
			var n2 = vec1.getBinData();
			for (var i = 0; i < n1.Length; i++)
			{
				n1[i] = n2[i] * v;
			}
			return newVec as T;
		}

		public static T normalizeSelf<T>(T vec) where T : IVector
		{
			var n1 = vec.getBinData();
			double lsq = 0;
			for (var i = 0; i < n1.Length; i++)
			{
				lsq += n1[i] * n1[i];
			}
			if (lsq == 0)
			{
				for (var i = 0; i < n1.Length; i++)
				{
					n1[i] = 0;
				}
			}
			else
			{
				var l = Math.Sqrt(lsq);
				for (var i = 0; i < n1.Length; i++)
				{
					n1[i] /= l;
				}
			}
			return vec;
		}

		public static number len<T>(T vec) where T : IVector
		{
			var n1 = vec.getBinData();
			return NumberArray.len(n1);
		}

		/**
		 * 覆盖
		 * @param out1 
		 * @param vec2 
		 */
		public static T merge<T>(T out1, T vec2) where T : IVector
		{
			NumberArray.merge(out1.getBinData(), vec2.getBinData());
			return out1;
		}

		/**
		 * 最小合并
		 * @param vec1 
		 * @param vec2 
		 */
		public static T collect<T>(T vec1, T vec2) where T : IVector
		{
			NumberArray.collect(vec1.getBinData(), vec2.getBinData());
			return vec1;
		}

		public static Vector4 transEulerToQuaternion(Vector4 quat, Vector3 vec3)
		{
			NumberArray.transEulerToQuaternion(quat.getBinData(), vec3.getBinData());
			return quat;
		}

		public static Vector3 transQuaternionToEuler(Vector3 vec3, Vector4 quat, bool outerZ = false)
		{
			NumberArray.transQuaternionToEuler(vec3.getBinData(), quat.getBinData(), outerZ);
			return vec3;
		}

		public static Vector4 multiplyQuaternion(Vector4 out1, Vector4 a, Vector4 b)
		{
			NumberArray.multiplyQuaternion(out1.getBinData(), a.getBinData(), b.getBinData());
			return out1;
		}

		public static IVector resetValues(IVector vec, number value = 0)
		{
			var ns = vec.getBinData();
			for (var i = 0; i < ns.Length; i++)
			{
				ns[i] = value;
			}
			return vec;
		}

		/**
		 * 3维叉乘
		 * @param out1 
		 * @param a 
		 */
		public static Vector3 crossBy3(Vector3 out1, Vector3 a)
		{
			// var { x: ax, y: ay, z: az } = a;
			// var { x: bx, y: by, z: bz } = out1;
			var ax = a.x;
			var ay = a.y;
			var az = a.z;

			var bx = out1.x;
			var by = out1.y;
			var bz = out1.z;

			out1.x = ay * bz - az * by;
			out1.y = az * bx - ax * bz;
			out1.z = ax * by - ay * bx;
			return out1;
		}

		public static number dot(Vector3 out1, Vector3 a)
		{
			double n = 0;
			var ns1 = out1.getBinData();
			var ns2 = a.getBinData();
			var l = Math.Min(ns1.Length, ns2.Length);
			for (var i = 0; i < l; i++)
			{
				n += (ns1[i] * ns2[i]);
			}
			return n;
		}

		/**
		 * 根据x，y决定的方向转换为角度 [-PI~PI]
		 * @param b 
		 */
		public static number getRotationZ2(IVector b)
		{
			var data = b.getBinData();
			var th = Math.Atan2(data[1], data[0]);
			return th;
		}

		/**
		 * 根据x，y决定的方向转换为角度 [-PI~PI]
		 * @param b 
		 */
		public static Vector3 getRotation2(IVector b)
		{
			var data = b.getBinData();
			var th = Math.Atan2(data[1], data[0]);
			return Vector3.fromNumArray(new number[] { 0, 0, th });
		}

		/**
		 * 绕原点按笛卡尔坐标系弧度旋转
		 * @param out1 
		 */
		public static IVector rotateSelfByZero2(IVector out1, number angle)
		{
			var od = out1.getBinData();
			var cosInc = Math.Cos(angle);
			var sinInc = Math.Sin(angle);
			var x = cosInc * od[0] - sinInc * od[1];
			var y = sinInc * od[0] + cosInc * od[1];
			od[0] = x;
			od[1] = y;
			return out1;
		}

		public static T asVectorN<T>(IVector b) where T : class, IVector
		{
			return b as T;
		}

		public static Vector2 asVector2(IVector b)
		{
			return b as Vector2;
		}

		public static Vector3 asVector3(IVector b)
		{
			return b as Vector3;
		}

		public static Vector4 asVector4(IVector b)
		{
			return b as Vector4;
		}

	}
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

	public class Vector3 : CommonVector
	{
		protected number[] data = new number[] { 0, 0, 0, };

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

        public override string ToString()
        {
			return $"Vec3{{{this.x},{this.y},{this.z}}}";
        }
        public Vector3()
		{
		}

		public Vector3(number x, number y, number z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public override number[] getBinData()
		{
			return this.data;
		}

		public virtual void setBinData(number[] data)
		{
			this.data = data;
		}

		public number len
		{
			get
			{
				return System.Math.Sqrt(x * x + y * y + z * z);
			}
		}

		public Vector3 merge(Vector3 b)
		{
			this.x = b.x;
			this.y = b.y;
			this.z = b.z;
			return this;
		}

		public Vector3 clone()
		{
			return new Vector3().merge(this);
		}

		public static Vector3 fromNumArray(number[] arr)
		{
			var vec = new Vector3();
            if (arr.Length >= 1)
            {
				vec.x = arr[0];
			}
			if(arr.Length >= 2)
            {
				vec.y = arr[1];
			}
			if( arr.Length >= 3)
            {
				vec.z = arr[2];
			}
			return vec;
		}
	}

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

	public class Rect : IWHRectSpec
	{

		public Rect()
		{
			this.x = 0;
			this.y = 0;
			this.width = 0;
			this.height = 0;
		}

		public static Vector2 top(IWHRectSpec self)
		{
			return new Vector2(self.x, self.y + self.height / 2);
		}

		public static Vector2 bottom(IWHRectSpec self)
		{
			return new Vector2(self.x, self.y - self.height / 2);
		}

		public static Vector2 center(IWHRectSpec self)
		{
			return new Vector2(self.x, self.y);
		}

		public static T copyRectLike<T>(T self, IWHRectSpec rect) where T : IWHRectSpec
		{
			var x = rect.x;
			var y = rect.y;
			var width = rect.width;
			var height = rect.height;
			self.x = x;
			self.y = y;
			self.width = width;
			self.height = height;
			return self;
		}


	}

	public class IWHRectSpec
	{
		public number x;
		public number y;
		public number width;
		public number height;
	}

	/**
	 * BLRect = 左下角 + size
	 */
	public class BLRect : IWHRectSpec
	{

		public BLRect()
		{
			this.x = 0;
			this.y = 0;
			this.width = 0;
			this.height = 0;
		}
		public BLRect(number x, number y, number width, number height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		public number centerX
		{
			get
			{
				return this.x;
			}
		}

		public number centerY
		{
			get
			{
				return this.y;
			}
		}

		public Vector2 top
		{
			get
			{
				return BLRect.top_s(this);
			}
		}

		public Vector2 bottom
		{
			get
			{
				return BLRect.bottom_s(this);
			}
		}

		public Vector2 center
		{
			get
			{
				return BLRect.center_s(this);
			}
		}

		public number leftX
		{
			get
			{
				return this.x;
			}
		}

		public number rightX
		{
			get
			{
				return this.x + this.width;
			}
		}

		public Vector2 left
		{
			get
			{
				var self = this;
				return new Vector2(self.x, self.y + this.height / 2);
			}
		}

		public Vector2 right
		{
			get
			{
				var self = this;
				return new Vector2(self.x + self.width, self.y + this.height / 2);
			}
		}

		public BLRect fromRectLike(IWHRectSpec spec)
		{
			return BLRect.copyRectLike_s(this, spec);
		}

		public BLRect copyRectLike(IWHRectSpec spec)
		{
			return BLRect.copyRectLike_s(this, spec);
		}

		public BLRect reset()
		{
			return BLRect.reset_s(this);
		}

		public BLRect mergeFrom(IWHRectSpec rect)
		{
			return BLRect.mergeFrom_s(this, rect);
		}

		public BLRect clone()
		{
			return BLRect.clone_s(this);
		}

		public bool containPoint(IVector pt)
		{
			return BLRect.containPoint_s(this, pt);
		}

		/**
		 * 将点就近限制在矩形框内
		 * @param rect 
		 * @param pt 
		 */
		public void limitPointSelf(IVector pt)
		{
			BLRect.limitPointSelf_s(this, pt);
		}

		public static Vector2 top_s(IWHRectSpec self)
		{
			return new Vector2(self.x + self.width / 2, self.y + self.height);
		}

		public static Vector2 bottom_s(IWHRectSpec self)
		{
			return new Vector2(self.x + self.width / 2, self.y);
		}

		public static Vector2 center_s(IWHRectSpec self)
		{
			return new Vector2(self.x + self.width / 2, self.y + self.height / 2);
		}

		public static IWHRectSpec fromRectLike_s(IWHRectSpec self)
		{
			var x = self.x;
			var y = self.y;
			var width = self.width;
			var height = self.height;
			return new BLRect(x, y, width, height);
		}

		public static T copyRectLike_s<T>(T self, IWHRectSpec rect) where T : IWHRectSpec
		{
			self.x = rect.x;
			self.y = rect.y;
			self.width = rect.width;
			self.height = rect.height;
			return self;
		}

		public static T reset_s<T>(T self) where T : IWHRectSpec
		{
			self.x = 0;
			self.y = 0;
			self.width = 0;
			self.height = 0;
			return self;
		}

		public static T mergeFrom_s<T>(T self, IWHRectSpec rect) where T : IWHRectSpec
		{
			self.width = rect.width;
			self.height = rect.height;
			self.x = rect.x;
			self.y = rect.y;
			return self;
		}

		public static BLRect clone_s(IWHRectSpec self)
		{
			var rect = new BLRect();
			BLRect.mergeFrom_s(rect, self);
			return rect;
		}

		public static bool containPoint_s(IWHRectSpec rect, IVector pt)
		{
			var ns = pt.getBinData();
			var x = ns[0];
			var y = ns[1];
			if (
				(rect.x - x) * (rect.x + rect.width - x) <= 0
				&& (rect.y - y) * (rect.y + rect.height - y) <= 0
			)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/**
		 * 将点就近限制在矩形框内
		 * @param rect
		 * @param pt
		 */
		public static void limitPointSelf_s(IWHRectSpec rect, IVector pt)
		{
			var ns = pt.getBinData();
			var x = ns[0];
			var y = ns[1];
			if (x < rect.x)
			{
				x = rect.x;
			}
			var rx = rect.x + rect.width;
			if (x > rx)
			{
				x = rx;
			}
			if (y < rect.y)
			{
				y = rect.y;
			}
			var ry = rect.y + rect.height;
			if (y > ry)
			{
				y = ry;
			}
			ns[0] = x;
			ns[1] = y;
		}
	}


}

