
namespace fsync.amath
{
	using lang.common;
	using number = System.Double;
	using Math = System.Math;

	public interface IVector : IClone
	{
		number[] getBinData();
	}
	public abstract class CommonVector : IVector
	{
		public abstract number[] getBinData();

		public CommonVector normalizeSelf()
		{
			return Vector.normalizeSelf(this);
		}

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

		public static number dot(Vector3 a, Vector3 b)
		{
			double n = 0;
			var ns1 = a.getBinData();
			var ns2 = b.getBinData();
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


}

