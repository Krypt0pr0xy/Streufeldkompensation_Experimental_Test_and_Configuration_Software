#define release

using System;
using System.Windows.Forms;
using System.IO;
using NationalInstruments.Visa;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using System.Diagnostics;
using System.Linq;

namespace TEST_Software_V1
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>


    public class MainForm : System.Windows.Forms.Form
    {

        #region Init
        private MessageBasedSession mbSession;
        private string lastResourceString = null;
        private System.Windows.Forms.Button OpenSessionButton;
        private System.Windows.Forms.Button CloseSessionButton;
        private System.ComponentModel.IContainer components;
        private Button BT_start;
        private Button BT_stop;
        private Label label2;
        private NumericUpDown NUP_interval;
        private Label label3;
        private Label l_counts;
        private Label label4;
        private Label label5;
        private Label label6;
        private NumericUpDown NUP_start;
        private NumericUpDown NUP_stop;
        private NumericUpDown NUP_increment;
        private Label label7;
        private Label l_current_value_should;

        private System.Int64 counter = 0;
        private double should_value = 0.0;
        private readonly double[] Value_array = new double[100000];
        private Button BT_write_file;
        private Label label8;
        private Label l_current_value_is;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private Button BT_help;
        private Label label1;
        private NumericUpDown NUD_V;
        private Button BT_send;
        private RadioButton RB_10V;
        private RadioButton RB_1V;
        private Label label10;
        private NumericUpDown NUD_CH;
        private TextBox textbox;
        private Label label11;
        private ComboBox CB_Port;
        private Button BT_close;
        private Button BT_open;
        private System.Int64 Value_array_counter = 0;
        private System.IO.Ports.SerialPort sport;

        public static bool sport_connected = false;//bool for connection of the Serial port
        public static bool mesure_connected = false;
        private System.Windows.Forms.Timer Timer_Update_UART;
        private Label label14;
        private Label l_difference;
        private Button BT_save;
        private Label label15;
        private Label l_Version;
        private Label label12;
        private Label label13;
        private Label l_slope;
        private Label l_offset;
        private Button BT_saveAB;
        private Label label18;
        private Label label19;
        private System.Windows.Forms.Timer Timer_Trigger;
        private double AlphaA_Value = 0;
        private Label l_finished;
        private NumericUpDown NUD_multi;
        private Label label9;
        private RadioButton RB_out_res_low;
        private RadioButton RB_out_res_high;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button BT_Mesure_read;
        private Label l_mesure_read;
        private NumericUpDown NUP_counts_set;
        private Label label16;
        private Label label20;
        private Label l_INL_absolut;
        private Label label21;
        private Label L_gainerror;
        private Label label22;
        private Label label23;
        private double AlphaB_Value = 0;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        private GroupBox groupBox7;
        private GroupBox groupBox8;
        private GroupBox groupBox9;
        private bool timer_trigger_need_stop = false;

        #endregion
        public MainForm()
        {
            InitializeComponent();//Init all Components


            //OpenSessionButton.Enabled = true;
            //CloseSessionButton.Enabled = false;

            l_Version.Text = "Perpetual Beta Developer Build V4: C. Wyler ETH";//Version Name and Build Version
            this.Text = this.l_Version.Text;//set Title same as Version label
            //Title title = chart1.Titles.Add("Skala");//Title from chart
            Update_chart();//Update Chart
            BT_start.Enabled = true;//Enable Start button on beginning
            BT_stop.Enabled = false;//Disable Stop button on Beginning
            //add for each avaible com port a Item
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())//Search for new com port names
            {
                CB_Port.Items.Add(s);//Add Item to th Port list
            }

            BT_send.Enabled = false;//Diasble send Button
            
            BT_close.Enabled = false;//Diasble Close Button

            Timer_Trigger.Interval = (int)NUP_interval.Value;//set interval off Tmer to the Numeric Up Down


        }
        //###########################################################################################################################################################################################################
        //###########################################################################################################################################################################################################
        //                                      FRONTEND
        //###########################################################################################################################################################################################################
        //###########################################################################################################################################################################################################


        //Implementing the Dispose method is primarily for releasing unmanaged resources. 
        protected override void Dispose(bool disposing)
        {
            if (disposing)//Check if Bool is set
            {
                if (mbSession != null)//chech if Session exists 
                {
                    mbSession.Dispose();
                }
                if (components != null)//chech if Session exists 
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.OpenSessionButton = new System.Windows.Forms.Button();
            this.CloseSessionButton = new System.Windows.Forms.Button();
            this.BT_start = new System.Windows.Forms.Button();
            this.BT_stop = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.NUP_interval = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.l_counts = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.NUP_start = new System.Windows.Forms.NumericUpDown();
            this.NUP_stop = new System.Windows.Forms.NumericUpDown();
            this.NUP_increment = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.l_current_value_should = new System.Windows.Forms.Label();
            this.BT_write_file = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.l_current_value_is = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.BT_help = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.NUD_V = new System.Windows.Forms.NumericUpDown();
            this.BT_send = new System.Windows.Forms.Button();
            this.RB_10V = new System.Windows.Forms.RadioButton();
            this.RB_1V = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.NUD_CH = new System.Windows.Forms.NumericUpDown();
            this.textbox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.CB_Port = new System.Windows.Forms.ComboBox();
            this.BT_close = new System.Windows.Forms.Button();
            this.BT_open = new System.Windows.Forms.Button();
            this.sport = new System.IO.Ports.SerialPort(this.components);
            this.Timer_Update_UART = new System.Windows.Forms.Timer(this.components);
            this.Timer_Trigger = new System.Windows.Forms.Timer(this.components);
            this.label14 = new System.Windows.Forms.Label();
            this.l_difference = new System.Windows.Forms.Label();
            this.BT_save = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.l_Version = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.l_slope = new System.Windows.Forms.Label();
            this.l_offset = new System.Windows.Forms.Label();
            this.BT_saveAB = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.l_finished = new System.Windows.Forms.Label();
            this.NUD_multi = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.RB_out_res_low = new System.Windows.Forms.RadioButton();
            this.RB_out_res_high = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BT_Mesure_read = new System.Windows.Forms.Button();
            this.l_mesure_read = new System.Windows.Forms.Label();
            this.NUP_counts_set = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.l_INL_absolut = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.L_gainerror = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.NUP_interval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUP_start)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUP_stop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUP_increment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_V)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_CH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_multi)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUP_counts_set)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpenSessionButton
            // 
            this.OpenSessionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpenSessionButton.Location = new System.Drawing.Point(122, 36);
            this.OpenSessionButton.Name = "OpenSessionButton";
            this.OpenSessionButton.Size = new System.Drawing.Size(96, 26);
            this.OpenSessionButton.TabIndex = 0;
            this.OpenSessionButton.Text = "Open";
            this.OpenSessionButton.UseVisualStyleBackColor = true;
            this.OpenSessionButton.Click += new System.EventHandler(this.OpenSession_Click);
            // 
            // CloseSessionButton
            // 
            this.CloseSessionButton.Enabled = false;
            this.CloseSessionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseSessionButton.Location = new System.Drawing.Point(224, 36);
            this.CloseSessionButton.Name = "CloseSessionButton";
            this.CloseSessionButton.Size = new System.Drawing.Size(96, 26);
            this.CloseSessionButton.TabIndex = 1;
            this.CloseSessionButton.Text = "Close";
            this.CloseSessionButton.UseVisualStyleBackColor = true;
            this.CloseSessionButton.Click += new System.EventHandler(this.CloseSession_Click);
            // 
            // BT_start
            // 
            this.BT_start.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_start.Location = new System.Drawing.Point(180, 21);
            this.BT_start.Name = "BT_start";
            this.BT_start.Size = new System.Drawing.Size(88, 23);
            this.BT_start.TabIndex = 14;
            this.BT_start.Text = "Start";
            this.BT_start.UseVisualStyleBackColor = true;
            this.BT_start.Click += new System.EventHandler(this.BT_start_Click);
            // 
            // BT_stop
            // 
            this.BT_stop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_stop.Location = new System.Drawing.Point(291, 21);
            this.BT_stop.Name = "BT_stop";
            this.BT_stop.Size = new System.Drawing.Size(88, 23);
            this.BT_stop.TabIndex = 15;
            this.BT_stop.Text = "Stop";
            this.BT_stop.UseVisualStyleBackColor = true;
            this.BT_stop.Click += new System.EventHandler(this.BT_stop_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Interval [ms]:";
            // 
            // NUP_interval
            // 
            this.NUP_interval.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NUP_interval.Location = new System.Drawing.Point(80, 24);
            this.NUP_interval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.NUP_interval.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NUP_interval.Name = "NUP_interval";
            this.NUP_interval.Size = new System.Drawing.Size(75, 22);
            this.NUP_interval.TabIndex = 18;
            this.NUP_interval.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NUP_interval.ValueChanged += new System.EventHandler(this.NUP_interval_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "counts now:";
            // 
            // l_counts
            // 
            this.l_counts.AutoSize = true;
            this.l_counts.Location = new System.Drawing.Point(77, 63);
            this.l_counts.Name = "l_counts";
            this.l_counts.Size = new System.Drawing.Size(13, 13);
            this.l_counts.TabIndex = 20;
            this.l_counts.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "start Value:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "stop Value:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Increment:";
            // 
            // NUP_start
            // 
            this.NUP_start.DecimalPlaces = 6;
            this.NUP_start.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NUP_start.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.NUP_start.Location = new System.Drawing.Point(95, 13);
            this.NUP_start.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.NUP_start.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.NUP_start.Name = "NUP_start";
            this.NUP_start.Size = new System.Drawing.Size(140, 22);
            this.NUP_start.TabIndex = 24;
            this.NUP_start.Value = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.NUP_start.ValueChanged += new System.EventHandler(this.NUP_start_ValueChanged);
            // 
            // NUP_stop
            // 
            this.NUP_stop.DecimalPlaces = 6;
            this.NUP_stop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NUP_stop.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.NUP_stop.Location = new System.Drawing.Point(95, 42);
            this.NUP_stop.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.NUP_stop.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.NUP_stop.Name = "NUP_stop";
            this.NUP_stop.Size = new System.Drawing.Size(140, 22);
            this.NUP_stop.TabIndex = 25;
            this.NUP_stop.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // NUP_increment
            // 
            this.NUP_increment.DecimalPlaces = 6;
            this.NUP_increment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NUP_increment.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.NUP_increment.Location = new System.Drawing.Point(95, 68);
            this.NUP_increment.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.NUP_increment.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            196608});
            this.NUP_increment.Name = "NUP_increment";
            this.NUP_increment.Size = new System.Drawing.Size(140, 22);
            this.NUP_increment.TabIndex = 26;
            this.NUP_increment.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.NUP_increment.ValueChanged += new System.EventHandler(this.NUP_increment_ValueChanged);
            this.NUP_increment.Scroll += new System.Windows.Forms.ScrollEventHandler(this.NUP_increment_ValueChanged);
            this.NUP_increment.TabIndexChanged += new System.EventHandler(this.NUP_increment_ValueChanged);
            this.NUP_increment.VisibleChanged += new System.EventHandler(this.NUP_increment_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(239, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Current Value Should:";
            // 
            // l_current_value_should
            // 
            this.l_current_value_should.AutoSize = true;
            this.l_current_value_should.Location = new System.Drawing.Point(353, 17);
            this.l_current_value_should.Name = "l_current_value_should";
            this.l_current_value_should.Size = new System.Drawing.Size(13, 13);
            this.l_current_value_should.TabIndex = 28;
            this.l_current_value_should.Text = "0";
            // 
            // BT_write_file
            // 
            this.BT_write_file.Enabled = false;
            this.BT_write_file.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_write_file.Location = new System.Drawing.Point(16, 140);
            this.BT_write_file.Name = "BT_write_file";
            this.BT_write_file.Size = new System.Drawing.Size(175, 23);
            this.BT_write_file.TabIndex = 29;
            this.BT_write_file.Text = "write data to File";
            this.BT_write_file.UseVisualStyleBackColor = true;
            this.BT_write_file.Click += new System.EventHandler(this.BT_write_file_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(242, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "Current Value Is:";
            // 
            // l_current_value_is
            // 
            this.l_current_value_is.AutoSize = true;
            this.l_current_value_is.Location = new System.Drawing.Point(353, 46);
            this.l_current_value_is.Name = "l_current_value_is";
            this.l_current_value_is.Size = new System.Drawing.Size(13, 13);
            this.l_current_value_is.TabIndex = 31;
            this.l_current_value_is.Text = "0";
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(430, 249);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(738, 462);
            this.chart1.TabIndex = 32;
            this.chart1.Text = "chart1";
            // 
            // BT_help
            // 
            this.BT_help.Location = new System.Drawing.Point(418, 52);
            this.BT_help.Name = "BT_help";
            this.BT_help.Size = new System.Drawing.Size(51, 24);
            this.BT_help.TabIndex = 67;
            this.BT_help.Text = "Help";
            this.BT_help.UseVisualStyleBackColor = true;
            this.BT_help.Click += new System.EventHandler(this.B_help_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(265, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 66;
            this.label1.Text = "V:";
            // 
            // NUD_V
            // 
            this.NUD_V.DecimalPlaces = 8;
            this.NUD_V.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.NUD_V.Location = new System.Drawing.Point(288, 31);
            this.NUD_V.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUD_V.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.NUD_V.Name = "NUD_V";
            this.NUD_V.Size = new System.Drawing.Size(91, 20);
            this.NUD_V.TabIndex = 65;
            this.NUD_V.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUD_V.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NUD_V_KeyPress);
            // 
            // BT_send
            // 
            this.BT_send.Location = new System.Drawing.Point(393, 19);
            this.BT_send.Name = "BT_send";
            this.BT_send.Size = new System.Drawing.Size(76, 42);
            this.BT_send.TabIndex = 64;
            this.BT_send.Text = "Send";
            this.BT_send.UseVisualStyleBackColor = true;
            this.BT_send.Click += new System.EventHandler(this.BT_send_Click);
            // 
            // RB_10V
            // 
            this.RB_10V.AutoSize = true;
            this.RB_10V.Location = new System.Drawing.Point(5, 37);
            this.RB_10V.Name = "RB_10V";
            this.RB_10V.Size = new System.Drawing.Size(61, 17);
            this.RB_10V.TabIndex = 63;
            this.RB_10V.Text = "+/- 10V";
            this.RB_10V.UseVisualStyleBackColor = true;
            this.RB_10V.CheckedChanged += new System.EventHandler(this.RB_10V_CheckedChanged);
            // 
            // RB_1V
            // 
            this.RB_1V.AutoSize = true;
            this.RB_1V.Checked = true;
            this.RB_1V.Location = new System.Drawing.Point(5, 14);
            this.RB_1V.Name = "RB_1V";
            this.RB_1V.Size = new System.Drawing.Size(55, 17);
            this.RB_1V.TabIndex = 62;
            this.RB_1V.TabStop = true;
            this.RB_1V.Text = "+/- 1V";
            this.RB_1V.UseVisualStyleBackColor = true;
            this.RB_1V.CheckedChanged += new System.EventHandler(this.RB_1V_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(25, 13);
            this.label10.TabIndex = 61;
            this.label10.Text = "CH:";
            // 
            // NUD_CH
            // 
            this.NUD_CH.Location = new System.Drawing.Point(37, 31);
            this.NUD_CH.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.NUD_CH.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUD_CH.Name = "NUD_CH";
            this.NUD_CH.Size = new System.Drawing.Size(37, 20);
            this.NUD_CH.TabIndex = 60;
            this.NUD_CH.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textbox
            // 
            this.textbox.BackColor = System.Drawing.SystemColors.Window;
            this.textbox.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textbox.Location = new System.Drawing.Point(25, 249);
            this.textbox.Multiline = true;
            this.textbox.Name = "textbox";
            this.textbox.ReadOnly = true;
            this.textbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textbox.Size = new System.Drawing.Size(431, 462);
            this.textbox.TabIndex = 59;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 58);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 13);
            this.label11.TabIndex = 56;
            this.label11.Text = "Select DAC Port:";
            // 
            // CB_Port
            // 
            this.CB_Port.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Port.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CB_Port.FormattingEnabled = true;
            this.CB_Port.Location = new System.Drawing.Point(99, 53);
            this.CB_Port.Name = "CB_Port";
            this.CB_Port.Size = new System.Drawing.Size(82, 24);
            this.CB_Port.TabIndex = 69;
            this.CB_Port.Click += new System.EventHandler(this.CB_Port_Click);
            // 
            // BT_close
            // 
            this.BT_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_close.Location = new System.Drawing.Point(310, 53);
            this.BT_close.Name = "BT_close";
            this.BT_close.Size = new System.Drawing.Size(96, 24);
            this.BT_close.TabIndex = 71;
            this.BT_close.Text = "Close";
            this.BT_close.UseVisualStyleBackColor = true;
            this.BT_close.Click += new System.EventHandler(this.BT_close_Click);
            // 
            // BT_open
            // 
            this.BT_open.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_open.Location = new System.Drawing.Point(197, 53);
            this.BT_open.Name = "BT_open";
            this.BT_open.Size = new System.Drawing.Size(96, 24);
            this.BT_open.TabIndex = 70;
            this.BT_open.Text = "Open";
            this.BT_open.UseVisualStyleBackColor = true;
            this.BT_open.Click += new System.EventHandler(this.BT_open_Click);
            // 
            // Timer_Update_UART
            // 
            this.Timer_Update_UART.Enabled = true;
            this.Timer_Update_UART.Tick += new System.EventHandler(this.Update_UART_Tick);
            // 
            // Timer_Trigger
            // 
            this.Timer_Trigger.Tick += new System.EventHandler(this.Timer_Trigger_Tick);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(242, 72);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 13);
            this.label14.TabIndex = 76;
            this.label14.Text = "Difference:";
            // 
            // l_difference
            // 
            this.l_difference.AutoSize = true;
            this.l_difference.Location = new System.Drawing.Point(353, 72);
            this.l_difference.Name = "l_difference";
            this.l_difference.Size = new System.Drawing.Size(13, 13);
            this.l_difference.TabIndex = 77;
            this.l_difference.Text = "0";
            // 
            // BT_save
            // 
            this.BT_save.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_save.Location = new System.Drawing.Point(16, 169);
            this.BT_save.Name = "BT_save";
            this.BT_save.Size = new System.Drawing.Size(175, 23);
            this.BT_save.TabIndex = 78;
            this.BT_save.Text = "Save Picture of Chart";
            this.BT_save.UseVisualStyleBackColor = true;
            this.BT_save.Click += new System.EventHandler(this.BT_save_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 43);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(110, 13);
            this.label15.TabIndex = 79;
            this.label15.Text = "Select Mesuredevice:";
            // 
            // l_Version
            // 
            this.l_Version.AutoSize = true;
            this.l_Version.Location = new System.Drawing.Point(914, 714);
            this.l_Version.Name = "l_Version";
            this.l_Version.Size = new System.Drawing.Size(42, 13);
            this.l_Version.TabIndex = 81;
            this.l_Version.Text = "Version";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(462, 249);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 82;
            this.label12.Text = "Difference";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(1043, 673);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(24, 13);
            this.label13.TabIndex = 83;
            this.label13.Text = "Soll";
            // 
            // l_slope
            // 
            this.l_slope.AutoSize = true;
            this.l_slope.Location = new System.Drawing.Point(65, 72);
            this.l_slope.Name = "l_slope";
            this.l_slope.Size = new System.Drawing.Size(13, 13);
            this.l_slope.TabIndex = 84;
            this.l_slope.Text = "0";
            this.l_slope.Click += new System.EventHandler(this.L_slope_Click);
            // 
            // l_offset
            // 
            this.l_offset.AutoSize = true;
            this.l_offset.Location = new System.Drawing.Point(65, 56);
            this.l_offset.Name = "l_offset";
            this.l_offset.Size = new System.Drawing.Size(13, 13);
            this.l_offset.TabIndex = 85;
            this.l_offset.Text = "0";
            this.l_offset.Click += new System.EventHandler(this.L_offset_Click);
            // 
            // BT_saveAB
            // 
            this.BT_saveAB.Enabled = false;
            this.BT_saveAB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.BT_saveAB.Location = new System.Drawing.Point(16, 96);
            this.BT_saveAB.Name = "BT_saveAB";
            this.BT_saveAB.Size = new System.Drawing.Size(175, 32);
            this.BT_saveAB.TabIndex = 86;
            this.BT_saveAB.Text = "Save Values";
            this.BT_saveAB.UseVisualStyleBackColor = true;
            this.BT_saveAB.Click += new System.EventHandler(this.BT_saveAB_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(13, 72);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(34, 13);
            this.label18.TabIndex = 87;
            this.label18.Text = "Slope";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(12, 56);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(35, 13);
            this.label19.TabIndex = 88;
            this.label19.Text = "Offset";
            // 
            // l_finished
            // 
            this.l_finished.AutoSize = true;
            this.l_finished.Location = new System.Drawing.Point(118, 63);
            this.l_finished.Name = "l_finished";
            this.l_finished.Size = new System.Drawing.Size(0, 13);
            this.l_finished.TabIndex = 89;
            // 
            // NUD_multi
            // 
            this.NUD_multi.DecimalPlaces = 8;
            this.NUD_multi.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.NUD_multi.Location = new System.Drawing.Point(87, 20);
            this.NUD_multi.Name = "NUD_multi";
            this.NUD_multi.Size = new System.Drawing.Size(95, 20);
            this.NUD_multi.TabIndex = 90;
            this.NUD_multi.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 91;
            this.label9.Text = "Multiply by:";
            // 
            // RB_out_res_low
            // 
            this.RB_out_res_low.AutoSize = true;
            this.RB_out_res_low.Checked = true;
            this.RB_out_res_low.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.RB_out_res_low.Location = new System.Drawing.Point(5, 14);
            this.RB_out_res_low.Margin = new System.Windows.Forms.Padding(2);
            this.RB_out_res_low.Name = "RB_out_res_low";
            this.RB_out_res_low.Size = new System.Drawing.Size(68, 17);
            this.RB_out_res_low.TabIndex = 92;
            this.RB_out_res_low.TabStop = true;
            this.RB_out_res_low.Text = "270 Ohm";
            this.RB_out_res_low.UseVisualStyleBackColor = true;
            // 
            // RB_out_res_high
            // 
            this.RB_out_res_high.AutoSize = true;
            this.RB_out_res_high.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.RB_out_res_high.Location = new System.Drawing.Point(5, 35);
            this.RB_out_res_high.Margin = new System.Windows.Forms.Padding(2);
            this.RB_out_res_high.Name = "RB_out_res_high";
            this.RB_out_res_high.Size = new System.Drawing.Size(71, 17);
            this.RB_out_res_high.TabIndex = 93;
            this.RB_out_res_high.Text = "2.7k Ohm";
            this.RB_out_res_high.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RB_out_res_low);
            this.groupBox1.Controls.Add(this.RB_out_res_high);
            this.groupBox1.Location = new System.Drawing.Point(168, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(91, 56);
            this.groupBox1.TabIndex = 94;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RB_1V);
            this.groupBox2.Controls.Add(this.RB_10V);
            this.groupBox2.Location = new System.Drawing.Point(90, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(72, 56);
            this.groupBox2.TabIndex = 95;
            this.groupBox2.TabStop = false;
            // 
            // BT_Mesure_read
            // 
            this.BT_Mesure_read.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_Mesure_read.Location = new System.Drawing.Point(343, 36);
            this.BT_Mesure_read.Name = "BT_Mesure_read";
            this.BT_Mesure_read.Size = new System.Drawing.Size(63, 26);
            this.BT_Mesure_read.TabIndex = 96;
            this.BT_Mesure_read.Text = "Read";
            this.BT_Mesure_read.UseVisualStyleBackColor = true;
            this.BT_Mesure_read.Click += new System.EventHandler(this.BT_Mesure_read_Click);
            // 
            // l_mesure_read
            // 
            this.l_mesure_read.AutoSize = true;
            this.l_mesure_read.Location = new System.Drawing.Point(415, 43);
            this.l_mesure_read.Name = "l_mesure_read";
            this.l_mesure_read.Size = new System.Drawing.Size(16, 13);
            this.l_mesure_read.TabIndex = 97;
            this.l_mesure_read.Text = "---";
            // 
            // NUP_counts_set
            // 
            this.NUP_counts_set.Location = new System.Drawing.Point(291, 60);
            this.NUP_counts_set.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.NUP_counts_set.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.NUP_counts_set.Name = "NUP_counts_set";
            this.NUP_counts_set.Size = new System.Drawing.Size(75, 20);
            this.NUP_counts_set.TabIndex = 98;
            this.NUP_counts_set.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.NUP_counts_set.ValueChanged += new System.EventHandler(this.NUP_counts_set_ValueChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(209, 63);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 13);
            this.label16.TabIndex = 99;
            this.label16.Text = "counts set:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(11, 16);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(115, 13);
            this.label20.TabIndex = 101;
            this.label20.Text = "INL absolute value [V]:";
            // 
            // l_INL_absolut
            // 
            this.l_INL_absolut.AutoSize = true;
            this.l_INL_absolut.Location = new System.Drawing.Point(135, 16);
            this.l_INL_absolut.Name = "l_INL_absolut";
            this.l_INL_absolut.Size = new System.Drawing.Size(16, 13);
            this.l_INL_absolut.TabIndex = 103;
            this.l_INL_absolut.Text = "---";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(245, 16);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(57, 13);
            this.label21.TabIndex = 104;
            this.label21.Text = "Gain Error:";
            // 
            // L_gainerror
            // 
            this.L_gainerror.AutoSize = true;
            this.L_gainerror.Location = new System.Drawing.Point(308, 16);
            this.L_gainerror.Name = "L_gainerror";
            this.L_gainerror.Size = new System.Drawing.Size(16, 13);
            this.L_gainerror.TabIndex = 105;
            this.L_gainerror.Text = "---";
            this.L_gainerror.Click += new System.EventHandler(this.L_gainerror_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 16);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(201, 13);
            this.label22.TabIndex = 106;
            this.label22.Text = "Keithley DMM6500 6 1/2 Digit Multimeter";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 28);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(171, 13);
            this.label23.TabIndex = 107;
            this.label23.Text = "DAC 20 Bit Eigenbau (USB UART)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BT_send);
            this.groupBox3.Controls.Add(this.NUD_CH);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.NUD_V);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Location = new System.Drawing.Point(25, 157);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(481, 77);
            this.groupBox3.TabIndex = 108;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DAC Menu";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.BT_help);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.CB_Port);
            this.groupBox4.Controls.Add(this.BT_open);
            this.groupBox4.Controls.Add(this.BT_close);
            this.groupBox4.Location = new System.Drawing.Point(25, 71);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(481, 87);
            this.groupBox4.TabIndex = 109;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "DAC Setup";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Controls.Add(this.OpenSessionButton);
            this.groupBox5.Controls.Add(this.CloseSessionButton);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.BT_Mesure_read);
            this.groupBox5.Controls.Add(this.l_mesure_read);
            this.groupBox5.Location = new System.Drawing.Point(25, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(481, 69);
            this.groupBox5.TabIndex = 110;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Mesure Device Setup";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.BT_stop);
            this.groupBox6.Controls.Add(this.BT_start);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.NUP_interval);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.l_counts);
            this.groupBox6.Controls.Add(this.l_finished);
            this.groupBox6.Controls.Add(this.NUP_counts_set);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Location = new System.Drawing.Point(528, 8);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(428, 104);
            this.groupBox6.TabIndex = 111;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Mesure Setup";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.NUP_stop);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Controls.Add(this.label5);
            this.groupBox7.Controls.Add(this.label6);
            this.groupBox7.Controls.Add(this.NUP_start);
            this.groupBox7.Controls.Add(this.NUP_increment);
            this.groupBox7.Controls.Add(this.label7);
            this.groupBox7.Controls.Add(this.l_current_value_should);
            this.groupBox7.Controls.Add(this.label8);
            this.groupBox7.Controls.Add(this.l_current_value_is);
            this.groupBox7.Controls.Add(this.label14);
            this.groupBox7.Controls.Add(this.l_difference);
            this.groupBox7.Location = new System.Drawing.Point(528, 113);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(428, 95);
            this.groupBox7.TabIndex = 112;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Mesure Control";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.BT_save);
            this.groupBox8.Controls.Add(this.BT_write_file);
            this.groupBox8.Controls.Add(this.l_slope);
            this.groupBox8.Controls.Add(this.l_offset);
            this.groupBox8.Controls.Add(this.BT_saveAB);
            this.groupBox8.Controls.Add(this.label18);
            this.groupBox8.Controls.Add(this.label19);
            this.groupBox8.Controls.Add(this.NUD_multi);
            this.groupBox8.Controls.Add(this.label9);
            this.groupBox8.Location = new System.Drawing.Point(971, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(197, 205);
            this.groupBox8.TabIndex = 113;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Data";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label21);
            this.groupBox9.Controls.Add(this.label20);
            this.groupBox9.Controls.Add(this.l_INL_absolut);
            this.groupBox9.Controls.Add(this.L_gainerror);
            this.groupBox9.Location = new System.Drawing.Point(770, 214);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(398, 37);
            this.groupBox9.TabIndex = 114;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Data";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1198, 742);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.l_Version);
            this.Controls.Add(this.textbox);
            this.Controls.Add(this.chart1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(295, 316);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.NUP_interval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUP_start)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUP_stop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUP_increment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_V)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_CH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_multi)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUP_counts_set)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new MainForm());//Start APP
        }

        //###########################################################################################################################################################################################################
        //###########################################################################################################################################################################################################
        //                                      BACKEND
        //###########################################################################################################################################################################################################
        //###########################################################################################################################################################################################################

        private void OpenSession_Click(object sender, System.EventArgs e)//Open Dialog for Measur components
        {
            using (SelectResource sr = new SelectResource())//Selct Resource from list
            {
                if (lastResourceString != null)
                {
                    sr.ResourceName = lastResourceString;
                }
                DialogResult result = sr.ShowDialog(this);
                if (result == DialogResult.OK)//iff Buttoen OK is Pressed
                {
                    lastResourceString = sr.ResourceName;
                    using (var rmSession = new ResourceManager())//USe The selected Device for Measurments
                    {
                        try
                        {
                            mbSession = (MessageBasedSession)rmSession.Open(sr.ResourceName);//open Port
                            OpenSessionButton.Enabled = false;
                            CloseSessionButton.Enabled = true;
                            mesure_connected = true;
                            Adding_text_to_textbox("Connected with " + sr.ResourceName.ToString() + " Mesuredevice.");//Response to the Textbox
                            mbSession.RawIO.Write(":SENS:FUNC \"VOLT:DC\"");//Write Command Set to DC Voltage
                            mbSession.RawIO.Write(":SENS:VOLT:INP AUTO");//Write Command Set input Inpedance to Auto
                            mbSession.RawIO.Write(":SENS:VOLT:AZER ON");//Write Command Set the integration rate (NPLCs) to 10
                            mbSession.RawIO.Write(":SENS:VOLT:AVER:TCON REP");//Write Command Set the averaging filter type to repeating
                            mbSession.RawIO.Write(":SENS:VOLT:AVER ON");//Write Command Enable the filter
                        }
                        catch (InvalidCastException)
                        {
                            Adding_text_to_textbox("Resource selected must be a message-based session");//Display error in Message Box
                        }
                        catch (Exception exp)
                        {
                            Adding_text_to_textbox(exp.Message);//Display error in Message Box
                        }
                    }
                }
            }
        }

        private void CloseSession_Click(object sender, System.EventArgs e)//Close Session for Measurement
        {
            OpenSessionButton.Enabled = true;
            CloseSessionButton.Enabled = false;
            mbSession.Dispose();//Close Session
        }
        //###########################################################################################################################################################################################################

        private void BT_close_Click(object sender, EventArgs e)
        {
            if (sport.IsOpen)//check if serial port ist open
            { sport.Close(); }//close serial port

            BT_open.Enabled = true;//Enable Open Button
            //disable send and close Button
            BT_send.Enabled = false;
            BT_close.Enabled = false;

            sport_connected = false;//set bool to false
        }
        private void BT_open_Click(object sender, EventArgs e)
        {
            try//try to open Serial Port
            {
                Serialport_open(CB_Port.Text);//Open funtion serialport open
                Adding_text_to_textbox("Connected with " + CB_Port.Text + " DAC device.");//Response to the Textbox
            }
            catch (Exception) { Adding_text_to_textbox("Error No Port is selected"); }//if an error has happend catch with exeption
        }

