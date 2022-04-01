
using UnityEngine;
using System;
using System.Collections.Generic;

namespace kitten.UnityAdapter
{
	public class SimpleNodePool
	{
		protected List<GameObject> nodePool;
		protected GameObject defaultSprite = null;
		protected GameObject container = null;

		public SimpleNodePool init()
		{
			this.nodePool = new List<GameObject>();
			return this;
		}

		public void initSlots(GameObject c, GameObject s)
		{
			this.container = c;
			this.defaultSprite = s;
		}

		public GameObject pop()
		{
			GameObject node = null;
			if (this.nodePool.Count > 0)
			{
				var lastIndex = this.nodePool.Count - 1;
				node = this.nodePool[lastIndex];
				this.nodePool.RemoveAt(lastIndex);
			}
			if (!node)
			{
				node = GameObject.Instantiate(this.defaultSprite);
				//node.transform.parent = this.container.transform;
				node.transform.SetParent(this.container.transform, false);
			}
			return node;
		}

		public void push(GameObject node)
		{
			this.nodePool.Add(node);
		}

		public static SimpleNodePool defaultNodePool = new SimpleNodePool().init();
	}
}
