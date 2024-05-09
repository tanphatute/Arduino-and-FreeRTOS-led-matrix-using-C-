using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace ComputerToArduino
{
    public partial class Form1 : Form

    {
        bool isConnected = false;
        String[] ports;
        SerialPort port;

        public Form1()
        {
            InitializeComponent();
            disableControls();
            getAvailableComPorts();

            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
                Console.WriteLine(port);
                if (ports[0] != null)
                {
                    comboBox1.SelectedItem = ports[0];
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                connectToArduino();
            } else
            {
                disconnectFromArduino();
            }
        }

        void getAvailableComPorts()
        {
            ports = SerialPort.GetPortNames();
        }

        private void connectToArduino()
        {
            isConnected = true;
            string selectedPort = comboBox1.GetItemText(comboBox1.SelectedItem);
            port = new SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One);
            port.Open();
            port.Write("#");
            button1.Text = "Disconnect";
            enableControls();
        }


        private void disconnectFromArduino()
        {
            isConnected = false;
            port.Close();
            button1.Text = "Connect";
            disableControls();
            resetDefaults();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                port.Write("#TEXT");
            }
        }

        private void enableControls()
        {
            
            button2.Enabled = true;
            textBox1.Enabled = true;
            groupBox3.Enabled = true;

        }

        private void disableControls()
        {
            
            button2.Enabled = false;
            textBox1.Enabled = false;
            groupBox3.Enabled = false;
        }

        private void resetDefaults()
        {          
            textBox1.Text = "";
            
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
