using System;

namespace SensorNetwork2
{
	/// <summary>
	/// Summary description for ReceivedMessageList.
	/// </summary>
	public class ReceivedMessageList
	{
		private float mac_x;
		private float mac_y;
		private bool nodeReceivedMessage;
		private int iteration;
		public ReceivedMessageList Next;
		public ReceivedMessageList()
		{
			//
			// TODO: Add constructor logic here
			//
			this.nodeReceivedMessage=false;//node didnot receive a message
			this.iteration=0;
		}
		public float MAC_X
		{
			get
			{
				return this.mac_x;
			}
			set
			{
				this.mac_x=value;
			}
		}
		public float MAC_Y
		{
			get
			{
				return this.mac_y;
			}
			set
			{
				this.mac_y=value;
			}
		}
		public bool NODERECEIVEDMESSAGE
		{
			get
			{
				return this.nodeReceivedMessage;
			}
			set
			{
				this.nodeReceivedMessage=value;
			}
		}
		public int ITERATION
		{
			get
			{
				return this.iteration;
			}
			set
			{
				this.iteration=value;
			}
		}

	}
}
