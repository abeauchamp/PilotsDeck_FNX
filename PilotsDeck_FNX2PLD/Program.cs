using FSUIPC;
using Serilog;
using System.Configuration;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms;
using System.Text;

namespace PilotsDeck_FNX2PLD
{
    public partial class MyFancyForm : Form
    {
        private System.Windows.Forms.TextBox textBox1;
        private CheckBox checkBoxFSUIPC;
        private Label label2;
        private CheckBox checkBoxFenix;
        private Label label3;
        private CheckBox checkBoxStarted;
        private Label label4;
        private CheckBox checkBoxDataLoaded;
        private System.ComponentModel.IContainer components;
        private Label label1;

        public MyFancyForm()
        {
            InitializeComponent();
        }

        public enum StartState
        {
            INIT,
            FSUIPC_STARTED,
            AIRCRAFT_FOUND,
            SIM_STARTED,
            DATA_LOADED
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyFancyForm));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBoxFSUIPC = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxFenix = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxStarted = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxDataLoaded = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "FSUIPC7 connection :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox1.Location = new System.Drawing.Point(14, 112);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(702, 362);
            this.textBox1.TabIndex = 1;
            // 
            // checkBoxFSUIPC
            // 
            this.checkBoxFSUIPC.AutoSize = true;
            this.checkBoxFSUIPC.Enabled = false;
            this.checkBoxFSUIPC.Location = new System.Drawing.Point(157, 12);
            this.checkBoxFSUIPC.Name = "checkBoxFSUIPC";
            this.checkBoxFSUIPC.Size = new System.Drawing.Size(15, 14);
            this.checkBoxFSUIPC.TabIndex = 2;
            this.checkBoxFSUIPC.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fenix Aircraft selected :";
            // 
            // checkBoxFenix
            // 
            this.checkBoxFenix.AutoSize = true;
            this.checkBoxFenix.Enabled = false;
            this.checkBoxFenix.Location = new System.Drawing.Point(157, 36);
            this.checkBoxFenix.Name = "checkBoxFenix";
            this.checkBoxFenix.Size = new System.Drawing.Size(15, 14);
            this.checkBoxFenix.TabIndex = 4;
            this.checkBoxFenix.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Simulation started :";
            // 
            // checkBoxStarted
            // 
            this.checkBoxStarted.Enabled = false;
            this.checkBoxStarted.Location = new System.Drawing.Point(157, 60);
            this.checkBoxStarted.Name = "checkBoxStarted";
            this.checkBoxStarted.Size = new System.Drawing.Size(15, 14);
            this.checkBoxStarted.TabIndex = 6;
            this.checkBoxStarted.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Data loaded :";
            // 
            // checkBoxDataLoaded
            // 
            this.checkBoxDataLoaded.Enabled = false;
            this.checkBoxDataLoaded.Location = new System.Drawing.Point(157, 85);
            this.checkBoxDataLoaded.Name = "checkBoxDataLoaded";
            this.checkBoxDataLoaded.Size = new System.Drawing.Size(15, 14);
            this.checkBoxDataLoaded.TabIndex = 8;
            this.checkBoxDataLoaded.UseVisualStyleBackColor = true;
            // 
            // MyFancyForm
            // 
            this.ClientSize = new System.Drawing.Size(728, 486);
            this.Controls.Add(this.checkBoxDataLoaded);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBoxStarted);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBoxFenix);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBoxFSUIPC);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MyFancyForm";
            this.Text = "PilotsDeck_FNX";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MyFancyForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void MyFancyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Log.Information($"Window closed : program exiting");
            Environment.Exit(0);
        }

        internal void addLog(string? line)
        {
            textBox1.Invoke((MethodInvoker)delegate{
                textBox1.AppendText(line + System.Environment.NewLine);
            });
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        internal void setState(StartState state)
        {

            checkBoxFSUIPC.Invoke((MethodInvoker)delegate {
                switch (state)
                {
                    case StartState.INIT:
                        checkBoxFSUIPC.Checked = false;
                        checkBoxFenix.Checked = false;
                        checkBoxStarted.Checked = false;
                        checkBoxDataLoaded.Checked = false;
                        break;
                    case StartState.FSUIPC_STARTED:
                        checkBoxFSUIPC.Checked = true;
                        checkBoxFenix.Checked = false;
                        checkBoxStarted.Checked = false;
                        checkBoxDataLoaded.Checked = false;
                        break;
                    case StartState.AIRCRAFT_FOUND:
                        checkBoxFSUIPC.Checked = true;
                        checkBoxFenix.Checked = true;
                        checkBoxStarted.Checked = false;
                        checkBoxDataLoaded.Checked = false;
                        break;
                    case StartState.SIM_STARTED:
                        checkBoxFSUIPC.Checked = true;
                        checkBoxFenix.Checked = true;
                        checkBoxStarted.Checked = true;
                        checkBoxDataLoaded.Checked = false;
                        break;
                    case StartState.DATA_LOADED:
                        checkBoxFSUIPC.Checked = true;
                        checkBoxFenix.Checked = true;
                        checkBoxStarted.Checked = true;
                        checkBoxDataLoaded.Checked = true;
                        break;
                }
            });
            
        }
    }

    public class Program
    {
        public static readonly string FenixExecutable = Convert.ToString(ConfigurationManager.AppSettings["FenixExecutable"]) ?? "FenixSystem";
        public static readonly string logFilePath = Convert.ToString(ConfigurationManager.AppSettings["logFilePath"]) ?? "FNX2PLD.log";
        public static readonly string logLevel = Convert.ToString(ConfigurationManager.AppSettings["logLevel"]) ?? "Debug";
        public static readonly bool waitForConnect = Convert.ToBoolean(ConfigurationManager.AppSettings["waitForConnect"]);
        public static readonly bool ignoreCurrentAC = Convert.ToBoolean(ConfigurationManager.AppSettings["ignoreCurrentAC"]);
        public static readonly int offsetBase = Convert.ToInt32(ConfigurationManager.AppSettings["offsetBase"], 16);
        public static readonly int updateIntervall = Convert.ToInt32(ConfigurationManager.AppSettings["updateIntervall"]);
        public static readonly int waitReady = Convert.ToInt32(ConfigurationManager.AppSettings["waitReady"]);
        public static readonly int waitTick = Convert.ToInt32(ConfigurationManager.AppSettings["waitTick"]);
        public static readonly string groupName = "FNX2PLD";

        private static MemoryScanner? scanner = null;
        private static ElementManager? elementManager = null;

        private static MyFancyForm? guiForm = null;
        private static StreamReader? logFileReader = null;
        private static CancellationTokenSource? cancellationTokenSource = null;

        public static void Main()
        {
            File.Delete(logFilePath);
            LoggerConfiguration loggerConfiguration = new LoggerConfiguration().WriteTo.File(logFilePath, rollingInterval: RollingInterval.Infinite, retainedFileCountLimit: 1);
            if (logLevel == "Warning")
                loggerConfiguration.MinimumLevel.Warning();
            else if (logLevel == "Debug")
                loggerConfiguration.MinimumLevel.Debug();
            else
                loggerConfiguration.MinimumLevel.Information();
            Log.Logger = loggerConfiguration.CreateLogger();
            Log.Information($"Program: FNX2PLD started! Log Level: {logLevel} Log File: {logFilePath}");

            Stream stream = new FileStream(logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            logFileReader = new StreamReader(stream);

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                Thread.Sleep(waitTick * 100);

                while (true) {
                    readLogs();
                    Thread.Sleep(waitTick * 1000);
                } 
            }).Start();

                new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                /* run your code here */

                Thread.Sleep(2000);

                try
                {
                    //Init Prog, Open Process/FSUIPC, Wait for Connect
                    if (!Initialize())
                    {
                        Log.Logger.Information($"Please restart this app when FSUIPC7 is running");
                        return;
                    }

                    Log.Logger.Information($"Connected to FSUIPC7");
                    guiForm.setState(MyFancyForm.StartState.FSUIPC_STARTED);

                    cancellationTokenSource = new CancellationTokenSource();
                    CancellationToken cancellationToken = cancellationTokenSource.Token;

                    while (!cancellationToken.IsCancellationRequested)
                    {
                        if (Wait())
                        {
                            MainLoop(cancellationToken);
                            if (FSUIPCConnection.IsOpen)
                            {
                                Log.Logger.Information($"Program: Resetting Session (MainLoop stopped and FSUIPC Connected)");
                                Reset();
                            }
                        }
                        else if (!FSUIPCConnection.IsOpen)
                        {
                            Log.Logger.Error($"Program: FSUIPC Connection is closed - exiting.");
                            break;
                        }
                        else if (!IPCManager.IsSimOpen())
                        {
                            Log.Logger.Error($"Flight simulator not running - exiting.");
                            break;
                        }
                        else if (!IPCManager.IsAircraftFenix() && !ignoreCurrentAC)
                        {
                            Log.Logger.Warning($"Program: Loaded Aircraft is not a Fenix - exiting.");
                            break;
                        }
                        else
                        {
                            Log.Logger.Information($"Program: Resetting Session (WaitLoop failed)");
                            Reset();
                            Log.Information($"Program: Waiting {waitTick}s");
                            Thread.Sleep(waitTick * 1000);
                        }
                    }

                    Log.Logger.Information($"Please restart this app while FSUIPC7 and FS2020 are running");

                }
                catch (Exception ex)
                {
                    Log.Logger.Error($"Program: Critical Exception occured: {ex.Source} - {ex.Message}");
                }
            }).Start();

            guiForm = new MyFancyForm();
            guiForm.ShowDialog();
        }

        private static void MainLoop(CancellationToken cancellationToken)
        {
            //Main Loop
            Stopwatch watch = new Stopwatch();
            int measures = 0;
            int averageTick = 150;

            try
            {
                bool firstLoop = true;
                while (!cancellationToken.IsCancellationRequested)
                {
                    watch.Start();

                    if (!scanner.UpdateBuffers(elementManager.Patterns))
                    {
                        Log.Logger.Error($"Program: UpdateBuffers() failed - Exiting");
                        break;
                    }
                    elementManager.GenerateValues();
                    if (firstLoop){
                        guiForm.setState(MyFancyForm.StartState.DATA_LOADED);
                        Log.Logger.Information($"Success");
                        firstLoop = false;
                    }

                    watch.Stop();
                    measures++;
                    if (measures > averageTick)
                    {
                        Log.Logger.Debug($"Program: -------------------------------- Average elapsed Time for Reading and Updating Buffers: {string.Format("{0,3:F}", (watch.Elapsed.TotalMilliseconds) / averageTick)}ms --------------------------------");
                        measures = 0;
                        watch.Reset();
                    }


                    Thread.Sleep(updateIntervall);
                }
            }
            catch(Exception e)
            {
                Log.Logger.Error($"Program: Critical Exception during MainLoop()");
                cancellationTokenSource.Cancel();
            }
        }

        private static void readLogs()
        {
            if(guiForm != null) {
                string line;
                while ((line = logFileReader.ReadLine()) != null)
                {
                    guiForm?.addLog(line);
                }
            }
            
        }

        private static bool Wait()
        {
            bool startDirect = true;

            try
            {
                //Wait until Fenix is the current Aircraft for FSUIPC
                do
                {
                    IPCManager.RefreshCurrentAircraft();
                    if (!IPCManager.IsAircraftFenix())
                    {
                        startDirect = false;
                        Log.Information($"Program: Wating for until Fenix is the loaded Aircraft, sleeping for {waitTick * 4}s");
                        //Thread.Sleep(waitTick * 1000 * 4);
                        Thread.Sleep(1000);
                    }
                    if (!IPCManager.IsSimOpen())
                    {
                        Log.Information($"Sim has closed while waiting for Aircraft");
                        return false;
                    }
                }
                while (!IPCManager.IsAircraftFenix());

                guiForm.setState(MyFancyForm.StartState.AIRCRAFT_FOUND);

                //Wait until the Fenix Executable is running
                Process? fenixProc = null;
                while (fenixProc == null)
                {
                    fenixProc = Process.GetProcessesByName(FenixExecutable).FirstOrDefault();
                    if (fenixProc != null)
                        scanner = new MemoryScanner(fenixProc);
                    else
                    {
                        startDirect = false;
                        if (FSUIPCConnection.IsOpen)
                        {
                            if (IPCManager.IsSimOpen())
                            {
                                IPCManager.RefreshCurrentAircraft();
                                if (IPCManager.IsAircraftFenix())
                                {
                                    Log.Warning($"Program: Could not find Process {FenixExecutable}, trying again in {waitTick * 2}s");
                                    Thread.Sleep(waitTick * 1000 * 2);
                                }
                                else
                                {
                                    Log.Warning($"Program: Aircraft changed!");
                                    return false;
                                }
                            }
                            else
                            {
                                Log.Information($"Sim has closed while waiting for Process");
                                return false;
                            }
                        }
                        else
                        {
                            Log.Error($"Program: FSUIPC Closed while waiting on Fenix Process - exiting!");
                            return false;
                        }
                    }
                }

                Log.Information($"Sim sessions started");
                guiForm.setState(MyFancyForm.StartState.SIM_STARTED);

                //Delay Scanner Initialization until User clicked "Ready to Fly"
                if (!startDirect && Program.waitReady > 0)
                {
                    Log.Information($"Program: Waiting for User to click Ready to Fly, sleeping {waitReady}s");
                    Thread.Sleep(waitReady * 1000);
                }

                if (!scanner.IsInitialized())
                {
                    Log.Error($"Program: Could not open Process {FenixExecutable}!");
                    return false;
                }

                //Start WASM and ElementManager
                Log.Information($"Program: Initializing WASM Module");
                MSFSVariableServices.Init();
                MSFSVariableServices.LVARUpdateFrequency = 0;
                MSFSVariableServices.LogLevel = LOGLEVEL.LOG_LEVEL_INFO;
                MSFSVariableServices.Start();
                if (!MSFSVariableServices.IsRunning)
                {
                    Log.Error($"Program: WASM Module is not running! Closing ...");
                    return false;
                }

                elementManager = new ElementManager();

                //Search Memory Locations for Patterns
                scanner.SearchPatterns(elementManager.Patterns.Values.ToList());

                foreach (var pattern in elementManager.Patterns)
                {
                    if (pattern.Value.Location != 0)
                        Log.Information($"Program: Pattern <{pattern.Key}> is at Address 0x{pattern.Value.Location:X} ({pattern.Value.Location:d})");
                    else
                        Log.Error($"Program: Location for Pattern <{pattern.Key}> not found!");
                }

                return true;
            }
            catch
            {
                Log.Logger.Error($"Program: Critical Exception during Wait()");
                cancellationTokenSource.Cancel();
            }

            return false;
        }

        private static bool Initialize()
        {
            if (!FSUIPCConnection.IsOpen && waitForConnect)
                IPCManager.WaitForConnection();
            else
            {
                if (!IPCManager.OpenSafeFSUIPC())
                {
                    return false;
                }
                    
            }
            
            return true;
        }

        private static void Reset()
        {
            guiForm.setState(MyFancyForm.StartState.INIT);
            try
            {
                scanner = null;
                if (elementManager != null)
                    elementManager.Dispose();
                elementManager = null;
                if (FSUIPCConnection.IsOpen)
                    FSUIPCConnection.Close();
                if (MSFSVariableServices.IsRunning)
                    MSFSVariableServices.Stop();
            }
            catch
            {
                Log.Logger.Error($"Program: Exception during Reset()");
            }
        }
    }
}