using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = "Diagnostic initialized";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String p  = "COM17";
            SerialPort port = new SerialPort(p,9600, Parity.None, 8, StopBits.One);
            Boolean portExists = false;
            try
            {
                var all_ports = SerialPort.GetPortNames();
                foreach( String p_name in all_ports ){
                    Console.WriteLine(p_name);
                    if (p_name.Contains("17"))
                    {
                        label1.Text = "Port COM17 discovered";
                        portExists = true;
                    }
                }

                if (!portExists)
                {
                    label1.Text = "Error : Port COM17 not found. Please check Device Manager.";
                    return;
                }

                label1.Text = "Checking if " + p + " is already open";
                if (port.IsOpen == true)
                {
                    label1.Text = "Closing port " + p;
                    port.Close();                                    
                }
                label1.Text = "Opening Port " + p;
                port.Open();
                port.Write("1");
                port.Close();
                label1.Text = "Successfully wrote to Photon on COM17 ";
            } 
            catch (Exception exception)
            {
                Console.WriteLine("Error accessing com17 port " + exception.ToString());
                label1.Text = exception.ToString();
            }          

        }
    }
}
