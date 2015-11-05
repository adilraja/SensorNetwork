using System;
using GDIInterop;
using System.Drawing;
namespace SensorNetwork2
{
	/// <summary>
	/// Summary description for SensorNode.
	/// </summary>
	/// //msg format:
	/// //srcMac,DstMac, MsgType,Data(cost, Power)etc
	public class SensorNode
	{
		private int pixel_number_x;
		private int pixel_number_y;
		private int parent_pixel_number_x;
		private int parent_pixel_number_y;
		private int simulationSpeed;
		private float mac_x;
		private float mac_y;
		private float p_mac_x;
		private float p_mac_y;
		private float old_p_mac_x;
		private float old_p_mac_y;
		private int MessageCycles;
		private int totalBitsTransmitted;
		private int totalBitsReceived;
		private string childMacList;
		private int hops;
		private string receivedApplicationMessages;
		private int receivedApplicationMessagesCount;
		private int nodeNum;
		private double PowerOfThePath;
		private int children;
		private float xmissionRange;
		private bool joinStatus;//whether a node is joined or not
		private bool nodeDead;//whether a node is dead or alive (true for dead false for alive)
		private bool nodeType;//leader or ordinary? true for leader false for ordinary
		private bool neighborReceivedXmitted;
		private int nodeState;//0,1,2,3
		private int iteration;
		private Image img;
		private SensorNode next;
		public NodeBattery battery;
		public SensorNode(bool TypeofNode,float range, int num, int speed )
		{
			//
			// TODO: Add constructor logic here
			//
			this.joinStatus=false;
			this.nodeDead=false;//the node is alive
			this.nodeType=TypeofNode;
			this.simulationSpeed=speed;
			this.children=0;
			this.iteration=0;
			// the node is in the receiving mode.
			this.nodeNum=num;
			this.xmissionRange=range;
			this.old_p_mac_x=-100;
			this.old_p_mac_y=-100;
			this.p_mac_x=40000;
			this.p_mac_y=40000;
			this.MessageCycles=0;
			this.totalBitsTransmitted=0;
			this.totalBitsReceived=0;
			this.childMacList=null;
			this.neighborReceivedXmitted=false;
			this.receivedApplicationMessages=null;
			this.receivedApplicationMessagesCount=0;
			if(TypeofNode==false)
			{
				this.img=ImageStream.FromFile("unDecidedNode.bmp");
				this.battery=new NodeBattery(0.5);
				this.hops=10000;
				this.nodeState=0;
				this.PowerOfThePath=0;
				this.nodeState=0;
			}
			else
			{
				this.img=ImageStream.FromFile("BaseStation.bmp");
				this.battery=new NodeBattery(10000);
				this.nodeState=1;
				this.hops=0;
				this.PowerOfThePath=this.battery.BATTERYPOWER;
				this.nodeState=1;
			}
		}
		public void bitmapUPDater()
		{
			if(this.battery.BATTERYPOWER<=0)
			{
				this.nodeDead=true;//node dead
				this.joinStatus=false;//node not joint to the network anymore
				this.nodeState=0;
			}
			if(this.joinStatus==true && this.nodeType==false)
				this.img=ImageStream.FromFile("DecidedNode.bmp");
			if(this.battery.BATTERYPOWER<0.05)
				this.img=ImageStream.FromFile("unDecidedNode.bmp");
			if(this.joinStatus==false && this.nodeType==false && this.nodeDead==true)
				this.img=ImageStream.FromFile("BaseStation.bmp");
		}
		public String sendJoinMessage()
		{//message type 1
			double temp;
			int hps;
			if(this.PATH_POWER<this.battery.BATTERYPOWER)
				temp=this.PATH_POWER;
			else
				temp=this.battery.BATTERYPOWER;
			if(this.battery.BATTERYPOWER<0.05)
				hps=this.HOP_COUNT+=1;
			else
				hps=this.HOP_COUNT;
			if(this.nodeDead==false)
                return this.MAC_X.ToString()+";"+this.MAC_Y.ToString()+";"+"10000"+";"+"10000"+";"+"1"+";"+hps.ToString()+";"+temp.ToString()+";"+this.PIXEL_NUMBER_X.ToString()+";"+this.PIXEL_NUMBER_Y.ToString();
			else return null;
		}		//		token1					token2					tok3		tok4	  t5		t6							t7								t8											t9
		public String sendLeavingMessage()
		{//message type 3
			return this.MAC_X.ToString()+";"+this.MAC_Y.ToString()+";"+this.OLD_PARENT_MAC_X.ToString()+";"+this.OLD_PARENT_MAC_Y.ToString()+";"+"3";
		}
		public String sendJoiningMessage()
		{//message type 2
			return this.MAC_X.ToString()+";"+this.MAC_Y.ToString()+";"+this.PARENT_MAC_X.ToString()+";"+this.PARENT_MAC_Y.ToString()+";"+"2";
		}
		public String sendApplicationMessage()
		{
			String str;
			int MsgLength=2000;
			if(this.receivedApplicationMessages!=null)
			{
				String [] toks=this.receivedApplicationMessages.Split(';');
				MsgLength+=int.Parse(toks[5]);
			}
			str=this.MAC_X.ToString()+";"+this.MAC_Y.ToString()+";"+this.PARENT_MAC_X.ToString()+";"+this.PARENT_MAC_Y.ToString()+";"+"4"+";"+MsgLength.ToString()+";"+this.receivedApplicationMessages;
			return str;
		}
		public string receiveJoinMessage(string Msg)//returns nothing
		{
			String []tokens =Msg.Split(';');
			switch(this.joinStatus)
			{
				case false:
	//		if(this.joinStatus==false)
				{
					this.PARENT_MAC_X=float.Parse(tokens[0]);
					this.PARENT_MAC_Y=float.Parse(tokens[1]);
					this.PARENT_PIXEL_NUMBER_X=int.Parse(tokens[7]);
					this.PARENT_PIXEL_NUMBER_Y=int.Parse(tokens[8]);
					this.HOP_COUNT=int.Parse(tokens[5])+1;
					this.PATH_POWER=double.Parse(tokens[6]);
					this.JOINSTATUS=true;
					this.nodeState=2;
					break;
				}
				case true:
	//		else
				{
					if(int.Parse(tokens[5])<this.HOP_COUNT-1)
					{
						this.OLD_PARENT_MAC_X=this.PARENT_MAC_X;
						this.OLD_PARENT_MAC_Y=this.PARENT_MAC_Y;
						this.PARENT_MAC_X=float.Parse(tokens[0]);
						this.PARENT_MAC_Y=float.Parse(tokens[1]);
						this.PARENT_PIXEL_NUMBER_X=int.Parse(tokens[7]);
						this.PARENT_PIXEL_NUMBER_Y=int.Parse(tokens[8]);
						this.HOP_COUNT=int.Parse(tokens[5])+1;
						this.PATH_POWER=double.Parse(tokens[6]);
						this.nodeState=2;
					}
					else if(int.Parse(tokens[5])==this.HOP_COUNT-1 && double.Parse(tokens[6])>this.PATH_POWER)
					{
						this.OLD_PARENT_MAC_X=this.PARENT_MAC_X;
						this.OLD_PARENT_MAC_Y=this.PARENT_MAC_Y;
						this.PARENT_MAC_X=float.Parse(tokens[0]);
						this.PARENT_MAC_Y=float.Parse(tokens[1]);
						this.PARENT_PIXEL_NUMBER_X=int.Parse(tokens[7]);
						this.PARENT_PIXEL_NUMBER_Y=int.Parse(tokens[8]);
						this.HOP_COUNT=int.Parse(tokens[5])+1;
						this.PATH_POWER=double.Parse(tokens[6]);
						this.nodeState=2;
					}
					else if(float.Parse(tokens[0])==this.p_mac_x && float.Parse(tokens[1])==this.p_mac_y)//if the node gets a message from its own parent then it updates the following fields
					{
						this.OLD_PARENT_MAC_X=this.PARENT_MAC_X;
						this.OLD_PARENT_MAC_Y=this.PARENT_MAC_Y;
						this.PARENT_MAC_X=float.Parse(tokens[0]);
						this.PARENT_MAC_Y=float.Parse(tokens[1]);
						this.PARENT_PIXEL_NUMBER_X=int.Parse(tokens[7]);
						this.PARENT_PIXEL_NUMBER_Y=int.Parse(tokens[8]);
						this.HOP_COUNT=int.Parse(tokens[5])+1;
						this.PATH_POWER=double.Parse(tokens[6]);
						this.nodeState=4;
					}
						break;
				}
			}
			return Msg;
		}
		public string receiveJoiningMessage(string Msg)
		{
			if(this.childMacList!=null)
			{
				bool flag=false;
				String [] childrenMacs=this.childMacList.Split(';');//tokens of the child mac list
				String [] tokens=Msg.Split(';');//tokens of the message
				foreach(String childMac in childrenMacs)
				{
					String [] MacXY=childMac.Split(',');
					if(tokens[0].CompareTo(MacXY[0])==0 && tokens[1].CompareTo(MacXY[1])==0)
						flag=true;//the mac already exits n message sender is already a child
				}
				if(flag==false)//if the mac does not already exist
				{
					this.childMacList+=tokens[0];
					this.childMacList+=",";
					this.childMacList+=tokens[1];
					this.childMacList+=";";
					this.NUM_CHILDREN++;
				}
			}
			else
			{
				String [] tokens=Msg.Split(';');//tokens of the message
				this.childMacList+=tokens[0];	
				this.childMacList+=",";
				this.childMacList+=tokens[1];
				this.childMacList+=";";
				this.NUM_CHILDREN++;
			}
			return Msg;
		}
		public string receiveLeavingMessage(string Msg)
		{
			if(this.childMacList!=null)
			{
				bool flag=false;
				String [] childrenMacs=this.childMacList.Split(';');//tokens of the child mac list
				String [] tokens=Msg.Split(';');//tokens of the message
				int i=0, childTokenNum=0;
				foreach(String childMac in childrenMacs)
				{
					String [] MacXY=childMac.Split(',');
					if(tokens[0].CompareTo(MacXY[0])==0 && tokens[1].CompareTo(MacXY[1])==0)
					{
						flag=true;//the mac already exits n message sender is already a child so it has to be removed
						childTokenNum=i;
					}
					i++;
				}
				if(flag==true)
				{
					String tempArray=null;
					for(int j=0;j<i;j++)
					{
						if(j!=childTokenNum)
						{
							tempArray+=childrenMacs[j];
							tempArray+=";";
						}
					}
					this.childMacList=tempArray;
					this.NUM_CHILDREN--;
				}
			}
			return Msg;
		}
		public string receiveApplicationMessage(string Msg)
		{
			String []str1=Msg.Split(';');
			if(this.receivedApplicationMessages!=null)
			{
				String []str2=this.receivedApplicationMessages.Split(';');
				String str3=this.receivedApplicationMessages;
				int bits1, bits2;
				bits1=int.Parse(str1[5]);
				bits2=int.Parse(str2[5]);
				bits1+=bits2;
				this.receivedApplicationMessagesCount++;
				int i=0;
				this.receivedApplicationMessages=null;
				foreach(string tok in str1)
				{
					if(i!=5)
					{
						this.receivedApplicationMessages+=tok;
						this.receivedApplicationMessages+=";";
					}
					else
					{
						this.receivedApplicationMessages+=bits1.ToString();
						this.receivedApplicationMessages+=";";
					}
					i++;
				}
			}
			this.receivedApplicationMessages+=Msg;
			return Msg;
		}
		public string MessageReceiver(string Message)
		{
			String [] tokens=Message.Split(';');
			if(this.battery.BATTERYPOWER<=0)
			{
				this.nodeDead=true;//node dead
				this.joinStatus=false;//node not joint to the network anymore
				this.nodeState=0;
			}
			if(this.joinStatus==true && this.nodeType==false)
                this.img=ImageStream.FromFile("DecidedNode.bmp");
			if(this.battery.BATTERYPOWER<0.05)
				this.img=ImageStream.FromFile("unDecidedNode.bmp");
			if(this.joinStatus==false && this.nodeType==false && this.nodeDead==true)
				this.img=ImageStream.FromFile("BaseStation.bmp");
			if((float.Parse(tokens[2])==this.MAC_X && float.Parse(tokens[3])==this.MAC_Y)||(float.Parse(tokens[2])==10000 && float.Parse(tokens[3])==10000))
			{
				String str;
				if(int.Parse(tokens[4])==1 && float.Parse(tokens[2])==10000 && float.Parse(tokens[3])==10000)
				{
					if(this.nodeType==false)
					{//leader does not receive the join message
						if(this.nodeState !=2 && this.nodeState!=3)
						{
							this.totalBitsReceived+=41;
							this.battery.BATTERYPOWER-= 50*0.000000001*41*this.simulationSpeed;
							str=this.receiveJoinMessage(Message);
						}
					}
					else
						this.nodeState=1;
				}
				else if(int.Parse(tokens[4])==2 && float.Parse(tokens[2])==this.MAC_X && float.Parse(tokens[3])==this.MAC_Y)
				{
					this.totalBitsReceived+=35;
					this.battery.BATTERYPOWER-= 50*0.000000001*35*this.simulationSpeed;
					str=this.receiveJoiningMessage(Message);
					if(this.nodeType==false)
						this.nodeState=4;
					else
						this.nodeState=1;
				}
				else if(int.Parse(tokens[4])==3 && float.Parse(tokens[2])==this.MAC_X && float.Parse(tokens[3])==this.MAC_Y)
				{
					this.totalBitsReceived+=35;
					this.battery.BATTERYPOWER-= 50*0.000000001*35*this.simulationSpeed;
					str=this.receiveLeavingMessage(Message);
					if(this.nodeType==false)
						this.nodeState=4;
					else
						this.nodeState=1;
				}
				else if(int.Parse(tokens[4])==4 && float.Parse(tokens[2])==this.MAC_X && float.Parse(tokens[3])==this.MAC_Y)
				{
						int bits=int.Parse(tokens[5]);
						this.totalBitsReceived+=bits;
						this.battery.BATTERYPOWER-= 50*0.000000001*bits*this.simulationSpeed;
						str=this.receiveApplicationMessage(Message);
				}
			}
			return Message;
		}
		public String MessageTransmitter()
		{
			String str=null;
			if(this.battery.BATTERYPOWER<=0)
			{
				this.nodeDead=true;//node dead
				this.joinStatus=false;//node not joint to the network anymore
				this.nodeState=0;
			}
			if(this.joinStatus==true && this.nodeType==false)
				this.img=ImageStream.FromFile("DecidedNode.bmp");
			if(this.battery.BATTERYPOWER<0.05)
				this.img=ImageStream.FromFile("unDecidedNode.bmp");
			if(this.joinStatus==false && this.nodeType==false && this.nodeDead==true)
				this.img=ImageStream.FromFile("BaseStation.bmp");
			if((this.joinStatus==true || this.nodeType==true)&&this.battery.BATTERYPOWER>=0)//if these are false then it doesnt make any sense to send any messages
			{
				switch(this.nodeState)
				{
					case 1:
					{
						if(this.nodeType==true)
							this.nodeState=0;
						else
						{
							this.nodeState=4;
							//	else
							//		this.nodeState=1;
						}
						this.totalBitsTransmitted+=41;
						this.battery.BATTERYPOWER-=((50*0.000000001*41+100*0.000000000001*41*this.xmissionRange*this.xmissionRange)*this.simulationSpeed);
						str=this.sendJoinMessage();
						break;
					}
					case 2:
					{
						if(this.nodeType==false)
						{
							if(this.OLD_PARENT_MAC_X!=-100 && this.OLD_PARENT_MAC_Y!=-100)
								this.nodeState=3;
							else//if(this.receivedApplicationMessagesCount>=this.NUM_CHILDREN)
								this.nodeState=4;
							this.totalBitsTransmitted+=35;
							this.battery.BATTERYPOWER-=((50*0.000000001*35+100*0.000000000001*35*this.xmissionRange*this.xmissionRange)*this.simulationSpeed);
							str=this.sendJoiningMessage();
						}
						else
							this.nodeState=0;
						break;
					}
					case 3:
					{
						if(this.nodeType==false)
						{
							this.nodeState=4;
							this.totalBitsTransmitted+=35;
							this.battery.BATTERYPOWER-=((50*0.000000001*35+100*0.000000000001*35*this.xmissionRange*this.xmissionRange)*this.simulationSpeed);
							str=this.sendLeavingMessage();
						}
						else 
							this.nodeState=0;
						break;
					}
					case 4:
					{
						if(this.nodeType==false)
						{
							if((this.receivedApplicationMessagesCount>=this.NUM_CHILDREN) && (this.nodeState!=2 && this.nodeState!=3))
							{
								this.nodeState=1;
								str=this.sendApplicationMessage();
								if(str!=null)
								{
									String [] tok =str.Split(';');
									int bits=int.Parse(tok[5]);
									this.totalBitsTransmitted+=bits;
									this.MessageCycles++;
									this.battery.BATTERYPOWER-=((50*0.000000001*bits+100*0.000000000001*bits*this.xmissionRange*this.xmissionRange)*this.simulationSpeed);
								}
								this.receivedApplicationMessages=null;
								this.receivedApplicationMessagesCount=0;
							}
							else
								str=null;
						}
						else
							this.nodeState=0;
						break;
					}
				}
				return str;
			}
			else
				return null;
		}

