using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SensorNetwork2
{
	/// <summary>
	/// Summary description for SensorNetwork.
	/// </summary>
	public class SensorNetwork : System.Windows.Forms.Form
	{
		private SensorNode headNode, tailNode,temp, temp1;
		private MessageList headMsg, tailMsg, tmpMsg;
		private ReceivedMessageList headRMsg, tmpRMsg, tailRMsg;
		private int numNodes, simulationSpeed, totMsgs, interNodeDistance=50;
		private float xmissionRange;
		private bool stopperFlag2;
		bool [] numbers, transmitMessageFlags;//numbers is for random number generation
		bool timerFlag, flag, hiddenNodeFlag, flag1, flag2;
		private Graphics oGraphics;
		private Bitmap DrawingArea;
		private Pen myPen, myLine;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel panel1;
		private System.Random rnd;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button1;
		private System.ComponentModel.IContainer components;
		public SensorNetwork(string str1, string str2, string str3)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.numNodes=int.Parse(str1);
			this.xmissionRange=float.Parse(str2);
			this.simulationSpeed=int.Parse(str3);
			rnd   = new Random();
			myPen = new Pen(Color.Blue);
			myLine=new Pen(Color.Gray);
			this.totMsgs=0;
			this.headNode=new SensorNode(true,this.xmissionRange,0, this.simulationSpeed);
			this.headRMsg=new ReceivedMessageList();
			this.temp=this.headNode;
			this.tmpRMsg=this.headRMsg;
			this.headMsg=new MessageList();//dummy node got to be present at all times.
			this.tailMsg=new MessageList();//-do-
			this.headMsg.NEXT=this.tailMsg;
			this.numbers=new bool[this.numNodes];
			this.timerFlag=false;
			this.stopperFlag2=false;
			for(int i=0;i<this.numNodes;i++)
			{
				this.tailNode=new SensorNode(false,this.xmissionRange,i+1,this.simulationSpeed);
				this.temp.Next=this.tailNode;
				this.temp=this.temp.Next;
				this.tailRMsg=new ReceivedMessageList();
				this.tmpRMsg.Next=this.tailRMsg;
				this.tmpRMsg=this.tmpRMsg.Next;
			}
		}
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.button1,
																					this.button7,
																					this.button6,
																					this.button5,
																					this.button4,
																					this.button3,
																					this.button2,
																					this.label1,
																					this.richTextBox1});
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(0, 600);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(790, 100);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "groupBox1";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(240, 64);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(96, 23);
			this.button1.TabIndex = 11;
			this.button1.Text = "Network Stats";
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(352, 64);
			this.button7.Name = "button7";
			this.button7.TabIndex = 10;
			this.button7.Text = "Back";
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(288, 24);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(136, 23);
			this.button6.TabIndex = 9;
			this.button6.Text = "Draw Random Topology";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(136, 64);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(96, 23);
			this.button5.TabIndex = 8;
			this.button5.Text = "Stop Simulation";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(8, 64);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(120, 23);
			this.button4.TabIndex = 7;
			this.button4.Text = "Start Simulation";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(136, 24);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(144, 23);
			this.button3.TabIndex = 6;
			this.button3.Text = "Draw Circular Topology";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(8, 24);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(120, 23);
			this.button2.TabIndex = 3;
			this.button2.Text = "Draw Grid Topology ";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(440, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Results";
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(432, 24);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(352, 64);
			this.richTextBox1.TabIndex = 1;
			this.richTextBox1.Text = "";
			// 
			// panel1
			// 
			this.panel1.AllowDrop = true;
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(790, 600);
			this.panel1.TabIndex = 1;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			// 
			// timer1
			// 
			this.timer1.Interval = 50;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// SensorNetwork
			// 
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(476, 116);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panel1,
																		  this.groupBox1});
			this.Name = "SensorNetwork";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SensorNetwork";
			this.Load += new System.EventHandler(this.SensorNetwork_Load);
			this.Closed += new System.EventHandler(this.SensorNetwork_Closed);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.SensorNetwork_Paint);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		private void SensorNetwork_Load(object sender, System.EventArgs e)
		{
			DrawingArea = new Bitmap(
				this.panel1.ClientRectangle.Width, 
				this.panel1.ClientRectangle.Height,
				System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			InitializeDrawingArea();
		}
		private void InitializeDrawingArea()
		{
			Graphics oGraphics;
			oGraphics = Graphics.FromImage(DrawingArea);
			this.myPen.Color = Color.AliceBlue;
			for ( int x = 0; x < this.panel1.Width; x++)
			{
				oGraphics.DrawLine(myPen, x, 0, x, this.panel1.Height);
			}
			oGraphics.Dispose();
      
			this.Invalidate();
		}
		private void button1_Click(object sender, System.EventArgs e)
		{
			int X, Y;
			Graphics oGraphics;
			oGraphics = Graphics.FromImage(DrawingArea);
			myPen.Color=Color.Green;
			oGraphics.DrawEllipse(
				myPen, 
				X=rnd.Next(0,this.panel1.Width-90),
				Y=rnd.Next(0,this.panel1.Height-90),
				90, 90);
	//		this.richTextBox1.Text+=X.ToString()+" "+Y.ToString()+"\n";
			oGraphics.Dispose();
			this.panel1.Invalidate();
		}

		private void SensorNetwork_Closed(object sender, System.EventArgs e)
		{
			DrawingArea.Dispose();
			System.Console.WriteLine("bye...");
			System.Console.Read();
		}

		private void SensorNetwork_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics oGraphics;

			oGraphics = e.Graphics;

			oGraphics.DrawImage(DrawingArea, 
				0, 0, 
				DrawingArea.Width, 
				DrawingArea.Height);
			oGraphics.Dispose();
		}

		private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics oGraphics;
			oGraphics = e.Graphics;
			oGraphics.DrawImage(DrawingArea, 
				0, 0, 
				DrawingArea.Width, 
				DrawingArea.Height);
		//	oGraphics.DrawImage(sensors[0].IMG,90,90);
			oGraphics.Dispose();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			SensorNode tmp;
			Graphics oGraphics;
			oGraphics = Graphics.FromImage(DrawingArea);
			oGraphics.Clear(Color.AliceBlue);
			tmp=this.headNode;
			this.tmpRMsg=this.headRMsg;
			this.headNode.resetNodeAttributes(true);
			int X=(int)((float)this.interNodeDistance*0.7), Y=(int)((float)this.interNodeDistance*0.7);
			int j=1;
			for(int i=0;i<this.numNodes;i++)
			{
				if(X>=(this.panel1.Width-2*this.interNodeDistance))
				{
					j=1;
					Y=Y+(int)((float)this.interNodeDistance*0.9);
				}
				oGraphics.DrawImage(tmp.IMG,X=j*(int)((float)this.interNodeDistance*0.9),Y,7,7);
	//			if(tmp==this.headNode)
	//				tmp.resetNodeAttributes(true);
	//			else
				tmp.PIXEL_NUMBER_X=X;
				tmp.PIXEL_NUMBER_Y=Y;
				tmp.MAC_X=((float)tmp.PIXEL_NUMBER_X*(this.xmissionRange/(float)this.interNodeDistance));
				tmp.MAC_Y=((float)tmp.PIXEL_NUMBER_Y*(this.xmissionRange/(float)this.interNodeDistance));
				this.tmpRMsg.MAC_X=tmp.MAC_X;
				this.tmpRMsg.MAC_Y=tmp.MAC_Y;
				this.tmpRMsg.NODERECEIVEDMESSAGE=false;
		/*		this.richTextBox1.Text+=tmp.MAC_X.ToString();
				this.richTextBox1.Text+=";";
				this.richTextBox1.Text+=this.tmpRMsg.MAC_X.ToString();
				this.richTextBox1.Text+=":";
				this.richTextBox1.Text+=tmp.MAC_Y.ToString();
				this.richTextBox1.Text+=";";
				this.richTextBox1.Text+=this.tmpRMsg.MAC_Y.ToString();
				this.richTextBox1.Text+="\n";*/
				tmp=tmp.Next;
				this.tmpRMsg=this.tmpRMsg.Next;
				tmp.resetNodeAttributes(false);
				j++;
			}
			oGraphics.Dispose();
			this.panel1.Invalidate();
		}
		private void button4_Click(object sender, System.EventArgs e)
		{
			this.timer1.Start();
		}
		private void button3_Click(object sender, System.EventArgs e)
		{
			SensorNode tmp;
			Graphics oGraphics;
			oGraphics = Graphics.FromImage(DrawingArea);
			oGraphics.Clear(Color.AliceBlue);
			tmp=this.headNode;
			this.headNode.resetNodeAttributes(true);
			int Xi=(int)(this.interNodeDistance*0.9), Y=(int)(this.interNodeDistance*0.9)/2;
			int Xj=(int)Math.Sqrt(3*Xi*Xi/4)*2;//		/|\
			int X=Xi;						   //	   / | \
			double j=1.5;					   //	  /__|__\
			for(int i=0;i<this.numNodes;i++)   //
			{
				if(X>=(this.panel1.Width-2*(int)this.interNodeDistance))
				{
					if(j%1==0)
						j=1.5;
					else
						j=1;
					Y=Y+(int)(this.interNodeDistance*0.9)/2;
				}
				oGraphics.DrawImage(tmp.IMG,X=(int)(j*(double)Xj),Y,7,7);
//				if(tmp==this.headNode)
//					tmp.resetNodeAttributes(true);
//				else
				tmp.PIXEL_NUMBER_X=X;
				tmp.PIXEL_NUMBER_Y=Y;
				tmp.MAC_X=((float)tmp.PIXEL_NUMBER_X*(this.xmissionRange/(float)this.interNodeDistance));
				tmp.MAC_Y=((float)tmp.PIXEL_NUMBER_Y*(this.xmissionRange/(float)this.interNodeDistance));
				tmp=tmp.Next;
				tmp.resetNodeAttributes(false);
				j=j+1;
			}
			oGraphics.Dispose();
			this.panel1.Invalidate();
		}
		public MessageList move_to_Message(int k)
		{
						int i; 
						MessageList tmp = this.headMsg; 
						for (i = 0; i < k;i++) 
						{ 
							tmp = tmp.NEXT; 
						} 
						return tmp; 
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			this.oGraphics = Graphics.FromImage(this.DrawingArea);
			this.temp=this.headNode;
			this.oGraphics.Clear(Color.AliceBlue);
			bool stopperFlag=false;
			for(int i=0;i<this.numNodes;i++)
			{
				this.temp.bitmapUPDater();
				oGraphics.DrawImage(this.temp.IMG,this.temp.PIXEL_NUMBER_X,this.temp.PIXEL_NUMBER_Y,7,7);
				if(this.temp.JOINSTATUS==true)
				{
					oGraphics.DrawLine(myLine,this.temp.PIXEL_NUMBER_X,this.temp.PIXEL_NUMBER_Y,this.temp.PARENT_PIXEL_NUMBER_X,this.temp.PARENT_PIXEL_NUMBER_Y);//ammend here
				}
				if(this.temp.BATTERY.BATTERYPOWER<=0)
				{
					this.NetworkStats();
					stopperFlag=true;
				}
				this.temp=this.temp.Next;
			}
			if(stopperFlag==true && this.stopperFlag2==false)
			{
				this.timer1.Stop();
				this.stopperFlag2=true;
			}
			Random rnd=new Random();
			for(int j=0;j<this.numNodes;j++)
				this.numbers[j]=false;
			for(int j=0;j<this.numNodes;j++)
			{
				int num=0;
				for(int k=0;k<this.numNodes;k++)
				{
			
					num=rnd.Next(0,this.numNodes);
					if(this.numbers[num]==false)
					{
						this.numbers[num]=true;
						break;
					}
					else
						k--;
				}
				this.temp=this.headNode;
				for(int k=0;k<num;k++)
					this.temp=this.temp.Next;
				float Xsq, Ysq, distSq;
				int msgCount=1;
				this.flag=false;//false for no message received
				this.hiddenNodeFlag=false;//true for no hidden node problem
				int delMsgs=0;
				this.tmpMsg=this.headMsg.NEXT;
				String msg=null;
				for(int k=0;k<this.totMsgs;k++)
				{
					String [] msgListToks=this.tmpMsg.MESSAGE.Split(';');
					Xsq=this.temp.MAC_X-float.Parse(msgListToks[0]);
					Xsq=Xsq*Xsq;
					Ysq=this.temp.MAC_Y-float.Parse(msgListToks[1]);
		//			msgListToks=null;
					Ysq=Ysq*Ysq;
					distSq=Xsq+Ysq;
					if(this.tmpMsg.ITERATION>=this.numNodes)
					{
						MessageList MsgToBeRemoved=this.move_to_Message(msgCount);
						this.move_to_Message(msgCount-1).NEXT=this.move_to_Message(msgCount+1);
						this.temp1=this.headNode;
						for(int m=0;m<this.numNodes;m++)
						{
				//			if(this.temp.MAC_X!=this.temp1.MAC_X || this.temp.MAC_Y!=this.temp1.MAC_Y)
				//			{
								Xsq=this.temp1.MAC_X-float.Parse(msgListToks[0]);
								Xsq=Xsq*Xsq;
								Ysq=this.temp1.MAC_Y-float.Parse(msgListToks[1]);
							//	msgListToks=null;
								Ysq=Ysq*Ysq;
								distSq=Xsq+Ysq;
								if((distSq<=this.xmissionRange*this.xmissionRange) )
								{
									SensorNode tmp=this.headNode;
									for(int n=0;n<this.numNodes;n++)
									{
                                        float x, y, hyp;
										x=this.temp1.MAC_X-tmp.MAC_X;
										x=x*x;
										y=this.temp1.MAC_Y-tmp.MAC_Y;
										y=y*y;
										hyp=x+y;
										if(hyp<=this.xmissionRange*this.xmissionRange)
										{
											if(tmp.ITERATION>=this.numNodes)
											{
												this.richTextBox1.Text+="I come here\n";
												tmp.NEIGHBOURRECEIVEDXMITTED=false;
												tmp.ITERATION=0;
											}
										}
										tmp=tmp.Next;
									}
									if(this.temp1.ITERATION>=this.numNodes)
									{
										this.temp1.NEIGHBOURRECEIVEDXMITTED=false;
										this.temp1.ITERATION=0;
									}
								}
							this.temp1=this.temp1.Next;
						}
						MsgToBeRemoved=null;
						delMsgs++;
						msgCount--;
					}
					//the value used for msg deletion
					else if(distSq<=this.xmissionRange*this.xmissionRange)
					{
						this.temp.MessageReceiver(this.tmpMsg.MESSAGE);
						this.temp1=this.headNode;
						for(int m=0;m<this.numNodes;m++)
						{
				//			if((this.temp.MAC_X!=this.temp1.MAC_X || this.temp.MAC_Y!=this.temp1.MAC_Y) && (this.temp1.NEIGHBOURRECEIVEDXMITTED==true))
				//			{
								Xsq=this.temp.MAC_X-this.temp1.MAC_X;
								Xsq=Xsq*Xsq;
								Ysq=this.temp.MAC_Y-this.temp1.MAC_Y;
								Ysq=Ysq*Ysq;
								distSq=Xsq+Ysq;
								if((distSq<=this.xmissionRange*this.xmissionRange) )
								{
									this.temp1.NEIGHBOURRECEIVEDXMITTED=true;//a neighbour has received a message
									this.temp1.ITERATION=0;
									//								this.richTextBox1.Text+="haan jee\n";					
								}
				//			}
							this.temp1=this.temp1.Next;
						}
						flag=true;//the node received a message
					}
					msgCount++;
					this.tmpMsg=this.tmpMsg.NEXT;
				}
				this.tmpMsg=this.headMsg.NEXT;
				this.tailMsg.ITERATION=0;
				while(this.tmpMsg.NEXT!=null)
				{
					this.tmpMsg.ITERATION++;
					this.tmpMsg=this.tmpMsg.NEXT;
				}
				this.tailMsg.ITERATION=0;
				this.temp1=this.headNode;
				for(int i=0;i<this.numNodes;i++)
				{
					if(this.temp1.NEIGHBOURRECEIVEDXMITTED==true)
						this.temp1.ITERATION++;
					if(this.temp1.ITERATION>=this.numNodes)
					{
						this.temp1.ITERATION=0;
						this.temp1.NEIGHBOURRECEIVEDXMITTED=false;
					}
					this.temp1=this.temp1.Next;
				}
				//if else with a flag
				if(flag==false)
				{
					bool xmitterFlag=false;
					this.temp1=this.headNode;
					for(int m=0;m<this.numNodes;m++)
					{
						if((this.temp1.NEIGHBOURRECEIVEDXMITTED==true))
						{
							Xsq=this.temp.MAC_X-this.temp1.MAC_X;
							Xsq=Xsq*Xsq;
							Ysq=this.temp.MAC_Y-this.temp1.MAC_Y;
							Ysq=Ysq*Ysq;
							distSq=Xsq+Ysq;
								
							if((distSq<=this.xmissionRange*this.xmissionRange) )
							{
								xmitterFlag=true;//a neighbour has received a message
//								this.richTextBox1.Text+="haan jee\n";					
							}
						}
						this.temp1=this.temp1.Next;
					}
					if(xmitterFlag==false)
					{
						String str;
						if((str=this.temp.MessageTransmitter())!=null)
						{
							String [] toks=str.Split(';');
							if(int.Parse(toks[4])==1)
								myPen.Color=Color.Red;
							else if(int.Parse(toks[4])==2)
								myPen.Color=Color.Green;
							else if(int.Parse(toks[4])==3)
								myPen.Color=Color.Blue;
							else 
								myPen.Color=Color.Purple;
							MessageList NewMsg=new MessageList();
							NewMsg.MESSAGE=str;
							NewMsg.NEXT=this.headMsg.NEXT;
							this.headMsg.NEXT=NewMsg;
							this.totMsgs++;
							oGraphics.DrawEllipse(myPen, this.temp.PIXEL_NUMBER_X-this.interNodeDistance, this.temp.PIXEL_NUMBER_Y-this.interNodeDistance, this.interNodeDistance*2, this.interNodeDistance*2);
							this.temp1=this.headNode;
							for(int m=0;m<this.numNodes;m++)
							{
								if((this.temp.MAC_X!=this.temp1.MAC_X || this.temp.MAC_Y!=this.temp1.MAC_Y) )
								{
									Xsq=this.temp.MAC_X-this.temp1.MAC_X;
									Xsq=Xsq*Xsq;
									Ysq=this.temp.MAC_Y-this.temp1.MAC_Y;
									Ysq=Ysq*Ysq;
									distSq=Xsq+Ysq;
									if((distSq<=this.xmissionRange*this.xmissionRange) )
									{
										this.temp1.NEIGHBOURRECEIVEDXMITTED=true;
										this.temp1.ITERATION=0;
									}
								}
								this.temp1=this.temp1.Next;
							}
						}
					}
				}
				this.totMsgs=this.totMsgs-delMsgs;
			}
			rnd=null;
			oGraphics.Dispose();
			this.panel1.Invalidate();				
		}
		private void button5_Click(object sender, System.EventArgs e)
		{
			SensorNode tmp=this.headNode;
			this.timer1.Stop();
			for(int i=0;i<this.numNodes;i++)
			{
				this.richTextBox1.Text+=tmp.NODENUM+";"+tmp.NUM_CHILDREN+"\n";
				tmp=tmp.Next;
			}
		}
		private void trackBar1_Scroll(object sender, System.EventArgs e)
		{
		
		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			Graphics oGraphics;
			oGraphics = Graphics.FromImage(DrawingArea);
			myPen.Color=Color.Red;
			myLine.Color=Color.Gray;
			//	this.tailMsg=this.headMsg;
			this.temp=this.headNode;
			oGraphics.Clear(Color.AliceBlue);
	//		this.richTextBox1.Text+=totMsgs.ToString();
	//		this.richTextBox1.Text+="\n";
			for(int i=0;i<this.numNodes;i++)
			{
				this.temp.PIXEL_NUMBER_X=0;
				this.temp.PIXEL_NUMBER_Y=0;
				this.temp.PARENT_PIXEL_NUMBER_X=0;
				this.temp.PARENT_PIXEL_NUMBER_Y=0;
				this.temp=this.temp.Next;
			}
			this.headNode.resetNodeAttributes(true);
			this.temp=this.headNode.Next;
			Random rnd=new Random();
			this.headNode.PIXEL_NUMBER_X=rnd.Next(this.interNodeDistance, this.panel1.Width-2*this.interNodeDistance);
			this.headNode.PIXEL_NUMBER_Y=rnd.Next(this.interNodeDistance, this.panel1.Height-2*this.interNodeDistance);
			this.headNode.MAC_X=((float)this.headNode.PIXEL_NUMBER_X*(this.xmissionRange/(float)this.interNodeDistance));
			this.headNode.MAC_Y=((float)this.headNode.PIXEL_NUMBER_Y*(this.xmissionRange/(float)this.interNodeDistance));
			oGraphics.DrawImage(this.headNode.IMG,this.headNode.PIXEL_NUMBER_X, this.headNode.PIXEL_NUMBER_Y,7,7);
			for(int i=0;i<this.numNodes-1;i++)
			{
				int X1, Y1, X2, Y2, dist;
				bool flag1=true, flag2=false;//
				SensorNode tmp=this.headNode;
				X2=rnd.Next(this.interNodeDistance,this.panel1.Width-2*this.interNodeDistance);
				Y2=rnd.Next(this.interNodeDistance,this.panel1.Height-2*this.interNodeDistance);
				for(int j=0;j<this.numNodes;j++)
				{
					if(tmp.PIXEL_NUMBER_X!=0 && tmp.PIXEL_NUMBER_Y!=0)
					{
						X1=tmp.PIXEL_NUMBER_X;
						Y1=tmp.PIXEL_NUMBER_Y;
						dist=(int)Math.Sqrt((double)(((X1-X2)*(X1-X2))+((Y1-Y2)*(Y1-Y2))));
						if(dist<this.interNodeDistance*0.7)
							flag1=false;//means that the node is too close
						if(dist<this.interNodeDistance*0.9)
							flag2=true;//means that the node is within the range of atleast one other node
					}
					tmp=tmp.Next;
				}
				if(flag1==true && flag2==true)
				{
					oGraphics.DrawImage(this.temp.IMG,X2, Y2,7,7);
					this.temp.resetNodeAttributes(false);
					this.temp.PIXEL_NUMBER_X=X2;
					this.temp.PIXEL_NUMBER_Y=Y2;
					this.temp.MAC_X=((float)this.temp.PIXEL_NUMBER_X*(this.xmissionRange/(float)this.interNodeDistance));
					this.temp.MAC_Y=((float)this.temp.PIXEL_NUMBER_Y*(this.xmissionRange/(float)this.interNodeDistance));
					this.temp=this.temp.Next;
				}
				else
					i--;//random number not appropriate so revert back
			}
			oGraphics.Dispose();
			this.panel1.Invalidate();
		}
		private void NetworkStats()
		{
			SensorNode tmp=this.headNode;
			int totCycles=0, totBitsXmitted=0, totBitsReceived=0;
			double dissipatedPower=0.0;
//			this.richTextBox1=true;
			this.richTextBox1.Text="nodeNum;  Cycles;\t\tBits Transmitted;\tBits Received\n";
//			this.richTextBox1.Font.Bold=false;
			for(int i=0;i<this.numNodes;i++)
			{
				int X, Y, Z;
				X=tmp.MESSAGECYCLES*this.simulationSpeed;
				if(i>0)
					dissipatedPower=dissipatedPower+(0.5-tmp.battery.BATTERYPOWER);
				Y=tmp.TOTALBITSTRANSMITTED*this.simulationSpeed;
				Z=tmp.TOTALBITSRECEIVED*this.simulationSpeed;
				this.richTextBox1.Text+=tmp.NODENUM+";\t  "+X.ToString()+";\t\t"+Y.ToString()+";\t\t"+Z.ToString()+"\n";
				totCycles+=X;
				totBitsXmitted+=Y;
				totBitsReceived+=Z;
				tmp=tmp.Next;
			}
//			this.richTextBox1.Font.Bold=true;
			this.richTextBox1.Text+="Total Cycles;\tBits Transmitted;\tBits Received\n";
//			this.richTextBox1.Font.Bold=false;
			this.richTextBox1.Text+=totCycles.ToString()+";\t\t"+totBitsXmitted.ToString()+";\t\t"+totBitsReceived+"\n";
			this.richTextBox1.Text+="total system energy dissipated = "+dissipatedPower.ToString()+" Joules\n";
		}
		private void button7_Click(object sender, System.EventArgs e)
		{
			Form1.frmMain.Show();
		//	this.Close();
			this.Dispose(true);
		}

		private void button1_Click_1(object sender, System.EventArgs e)
		{
			this.timer1.Stop();
			this.NetworkStats();
		}
	}
}
