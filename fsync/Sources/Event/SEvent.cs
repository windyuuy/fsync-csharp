
using System;
using System.Collections.Generic;

namespace fsync.Event
{
	/// <summary>
	/// 事件发送器
	/// </summary>
	public class SEvent<K, T>
	{
		/// <summary> 事件表 </summary>
		protected Dictionary<K, Action<T>> _Event;

		public SEvent()
		{
			this.Reset();
		}

		public virtual void Reset()
		{
			_Event = new Dictionary<K, Action<T>>();
		}

		/// <summary> 添加事件监听器 </summary>
		/// <param name="eventType">事件类型</param>
		/// <param name="eventHandler">事件处理器</param>
		public virtual Action<T> On(K state, Action<T> call)
		{
			if (this._Event!.ContainsKey(state))
			{
				this._Event[state] += call;
			}
			else
			{
				this._Event[state] = call;
			}
			return call;
		}

		/// <summary> 移除事件监听器 </summary>
		/// <param name="eventType">事件类型</param>
		/// <param name="eventHandler">事件处理器</param>
		public virtual void Off(K state, Action<T> call)
		{
			if (this._Event!.ContainsKey(state))
			{
				if (this._Event[state] != null)
				{
					this._Event[state] -= call;
				}
			}
		}

		/// <summary> 发送事件 </summary>
		/// <param name="eventType">事件类型</param>
		/// <param name="eventArg">事件参数</param>
		public virtual void Emit(K state, T data)
		{
			if (this._Event!.ContainsKey(state))
			{
				this._Event[state]?.Invoke(data);
			}
		}

		/// <summary> 是否已经拥有该类型的事件监听器 </summary>
		/// <param name="eventType">事件名称</param>
		public virtual bool HasListener(K eventType)
		{
			return _Event.ContainsKey(eventType);
		}

		public virtual void DriveHead(K eventType, T eventArg)
		{
			Action<T> callbacks;
			if (_Event.TryGetValue(eventType, out callbacks))
			{
				if (callbacks != null)
				{
					var list = callbacks.GetInvocationList();
					if (list.Length > 0)
					{
						var call0 = list[0];
						var call = call0 as Action<T>;
						_Event[eventType] = callbacks - call;
						call(eventArg);
					}
				}
			}
		}

		/// <summary> 清理所有事件监听器 </summary>
		public virtual void Clear()
		{
			_Event.Clear();
		}

	}

}