//###########################################################################################################################################################################################################
        private void BT_start_Click(object sender, EventArgs e)//Button Start
        {
#if release
            BT_saveAB.Enabled = false;
            BT_write_file.Enabled = false;

            //Lable for gain error and INL
            L_gainerror.Text = "0";
            l_INL_absolut.Text = "0";

            //Nummeric Up/Down Process disable
            NUP_start.Enabled = false;
            NUP_stop.Enabled = false;
            NUP_increment.Enabled = false;
            NUP_counts_set.Enabled = false;

            //Check if connected
            if ((mesure_connected == true) && (sport_connected == true))
            {
                BT_stop.Enabled = true;
                BT_start.Enabled = false;
                should_value = (double)NUP_start.Value;//set start Value
                Timer_Trigger.Enabled = true;
                timer_trigger_need_stop = false;
                Timer_Trigger.Start();//Start Timer for Trigger
                counter = 0;//Reset Counter
                l_slope.Text = "0";
                l_offset.Text = "0";
                l_finished.Text = "";

                Set_Voltage(should_value);//Set Voltage
            }
            else
            {
                Adding_text_to_textbox("Faild not connected! Open Serial port and / or open Measure Device");//Else add error to Textbox
            }
#else
#endif
            Value_array_counter = 0;
            Array.Clear(Value_array, 0, Value_array.Length);//CLear Array
        }

        private void BT_stop_Click(object sender, EventArgs e)//Event for STopp Button 
        {
            //
            NUP_start.Enabled = true;
            NUP_stop.Enabled = true;
            NUP_increment.Enabled = true;
            NUP_counts_set.Enabled = true;

            //Reset Timer
            Timer_Trigger.Enabled = false;
            Timer_Trigger.Stop();//Stop Timer
            Timer_Trigger.Dispose();
            timer_trigger_need_stop = true;
            BT_start.Enabled = true;
            BT_stop.Enabled = false;
            Application.UseWaitCursor = false;//USe default cursor
        }

        private void BT_send_Click(object sender, EventArgs e)
        {
            double voltage = (double)NUD_V.Value;//set decimal Value from GUI
            Set_Voltage(voltage);//Pass over the Voltage to the set Voltage function
        }

        private void B_help_Click(object sender, EventArgs e)
        {
            try
            {
                if (sport_connected == true)//Check if Port is Open
                {
                    sport.Write("Help_\r");//sending Help
                }
                else
                {
                    Adding_text_to_textbox("Please Connect First");//Text output for the textbox
                }
            }
            catch (Exception ex)//check for errors
            { Adding_text_to_textbox("Error:" + ex.Message.ToString()); }
        }
