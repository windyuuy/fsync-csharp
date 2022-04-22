
using UnityEngine;
using number = System.Double;

namespace gcc.common
{
	public class TransformTool
	{
		public static readonly TransformTool Inst = new TransformTool();

		public virtual void setScale(GameObject gameObject, number scale)
		{
			// gameObject.transform.localScale.
		}

		public virtual RectTransform getUITransform(GameObject stickRange)
		{
			// return stickRange.transform;
			return stickRange.GetComponent<RectTransform>();
		}

		public virtual RectTransform getUITransform(Transform stickRange)
		{
			// return stickRange.transform;
			return stickRange.GetComponent<RectTransform>();
		}


		public virtual Vector3 convVectorToPos3(fsync.amath.Vector3 pt)
		{
			var pos = new Vector3();
			pos.x = (float)pt.x;
			pos.y = (float)pt.y;
			pos.z = (float)pt.z;
			return pos;
		}
		public virtual fsync.amath.Vector3 convPos3ToVector(Vector3 pt, fsync.amath.Vector3? pout = null)
		{
			if (pout == null)
			{
				pout = new fsync.amath.Vector3();
			}
			pout.x = pt.x;
			pout.y = pt.y;
			pout.z = pt.z;
			return pout;
		}
		public virtual fsync.amath.Vector4 convPos4ToVector(Vector4 pt)
		{
			var pos = new fsync.amath.Vector4();
			pos.x = pt.x;
			pos.y = pt.y;
			pos.z = pt.z;
			pos.w = pt.w;
			return pos;
		}
		public virtual fsync.amath.Vector4 convQuatToVector(Quaternion pt)
		{
			var pos = new fsync.amath.Vector4();
			pos.x = pt.x;
			pos.y = pt.y;
			pos.z = pt.z;
			pos.w = pt.w;
			return pos;
		}

	}

}
