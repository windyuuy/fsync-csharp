
namespace fsync.amath
{
	using number = System.Double;
	using TSVector = Vector3;

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

		public number lenSQ
		{
			get
			{
				return (x * x + y * y + z * z);
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
			if (arr.Length >= 2)
			{
				vec.y = arr[1];
			}
			if (arr.Length >= 3)
			{
				vec.z = arr[2];
			}
			return vec;
		}

		public new Vector3 normalizeSelf()
		{
			return Vector.normalizeSelf(this);
		}

		/// <summary>
		/// A vector with components (0,0,0);
		/// </summary>
		public static readonly Vector3 zero;
		/// <summary>
		/// A vector with components (-1,0,0);
		/// </summary>
		public static readonly Vector3 left;
		/// <summary>
		/// A vector with components (1,0,0);
		/// </summary>
		public static readonly Vector3 right;
		/// <summary>
		/// A vector with components (0,1,0);
		/// </summary>
		public static readonly Vector3 up;
		/// <summary>
		/// A vector with components (0,-1,0);
		/// </summary>
		public static readonly Vector3 down;
		/// <summary>
		/// A vector with components (0,0,-1);
		/// </summary>
		public static readonly Vector3 back;
		/// <summary>
		/// A vector with components (0,0,1);
		/// </summary>
		public static readonly Vector3 forward;
		/// <summary>
		/// A vector with components (1,1,1);
		/// </summary>
		public static readonly Vector3 one;
		#region public static number operator *(JVector value1, JVector value2)
		public static number operator *(Vector3 value1, Vector3 value2)
		{
			return Vector.dot(value1, value2);
		}
		#endregion

		/// <summary>
		/// Multiplies a vector by a scale factor.
		/// </summary>
		/// <param name="value1">The vector to scale.</param>
		/// <param name="value2">The scale factor.</param>
		/// <returns>Returns the scaled vector.</returns>
		#region public static JVector operator *(JVector value1, number value2)
		public static Vector3 operator *(Vector3 value1, number value2)
		{
			Vector3 result = new Vector3();
			result.merge(value1);
			Vector.multUpVar(result, value2);
			return result;
		}
		#endregion

		/// <summary>
		/// Multiplies a vector by a scale factor.
		/// </summary>
		/// <param name="value2">The vector to scale.</param>
		/// <param name="value1">The scale factor.</param>
		/// <returns>Returns the scaled vector.</returns>
		public static Vector3 operator *(number value1, Vector3 value2)
		{
			Vector3 result = new Vector3();
			result.merge(value2);
			Vector.multUpVar(result, value1);
			return result;
		}


		/// <summary>
		/// Subtracts two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The difference of both vectors.</returns>
		#region public static JVector operator -(JVector value1, JVector value2)
		public static TSVector operator -(TSVector value1, TSVector value2)
		{
			TSVector result = new TSVector();
			result.merge(value1);
			Vector.subDown(result, value2);
			return result;
		}
		#endregion

		/// <summary>
		/// Negetive vector.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The difference of both vectors.</returns>
		#region public static JVector operator -(JVector value1)
		public static TSVector operator -(TSVector value1)
		{
			return new TSVector(-value1.x, -value1.y, -value1.z);
		}
		#endregion

	}


}