//###########################################################################################################################################################################################################
        private void NUD_V_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)//Check if Button Enter on a Keyboard is press do same as Send
            {
                //Send the Value from the input panel to the DAC
                double voltage = (double)NUD_V.Value;
                Set_Voltage(voltage);
            }
        }

        private void BT_save_Click(object sender, EventArgs e)
        {
            //Generate Time Stamp for Picture
            string year = DateTime.Now.Year.ToString("0000");
            string month = DateTime.Now.Month.ToString("00");
            string day = DateTime.Now.Day.ToString("00");
            string hour = DateTime.Now.Hour.ToString("00");
            string minute = DateTime.Now.Minute.ToString("00");
            string second = DateTime.Now.Second.ToString("00");
            string Filename = "mychart__" + year + "." + month + "." + day + "-" + hour + ";" + minute + ";" + second + ".png";

            chart1.SaveImage(Filename, ChartImageFormat.Png);//Save Chart as PNG in same path as EXE APP
            Process.Start(Filename);//Open File
        }

        private void BT_write_file_Click(object sender, EventArgs e)
        {
            //Writes complete array to a txt File 
            double increment = (double)NUP_start.Value;
            //Generate Time Stamp for File Name
            string year = DateTime.Now.Year.ToString("0000");
            string month = DateTime.Now.Month.ToString("00");
            string day = DateTime.Now.Day.ToString("00");
            string hour = DateTime.Now.Hour.ToString("00");
            string minute = DateTime.Now.Minute.ToString("00");
            string second = DateTime.Now.Second.ToString("00");
            string Filename = "Data__" + year + "." + month + "." + day + "-" + hour + ";" + minute + ";" + second + ".txt";
            //
            using (StreamWriter sr = new StreamWriter(Filename))//Write File for TXT File in the same path as the EXE APP
            {
                for (int counter_data = 0; counter_data < counter; counter_data++)//Write data to File
                {
                    sr.Write(increment);//Write SOLL
                    sr.Write("  ");
                    sr.WriteLine(Value_array[counter_data]);//Write IST
                    increment += (double)NUP_increment.Value;
                }
            }
            Process.Start(Filename);//Open File
        }

        private void BT_saveAB_Click(object sender, EventArgs e)
        {
            string Filename = "Calc_Value.txt";
            using (StreamWriter sw = File.AppendText(Filename))//Write File for TXT File in the same path as the EXE APP
            {
                //Send Data to File 
                sw.Write("CH" + NUD_CH.Value.ToString());
                sw.Write("  ");
                sw.Write("a=" + AlphaA_Value.ToString());
                sw.Write("  ");
                sw.WriteLine("b=" + AlphaB_Value.ToString());
                sw.WriteLine("");

            }
            Process.Start(Filename);//Open File
        }


        private void CB_Port_Click(object sender, EventArgs e)//Combo Box click event
        {
            CB_Port.Items.Clear();//Clear all items --> clear all com names
            //add for each avaible com port a Item
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())//Search for new com port names
            {
                CB_Port.Items.Add(s);//Add Item
            }
        }