		public float MAC_X
		{
			get
			{
				return mac_x;
			}
			set
			{
				mac_x=value;
			}
		}
		public float MAC_Y
		{
			get
			{
				return mac_y;
			}
			set
			{
				mac_y=value;
			}
		}
		public float OLD_PARENT_MAC_X
		{
			get
			{
				return old_p_mac_x;
			}
			set
			{
				old_p_mac_x=value;
			}
		}
		public float OLD_PARENT_MAC_Y
		{
			get
			{
				return old_p_mac_y;
			}
			set
			{
				old_p_mac_y=value;
			}
		}
		public float PARENT_MAC_X
		{
			get
			{
				return p_mac_x;
			}
			set
			{
				p_mac_x=value;
			}
		}
		public float PARENT_MAC_Y
		{
			get
			{
				return p_mac_y;
			}
			set
			{
				p_mac_y=value;
			}
		}
		public int HOP_COUNT
		{
			get
			{
				return hops;
			}
			set 
			{
				hops=value;
			}
		}
		public int NUM_CHILDREN
		{
			get
			{
				return children;
			}
			set 
			{
				children=value;
			}
		}
		public Image IMG
		{
			get
			{
				return img;
			}
			set
			{
				img=value;
			}
		}
		public double PATH_POWER
		{
			get
			{
				return this.PowerOfThePath;
			}
			set
			{
				this.PowerOfThePath=value;	
			}
		}
		public float XmissionRange
		{
			get
			{
				return this.xmissionRange;
			}
			set
			{
				this.xmissionRange=value;
			}

		}
		public SensorNode Next
		{
			get
			{
				return next;
			}
			set
			{
				next=value;
			}
		}
		public int NODENUM
		{
			get 
			{
				return this.nodeNum;
			}
			set
			{
				this.nodeNum=value;
			}
		}
		public bool JOINSTATUS
		{
			get
			{
				return this.joinStatus;
			}
			set
			{
				this.joinStatus=value;
			}
		}
		public int PIXEL_NUMBER_X
		{
			get
			{
				return this.pixel_number_x;
			}
			set 
			{
				this.pixel_number_x=value;
			}
		}
		public int PIXEL_NUMBER_Y
		{
			get
			{
				return this.pixel_number_y;
			}
			set
			{
				this.pixel_number_y=value;
			}
		}
		public int PARENT_PIXEL_NUMBER_X
		{
			get
			{
				return this.parent_pixel_number_x;
			}
			set
			{
				this.parent_pixel_number_x=value;
			}
		}
		public int PARENT_PIXEL_NUMBER_Y
		{
			get
			{
				return this.parent_pixel_number_y;
			}
			set
			{
				this.parent_pixel_number_y=value;
			}
		}
		public bool NODE_TYPE
		{
			get
			{
				return this.nodeType;
			}
			set
			{
				this.nodeType=value;
			}
		}
		public NodeBattery BATTERY
		{
			get
			{
				return this.battery;
			}
			set
			{
				this.battery=value;
			}
		}
		public int MESSAGECYCLES
		{
			get
			{
				return this.MessageCycles;
			}
			set
			{
				this.MessageCycles=value;
			}
		}
		public int TOTALBITSTRANSMITTED
		{
			get
			{
				return this.totalBitsTransmitted;
			}
			set
			{
				this.totalBitsTransmitted=value;
			}
		}
		public int TOTALBITSRECEIVED
		{
			get
			{
				return this.totalBitsReceived;
			}
			set
			{
				this.totalBitsReceived=value;
			}
		}
		public bool NEIGHBOURRECEIVEDXMITTED
		{
			get
			{
				return this.neighborReceivedXmitted;
			}
			set
			{
				this.neighborReceivedXmitted=value;
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
		public void resetNodeAttributes(bool TypeofNode)
		{
			this.joinStatus=false;
			this.nodeDead=false;//the node is alive
			this.nodeType=TypeofNode;
			this.children=0;
			this.nodeState=0;// the node is in the receiving mode.
			this.old_p_mac_x=-100;
			this.old_p_mac_y=-100;
			this.p_mac_x=40000;
			this.p_mac_y=40000;
			this.MessageCycles=0;
			this.totalBitsTransmitted=0;
			this.totalBitsReceived=0;
			this.childMacList=null;
			this.receivedApplicationMessages=null;
			this.receivedApplicationMessagesCount=0;
			if(TypeofNode==false)//ordinary node
			{
				this.img=ImageStream.FromFile("unDecidedNode.bmp");
				this.battery.BATTERYPOWER=0.5;
				this.nodeState=0;
				this.hops=10000;
				this.PowerOfThePath=0;
			}
			else//base station
			{
				this.img=ImageStream.FromFile("BaseStation.bmp");
				this.battery.BATTERYPOWER=10000;
				this.nodeState=1;
				this.hops=0;
				this.PowerOfThePath=1000;
			}
		}
	}
}