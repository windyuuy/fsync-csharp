namespace lang.common
{
	using number = System.Double;
	using Math = System.Math;

	public class NumberArray
	{
		public static number lenSQ(number[] ns)
		{
			number lsq = 0;
			for (var i = 0; i < ns.Length; i++)
			{
				lsq += ns[i] * ns[i];
			}
			return lsq;
		}
		public static number len(number[] ns)
		{
			number lsq = 0;
			for (var i = 0; i < ns.Length; i++)
			{
				lsq += ns[i] * ns[i];
			}
			return Math.Sqrt(lsq);
		}

		/**
		 * 覆盖
		 * @param out 
		 * @param ns2 
		 */
		public static number[] merge(number[] out1, number[] ns2)
		{
			for (var i = 0; i < ns2.Length; i++)
			{
				out1[i] = ns2[i];
			}
			return out1;
		}

		/**
		 * 最小合并
		 * @param ns1 
		 * @param ns2 
		 */
		public static number[] collect(number[] ns1, number[] ns2)
		{
			var count = Math.Min(ns1.Length, ns2.Length);
			for (var i = 0; i < count; i++)
			{
				ns1[i] = ns2[i];
			}
			return ns1;
		}

		public static number[] normalizeSelf(number[] n1)
		{
			number lsq = 0;
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
			return n1;
		}
		public static readonly number halfToRad = 0.5 * Math.PI / 180.0;

		public static readonly number _d2r = Math.PI / 180.0;

		public static readonly number _r2d = 180.0 / Math.PI;

		public static number toDegree(number a)
		{
			return a * _r2d;
		}

		public static number[] transEulerToQuaternion(number[] ns4, number[] ns3)
		{
			var x = ns3[0];
			var y = ns3[1];
			var z = ns3[2];


			x *= halfToRad;
			y *= halfToRad;
			z *= halfToRad;

			var sx = Math.Sin(x);
			var cx = Math.Cos(x);
			var sy = Math.Sin(y);
			var cy = Math.Cos(y);
			var sz = Math.Sin(z);
			var cz = Math.Cos(z);

			ns4[0] = sx * cy * cz + cx * sy * sz;
			ns4[1] = cx * sy * cz + sx * cy * sz;
			ns4[2] = cx * cy * sz - sx * sy * cz;
			ns4[3] = cx * cy * cz - sx * sy * sz;

			return ns4;
		}

		public static number[] transQuaternionToEuler(number[] ns3, number[] ns4, bool outerZ = false)
		{
			var x = ns4[0];
			var y = ns4[1];
			var z = ns4[2];
			var w = ns4[3];


			number bank = 0;
			number heading = 0;
			number attitude = 0;
			var test = x * y + z * w;
			if (test > 0.499999)
			{
				bank = 0; // default to zero
				heading = toDegree(2 * Math.Atan2(x, w));
				attitude = 90;
			}
			else if (test < -0.499999)
			{
				bank = 0; // default to zero
				heading = -toDegree(2 * Math.Atan2(x, w));
				attitude = -90;
			}
			else
			{
				var sqx = x * x;
				var sqy = y * y;
				var sqz = z * z;
				bank = toDegree(Math.Atan2(2 * x * w - 2 * y * z, 1 - 2 * sqx - 2 * sqz));
				heading = toDegree(Math.Atan2(2 * y * w - 2 * x * z, 1 - 2 * sqy - 2 * sqz));
				attitude = toDegree(Math.Asin(2 * test));
				if (outerZ)
				{
					bank = -180 * Math.Sign(bank + 1e-6) + bank;
					heading = -180 * Math.Sign(heading + 1e-6) + heading;
					attitude = 180 * Math.Sign(attitude + 1e-6) - attitude;
				}
			}

			// x
			ns3[0] = bank;
			// y
			ns3[1] = heading;
			// z
			ns3[2] = attitude;
			return ns3;
		}


		/**
		 * @zh 四元数乘法
		 */
		public static number[] multiplyQuaternion(number[] out1, number[] a, number[] b)
		{
			var x = a[0] * b[3] + a[3] * b[0] + a[1] * b[2] - a[2] * b[1];
			var y = a[1] * b[3] + a[3] * b[1] + a[2] * b[0] - a[0] * b[2];
			var z = a[2] * b[3] + a[3] * b[2] + a[0] * b[1] - a[1] * b[0];
			var w = a[3] * b[3] - a[0] * b[0] - a[1] * b[1] - a[2] * b[2];
			out1[0] = x;
			out1[1] = y;
			out1[2] = z;
			out1[3] = w;
			return out1;
		}

	}
}
