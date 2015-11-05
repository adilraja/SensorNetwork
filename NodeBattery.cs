using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SensorNetwork2
{
	/// <summary>
	/// Summary description for NodeBattery.
	/// </summary>
	public class NodeBattery : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ProgressBar progressBar1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private double batteryPower;
		public NodeBattery(double power)
		{
			//
			// Required for Windows Form Designer support
			//
			
			InitializeComponent();
			this.batteryPower=power;
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(8, 8);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(288, 23);
			this.progressBar1.TabIndex = 0;
			this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
			// 
			// NodeBattery
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(304, 40);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.progressBar1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.Name = "NodeBattery";
			this.ShowInTaskbar = false;
			this.Text = "NodeBattery";
			this.Load += new System.EventHandler(this.NodeBattery_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void NodeBattery_Load(object sender, System.EventArgs e)
		{
		
		}

		private void progressBar1_Click(object sender, System.EventArgs e)
		{
		
		}
	
		public double BATTERYPOWER
		{
			get
			{
				return this.batteryPower;
			}
			set
			{
				this.batteryPower=value;
			}
		}
	}
}
