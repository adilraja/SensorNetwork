using System;

namespace SensorNetwork2
{
	/// <summary>
	/// Summary description for MessageList.
	/// </summary>
	public class MessageList
	{
		private String Message;
		private int iter;
		private MessageList next;
		public MessageList()
		{
			//
			// TODO: Add constructor logic here
			//
			this.iter=0;
		}
		public MessageList NEXT
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next=value;
			}
		}
		public int ITERATION
		{
			get
			{
				return this.iter;
			}
			set
			{
				this.iter=value;
			}
		}
		public String MESSAGE
		{
			get
			{
				return this.Message;
			}
			set
			{
				this.Message=value;
			}
		}
		~MessageList()
		{
			this.MESSAGE=null;
		}
	}
}