
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using graphengine;

using number = System.Double;
namespace kitten.UnityAdapter
{
	public class SpriteImpl : graphengine.ISprite
	{
		public SpriteImpl()
		{
			this.init();
		}

		static number s_objId = 1;
		public number objId = 0;

		public GameObject node;
		public Image sprite;

		public bool visible
		{
			get { return this.node.activeSelf; }
			set { this.node.SetActive(value); }
		}

		protected void init()
		{
			this.objId = SpriteImpl.s_objId++;

			var node = SimpleNodePool.defaultNodePool.pop();

			this.node = node;
			// this.node["objId"] = this.objId;
			this.sprite = node.GetComponent<Image>();
			this.node.transform.localPosition = new Vector3(0, 0, 0);
		}

		public void destroy()
		{
			// this.node.destroy()
			var loc = this.node.transform.localPosition;
			loc.x = 10000;
			this.node.transform.localPosition = loc;
			SimpleNodePool.defaultNodePool.push(this.node);
			this.node = null;
			this.sprite = null;
		}

		public void setColor(string clr)
		{
			var ncolor = UnityEngine.Color.white;
			if (clr == "red")
			{
				ncolor = UnityEngine.Color.red;
			}
			else if (clr == "blue")
			{
				ncolor = UnityEngine.Color.blue;
			}
			else if (clr == "yellow")
			{
				ncolor = UnityEngine.Color.yellow;
			}
			else if (clr == "black")
			{
				ncolor = UnityEngine.Color.black;
			}
			else if (clr == "green")
			{
				ncolor = UnityEngine.Color.green;
			}
			else if (clr == "gray")
			{
				ncolor = UnityEngine.Color.gray;
			}
			else if (clr.IndexOf("(") >= 0)
			{
				var sc = clr.Split('(')[1];
				sc = sc.Split(')')[0];
				var colors = sc.Split(new string[] { ", " }, System.StringSplitOptions.None);
				var ncolors = colors.Take(3).Select(c => (byte)System.Double.Parse(c)).ToArray();
				var alpha = System.Double.Parse(colors[3]);
				ncolor = new UnityEngine.Color32(ncolors[0], ncolors[1], ncolors[2], (byte)(alpha * 255));
			}
			else
			{
				throw new System.Exception("invalid color");
			}

			// if (this.sprite['color']) {
			// 	this.sprite['color'] = ncolor;
			// }
			// if (this.node['color']) {
			// 	this.node['color'] = ncolor;
			// }
			this.sprite.color = ncolor;
		}

		public void setPos(number x, number y)
		{
			this.node.transform.localPosition = new Vector3(
				(float)ScreenTool.screenTool.convertDesignXReverse(x),
				(float)ScreenTool.screenTool.convertDesignYReverse(y),
				0
			);
		}
		public void setSize(number width, number height)
		{
			// if (this.node['setContentSize']) {
			// 	this.node['setContentSize'](width, height);
			// } else {
			// 	this.node.setScale(width, height, 1);
			// }
			this.node.transform.localScale = new Vector3((float)width, (float)height, 1);
			//this.node.GetComponent<RectTransform>().rect.Set()
		}
		public void setRadius(number radius)
		{
			// if (this.node['setContentSize']) {
			// 	this.node['setContentSize'](radius, radius);
			// } else {
			// 	this.node.setScale(radius, radius, radius);
			// }
			this.node.transform.localScale = new Vector3((float)radius, (float)radius, (float)radius);
		}

	}

	public class GraphImpl : graphengine.IGraph
	{
		public ISprite createSprite()
		{
			return new SpriteImpl();
		}
	}
}