//###########################################################################################################################################################################################################
        private void Update_UART_Tick(object sender, EventArgs e)
        {
            Update_UART();//Update Serial Interface
        }
        private void Timer_Trigger_Tick(object sender, EventArgs e)
        {
            Timer_Trigger.Enabled = false;//Disable the Timer Interrubt
            Application.UseWaitCursor = true;//Cursor as wait

            if (should_value > ((double)NUP_stop.Value))//check if end Value is reached
            {
                BT_saveAB.Enabled = true;
                BT_write_file.Enabled = true;

                //Nummeric Up/Down Process enable
                NUP_start.Enabled = true;
                NUP_stop.Enabled = true;
                NUP_increment.Enabled = true;
                NUP_counts_set.Enabled = true;

                //Reset Timer
                Timer_Trigger.Enabled = true;
                Timer_Trigger.Enabled = false;
                Timer_Trigger.Dispose();//Diasable Timer_Trigger
                Thread.Sleep(20);//Time for the Timer to shutdowm
                l_finished.Text = "Finished";
                BT_start.Enabled = true;
                BT_stop.Enabled = false;


                double increment = (double)NUP_start.Value;//First set Increment to the start point as the Start Value
                double[] difference = new double[100000];//Set new array, 100000 is the maximum test points
                for (int i = 0; i < counter; i++)//As long i is smaller as counter
                {
                    difference[i] = Value_array[i] - increment;//Calc difference from the Value array and the Increment
                    difference[i] = Math.Round(difference[i], 10);//Round the difference to the 10-th number 
                    increment += (double)NUP_increment.Value;//Increas the Increment by the increamant value
                }

                //Calc accurcy with the min max ABS of the full scale
                double ABS_Input = ((double)Math.Abs(NUP_start.Value) + (double)Math.Abs(NUP_stop.Value));//calc the fullscale input 
                double ABS_Output_max_difference = (Math.Abs(difference.Max()) + Math.Abs(difference.Min()));//calc th fullscale output_diffrence
                //double accurcy = ABS_Input / ABS_Output_max_difference;

                l_INL_absolut.Text = ABS_Output_max_difference.ToString();//set INL as lable to the frontend

                L_gainerror.Text = ((ABS_Output_max_difference / ABS_Input) * 100).ToString();//set gain error to the frontend

                //Save data in double arrays
                double[] x_array = new double[counter];
                double[] y_array = new double[counter];

                //set start Values for the arrays
                x_array[0] = (double)NUP_start.Value;
                y_array[0] = Value_array[0];
                //count as long counter_array is same as counter
                for (int counter_array = 1; counter_array < counter; counter_array++)
                {
                    y_array[counter_array] = Value_array[counter_array];//save the Value to the y array
                    x_array[counter_array] = (double)NUP_start.Value + ((double)NUP_increment.Value * counter_array);//save value to the x array
                }


                //Extract A and B VAlue from the Array
                AlphaA_Value = AlphaA(x_array, y_array, (int)counter);//Pass values over to the function
                AlphaB_Value = AlphaB(x_array, y_array, (int)counter);//Pass values over to the function

                //setting the Value for A and B in the labels
                l_slope.Text = AlphaA_Value.ToString();//set lable in the Front end
                l_offset.Text = AlphaB_Value.ToString();//set lable in the Front end

                Application.UseWaitCursor = false;//Cursor default
                return;
            }
            double current_value = 0;

#if release
            Set_Voltage(should_value);//Set Voltage
            Check_ack();//wait for the "OK String"
            Wait(500);//TEST42
            current_value = Get_Voltage();//Save the Value
#endif
            l_counts.Text = counter.ToString();
            l_current_value_should.Text = should_value.ToString();
            l_current_value_is.Text = current_value.ToString();

            Update_chart();//Update Chart

            counter++;//Increas Counter
            should_value += (double)NUP_increment.Value;//Increas should value by the Increment
            should_value = Math.Round(should_value, 6);//Round the number because of calc error

            Application.UseWaitCursor = false;//Curser Wait
            if (timer_trigger_need_stop == false) { Timer_Trigger.Enabled = true; }//Enable the Interrupt only when is allowed
        }

        private void NUP_interval_ValueChanged(object sender, EventArgs e)
        {
            Timer_Trigger.Interval = (int)NUP_interval.Value;//Update Timer Interval
        }

        private void NUP_start_ValueChanged(object sender, EventArgs e)
        {
            should_value = (double)NUP_start.Value;//Update Start Value
        }

        private void RB_10V_CheckedChanged(object sender, EventArgs e)
        {
            NUD_V.Maximum = 10;//set Maximum for +/- 10V range
            NUD_V.Minimum = -10;//set Minimum for +/- 10V range
        }

        private void RB_1V_CheckedChanged(object sender, EventArgs e)
        {
            NUD_V.Maximum = 1;//set Maximum for +/- 10V range
            NUD_V.Minimum = -1;//set Minimum for +/- 1V range
        }

        private void L_gainerror_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(L_gainerror.Text);//safe Text to Clipboard
        }

        private void L_slope_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(l_slope.Text);//safe Text to Clipboard
        }

        private void L_offset_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(l_offset.Text);//safe Text to Clipboard
        }

        private void Set_Voltage(double voltage)//Send Voltage over UART to DAC 
        {

            int output;
            if ((RB_1V.Checked == true) && (NUP_start.Value >= -1) && (NUP_stop.Value <= 1))//Check if Radio Button is in Correct Range
            {
                output = 1; //Out Put mode 1 = +/- 1V
                RB_1V.Checked = true;//Radio Button aktivieren
                RB_10V.Checked = false;//Radio Button deaktivieren
            }
            else
            {
                output = 10; //Out Put mode 10 = +/- 10V
                RB_1V.Checked = false;//Radio Button deaktivieren
                RB_10V.Checked = true;//Radio Button aktivieren
            }

            string OUT_RES;
            if (RB_out_res_high.Checked) { OUT_RES = "High"; }
            else { OUT_RES = "Low"; }

            try// Try to Send Data
            {

                if (sport_connected == true && sport.IsOpen == true)//Check if connected
                {
                    //send Data over Serial Port
                    sport.Write("SET_CH" + NUD_CH.Value.ToString() + "_" + voltage.ToString("F99").TrimEnd('0') + "_OUT" + output.ToString() + "_" + OUT_RES + "\r");//sending to serial Port without scientific spelling
                }
                else
                {
                    sport_connected = false;
                    Adding_text_to_textbox("Not Connected");//Text output for the textbox
                }
            }
            catch (Exception ex)//check for errors
            { Adding_text_to_textbox(ex.ToString());}//add the error message to the log textbox
        }

        private double Get_Voltage()//Measure The Voltage
        {
            double multiplier = (double)NUD_multi.Value;

            double current_value = 0.0;
            try//Try to Receive a Value from Device
            {
                string command = "MEASure?\n";//command to receive the measuret data
                string textToWrite = command.Replace("\\n", "\n").Replace("\\r", "\r");
                mbSession.RawIO.Write(textToWrite);//Write Command
                current_value = Convert.ToDouble(mbSession.RawIO.ReadString());//Extract Value
                current_value *= multiplier;
                Store_in_array(current_value);//store value in Array
            }
            catch (Exception exp) { Adding_text_to_textbox(exp.ToString()); }

            return current_value;//Return Value
        }


        private void Check_ack()//Check if Data was received from the uController
        {
            string string_ack = "";
            string string_buffer;
            while (!(string_ack.Contains("OK")))//Search for ACK in the Textbox in the Passt 45 symbols
            {
                Application.DoEvents();//Refresh GUI
                Update_UART();
                Wait(100);//Time to Fil up the UART buffer if it is necessary 
                string_buffer = textbox.Text;//safe textbox data string temp in a string buffer
                string_ack = string_buffer.Substring((string_buffer.Length - 50), 20);//extract spezific Substring
                //"\n\r\n\nO\nK \r\nSending --"
                string_ack = string_ack.Replace("\n","");//Replace String content with nothing
                string_ack = string_ack.Replace("\r", "");//Replace String content with nothing
               
            }
        }

        private void Store_in_array(double should_value)//store should value in array
        {
            Value_array[Value_array_counter] = should_value;//add value to the array at spezific adress
            Value_array_counter++;//Increas adress
        }
        public static void Wait(int milliseconds)//Delay Function with async
        {
            System.Windows.Forms.Timer Timer_wait_ms = new System.Windows.Forms.Timer();//set new timer
            if (milliseconds == 0 || milliseconds < 0) return;//quit if input Value is not valid
            Timer_wait_ms.Interval = milliseconds;//set timer interval
            Timer_wait_ms.Enabled = true;
            Timer_wait_ms.Start();//Start Timer
            //Run Timer function
            Timer_wait_ms.Tick += (s, e) =>
            {
                Timer_wait_ms.Stop();
                Timer_wait_ms.Enabled = false;
                Timer_wait_ms.Stop();
            };
            //Wait as long timer Triggers
            while(Timer_wait_ms.Enabled && mesure_connected && sport_connected)
            {
                //Do nothing else as:
                Application.DoEvents();//Update UI 
            }
        }
        //https://de.wikipedia.org/wiki/Methode_der_kleinsten_Quadrate
        //Lineare Regression wert berechnung für a1 (Gain)
        private double AlphaA(double[] x, double[] y, int i)
        {
            double x_mittelwert = 0;
            double y_mittelwert = 0;
            //Mittelwert berechnung
            for (int counter = 1; counter <= i; counter++)
            {
                x_mittelwert += x[counter - 1];
                y_mittelwert += y[counter - 1];
            }
            x_mittelwert /= i;
            y_mittelwert /= i;
            //Berechnung der Summen
            double SP = 0;
            double SQ = 0;
            for (int counter = 1; counter <= i; counter++)
            {
                SP += ((x[counter - 1] - x_mittelwert) * (y[counter - 1] - y_mittelwert));
                SQ += Math.Pow((x[counter - 1] - x_mittelwert), 2);
            }
            return SP / SQ;
        }

        //Lineare Regression wert berechnung für a0 (Offset)
        private double AlphaB(double[] x, double[] y, int i)
        {
            double x_mittelwert = 0;
            double y_mittelwert = 0;
            //Mittelwert berechnung
            for (int counter_mittelwert = 1; counter_mittelwert <= i; counter_mittelwert++)
            {
                x_mittelwert += x[counter_mittelwert - 1];
                y_mittelwert += y[counter_mittelwert - 1];
            }
            x_mittelwert /= i;
            y_mittelwert /= i;
            //Berechnung der Summen
            double SP = 0;
            double SQ = 0;
            for (int counter_summe = 1; counter_summe <= i; counter_summe++)
            {
                SP += ((x[counter_summe - 1] - x_mittelwert) * (y[counter_summe - 1] - y_mittelwert));
                SQ += Math.Pow((x[counter_summe - 1] - x_mittelwert), 2);
            }
            return y_mittelwert - ((SP / SQ) * x_mittelwert);
        }

        private void Update_chart()//Update Chart
        {
            double increment_chart = (double)NUP_start.Value;
            try//Try to Recalculate Points
            {
                chart1.ChartAreas[0].AxisY.IsStartedFromZero = false;//reset Y Axis
                chart1.ChartAreas[0].AxisX.Minimum = 0;//set Minimum of X Axis
                chart1.ChartAreas[0].RecalculateAxesScale();//Try to rescale the chart for ä perfetc fit
            }
            catch { };

            foreach (var series in chart1.Series)//Clear all old points
            {
                series.Points.Clear();//delete all old Diagramm Points
            }
            chart1.Series.Clear();//Clear chart1

            var pointzero = new System.Windows.Forms.DataVisualization.Charting.Series//add line of zero Voltage level
            {
                Name = "0V",//Name lable for 0V level
                Color = System.Drawing.Color.Black,//Color for the line
                IsVisibleInLegend = true,//Set parameter for Visibility
                IsXValueIndexed = true,//Set parameter for Index
                ChartType = SeriesChartType.Line,//set Line Type
            };
            this.chart1.Series.Add(pointzero);

            var series1 = new System.Windows.Forms.DataVisualization.Charting.Series//add line of Data
            {
                Name = "Series1",//Name lable for first chart level
                Color = System.Drawing.Color.Blue,//Color for the line
                IsVisibleInLegend = true,//Set parameter for Visibility
                IsXValueIndexed = true,//Set parameter for Index
                ChartType = SeriesChartType.Line,//set Line Type
                LegendText = "Channel"//set Legend Tex
            };
            this.chart1.Series.Add(series1);//add line in chart1



            double differnce_chart;
            //Display all Points
            for (int i = 0; i <= counter; i++)//Read data as long data points exists 
            {
                differnce_chart = Value_array[i] - increment_chart;//Calc the diffrence of Value
                differnce_chart = Math.Round(differnce_chart, 10);//Round to the 10-th. Comma Value
                l_difference.Text = differnce_chart.ToString();//Set lable for difference
                increment_chart = Math.Round(increment_chart, 10);//Round to the 10-th. Comma Value
                series1.Points.AddXY(increment_chart, differnce_chart);//Add X and Y points to the chart for DATA
                pointzero.Points.AddXY(increment_chart, 0);//Add X and Y points to the chart for Zero line
                increment_chart += (double)NUP_increment.Value;//Increas Increment
            }

            chart1.Invalidate();//Show diagram
        }



        private void Serialport_open(string port)//Open Serial port
        {
            sport = new System.IO.Ports.SerialPort(
            port, 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);//generate new object for sport with specific attributs 
            try//Try to:
            {
                sport.Open();//Open Serial port
                //Enable Button Close and Button Send
                BT_close.Enabled = true;//Enable Button Close
                BT_send.Enabled = true;//Enable Button send
                BT_open.Enabled = false;//Disable Button open
                sport_connected = true;//set bool to true

            }
            catch (Exception ex) { Adding_text_to_textbox("Error:" + ex.Message.ToString()); }//if an error has happend catch with exception
        }


        private void Adding_text_to_textbox(String input)//Funtion for adding text to the end of the text box
        {
            textbox.Text += input;//adding the input
            textbox.Text += "\r\n";//adding Carriage Return and Line Feed ( Used as a new line character in Windows)
            textbox.SelectionStart = textbox.Text.Length;//dynamic size
            textbox.ScrollToCaret();//scroll to the end
        }

        private void Update_UART()
        {
            //Update UART communication 
            string data;
            if (sport_connected == true && sport.IsOpen == true)
            {
                try//Try to read DATA from Serial
                {
                    while (sport.BytesToRead > 0)//ready data
                    {
                        //data = sport.ReadExisting();//Read until end
                        data = sport.ReadExisting();//Read until end
                        data += "\n";
                        data = data.Replace("\0", string.Empty);//Take all

                        textbox.Text += data;//add to text box
                        textbox.SelectionStart = textbox.Text.Length;//dynamic size
                        textbox.ScrollToCaret();//scroll to the end
                    }
                }
                catch (Exception ex)//check for errors
                { Adding_text_to_textbox("Error:" + ex.Message.ToString()); }//Add error to textbox
            }
        }

        private void BT_Mesure_read_Click(object sender, EventArgs e)
        {
            if (mesure_connected == true)//Check if connectet
            {
                try//Try to Receive a Value from Device
                {
                    mbSession.RawIO.Write(":READ?");//Write Command
                    l_mesure_read.Text = Convert.ToDouble(mbSession.RawIO.ReadString()).ToString();//Extract Value
                }
                catch (Exception exp) { Adding_text_to_textbox(exp.ToString()); }//add error Message to Textbox log
            }
            else
            {
                Adding_text_to_textbox("Faild not connected, open Measure Device");//Else add error to Textbox
            }
        }
            
           

        private void NUP_increment_ValueChanged(object sender, EventArgs e)
        {
            //recalculate Counts Value
            decimal start_value;
            decimal stop_value;
            if (NUP_start.Value < 0) { start_value = (NUP_start.Value * -1); } else { start_value = NUP_start.Value; }//checks the start value and if necessary muliply by *-1
            if (NUP_stop.Value < 0) { stop_value = (NUP_stop.Value * -1); } else { stop_value = NUP_stop.Value; }//checks the stop value and if necessary muliply by *-1
            try
            {
                NUP_counts_set.Value = ((start_value + stop_value) / NUP_increment.Value);//Recalculate the counts_set value
            }
            catch{ }//catch if value is out of range
        }

        private void NUP_counts_set_ValueChanged(object sender, EventArgs e)
        {
            //recalculate Increment Value
            decimal start_value;
            decimal stop_value;
            if (NUP_start.Value < 0) { start_value = (NUP_start.Value * -1); } else { start_value = NUP_start.Value; }//checks the start value and if necessary muliply by *-1
            if (NUP_stop.Value < 0) { stop_value = (NUP_stop.Value * -1); } else { stop_value = NUP_stop.Value; }//checks the stop value and if necessary muliply by *-1
            try
            {
                NUP_increment.Value = ((start_value + stop_value) / NUP_counts_set.Value);//Recalculate the increment value
            }
            catch { }//catch if value is out of range
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //shutdown mesure Process
            try
            {
                //Set connection flag to false
                mesure_connected = false;
                sport_connected = false;

                //Stop Timer_Trigger
                Timer_Trigger.Enabled = false;
                Timer_Trigger.Stop();
                Timer_Trigger.Dispose();
                timer_trigger_need_stop = true;

                //Stop Timer_Update_UART 
                Timer_Update_UART.Enabled = false;
                Timer_Update_UART.Stop();
                Timer_Update_UART.Dispose();

                //Stop Serial and close it
                if (sport.IsOpen)//check if serial port ist open
                {
                    sport.Close();//close serial port
                }

                mbSession.Dispose();//Close Session
            }
            catch { };

            Application.Exit();//Close APP
        }
    }  
}
