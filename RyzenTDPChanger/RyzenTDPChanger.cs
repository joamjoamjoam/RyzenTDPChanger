// Big Box Play and Kill Plugin for Launchbox
// Adds a context menu option to each game allowing the user to kill Big Box after starting the game.
// Author: Don Freiday
// 05/11/2017

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace RyzenTDPChanger
{

    // TODO  Add Default TDP by Platform to Settings Dialog
    // TODO Add ability to Set Ryzen app in Settings Dialog
    // TODO Implement setTDP Function


    public enum AppType
    {
        NONE,
        LAUNCHBOX,
        BIGBOX
    }
    public class RyzenTDPChanger : IGameLaunchingPlugin, ISystemMenuItemPlugin, ISystemEventsPlugin
    {
        String ryzenAdjPath = "";
        bool tdpChangesEnabled = false;
        byte defaultTDP = 15;
        byte bigBoxTDP = 15;
        byte launchBoxTDP = 15;
        AppType appl = AppType.NONE;
        Dictionary<String, Byte> platformDict = new Dictionary<string, byte>();

        public string Caption
        {
            get { return "Set TDP Options for Ryzen ..."; }
        }

        public Image IconImage
        {
            get { return null; }
        }

        public bool ShowInLaunchBox
        {
            get { return true; }
        }

        public bool ShowInBigBox
        {
            get { return true; }
        }

        public bool AllowInBigBoxWhenLocked
        {
            get { return false; }
        }

        public void OnAfterGameLaunched(IGame game, IAdditionalApplication app, IEmulator emulator)
        {
            loadSettings();

            byte tdp = defaultTDP; // Global Game TDP

            // Check Platform TDP Field

            if (platformDict.ContainsKey(game.Platform.ToLower()))
            {
                // Override Global for Platform
                defaultTDP = platformDict[game.Platform.ToLower()];
                //System.Windows.Forms.MessageBox.Show($"Platform TDP is {defaultTDP}");
            }

            // Load Games TDP Field
            ICustomField tdpField = (game != null) ? game.GetAllCustomFields().Where(field => field.Name == "TDP").FirstOrDefault() : default(ICustomField);

            
            if (tdpField != null)
            {
                
                if (tdpField.Value != null)
                {
                    if (!byte.TryParse(tdpField.Value, out tdp) || tdp <= 0 || tdp > 100){
                        // Field is Invalid (Why cant we have CustomField Types??) just use default TDP
                        tdp = defaultTDP;
                    }
                }
            }

            setTDP(ryzenAdjPath, tdp);
        }

        public void OnBeforeGameLaunching(IGame game, IAdditionalApplication app, IEmulator emulator)
        {
        }

        public void OnGameExited()
        {
            loadSettings();

            // Wait for Steam Games to Exit.
            steamAppCounter("Game Not Available", mode: "attach");

            // Wait for Epic Games to Exit.


            // Set Big Box TDP to Default = 15w TDP

            System.Windows.Forms.MessageBox.Show($"Returning to UI TDP.");
            if (PluginHelper.StateManager.IsBigBox)
            {
                setTDP(ryzenAdjPath, bigBoxTDP);
            }
            else
            {
                setTDP(ryzenAdjPath, launchBoxTDP);
            }
        }



        private bool setTDP(String ryzenPath, byte tdp, bool overrideCheck = false)
        {
            bool bSuccess = false;

            if (tdpChangesEnabled || overrideCheck)
            {
                if (File.Exists(ryzenPath))
                {
                    if (tdp > 0 && tdp < 100)
                    {
                        // Set TDP Here
                        //System.Windows.Forms.MessageBox.Show($"Setting TDP to {tdp} with {ryzenPath}");

                        UInt32 effectiveTDP = tdp * 1000u;
                        String process = $"\"{ryzenPath}\" --stapm-limit={effectiveTDP} --fast-limit={effectiveTDP} --slow-limit={effectiveTDP}";
                        System.Windows.Forms.MessageBox.Show(process);
                        try
                        {
                            var proc1 = new ProcessStartInfo();
                            proc1.UseShellExecute = true;

                            proc1.WorkingDirectory = @"C:\Windows\System32";

                            proc1.FileName = @"C:\Windows\System32\cmd.exe";
                            proc1.Verb = "runas";
                            proc1.Arguments = "/c " + process;
                            proc1.WindowStyle = ProcessWindowStyle.Hidden;
                            Process.Start(proc1);
                        }
                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message, "Error Running RyzenAdj");
                        }
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show($"Unable to Access RyzenAdj at: {ryzenAdjPath}", "Failed to Set TDP");
                }
            }

            return bSuccess;
        }

        private void loadSettings()
        {
            // load settings from json

            if (File.Exists(getSettingsFile()))
            {
                List<String> platforms = PluginHelper.DataManager.GetAllPlatforms().Select(plat => plat.Name.ToLower()).ToList();

                if (platforms != null && platforms.Count > 0)
                {
                    foreach (string platform in platforms)
                    {
                        if (!platformDict.ContainsKey(platform.ToLower()))
                        {
                            platformDict.Add(platform, 0);
                        }
                    }
                }

                try
                {
                    JObject saveObj = JObject.Parse(File.ReadAllText(getSettingsFile()));

                    try
                    {
                        ulong tmp = saveObj["launchBoxTDP"].Value<byte>();

                        if (tmp > 0 && tmp <= 100)
                        {
                           launchBoxTDP = (byte)tmp;
                        }
                        else
                        {
                            launchBoxTDP = 10;
                        }
                    }
                    catch
                    {
                        launchBoxTDP = 10;
                    }


                    try
                    {
                        byte tmp = saveObj["defaultGameTDP"].Value<byte>();

                        if (tmp > 0 && tmp <= 100)
                        {
                            defaultTDP = tmp;
                        }
                        else
                        {
                            defaultTDP = 10;
                        }
                    }
                    catch
                    {
                        defaultTDP = 10;
                    }

                    try
                    {
                        byte tmp = saveObj["bigBoxTDP"].Value<byte>();

                        if (tmp > 0 && tmp <= 100)
                        {
                            bigBoxTDP = tmp;
                        }
                        else
                        {
                            bigBoxTDP = 10;
                        }
                    }
                    catch
                    {
                        bigBoxTDP = 10;
                    }

                    try
                    {
                        bool tmp = saveObj["ryzenAdjEnabled"].Value<bool>();

                        tdpChangesEnabled = tmp;
                    }
                    catch
                    {
                        tdpChangesEnabled = false;
                    }

                    try
                    {
                        String tmp = saveObj["ryzenAdjPath"].Value<String>();
                        ryzenAdjPath = tmp;
                    }
                    catch
                    {
                        ryzenAdjPath = "";
                    }

                    try
                    {
                        JObject platformObj = saveObj["platforms"].Value<JObject>();


                        foreach (KeyValuePair<String, Byte> kp in platformDict)
                        {
                            try
                            {
                                byte tmp = platformObj[kp.Key].Value<Byte>();
                                if (tmp > 0 && tmp <= 100)
                                {
                                    platformDict[kp.Key] = tmp;
                                }
                                else
                                {
                                    platformDict[kp.Key] = 0;
                                }
                            }
                            catch
                            {
                                // use 0 as default
                            }
                        }
                    }
                    catch
                    {
                        // Defaults setup in constructor
                    }

                }
                catch
                {
                   //System.Windows.Forms.MessageBox.Show("TDP Settings JSON is Invalid. TDP is unchanged");
                }
                //System.Windows.Forms.MessageBox.Show($"Loaded Settings: LB: {launchBoxTDP}, BB: {bigBoxTDP}, Default: {defaultTDP}");
            }
        }

        // ISystemMenuItem Interface Methods

        public void OnSelected()
        {
            String[] platforms = PluginHelper.DataManager.GetAllPlatforms().Select(p => p.Name.ToLower()).ToArray();

            SettingDialog dlg = new SettingDialog(getSettingsFile(), platforms);
            dlg.Show();
        }

        public void OnEventRaised(string eventType)
        {
            switch (eventType)
            {
                case SystemEventTypes.BigBoxStartupCompleted:
                    appl = AppType.BIGBOX;
                    break;

                case SystemEventTypes.LaunchBoxStartupCompleted:
                    appl = AppType.LAUNCHBOX;
                    break;
            }
        }

        public String getSettingsFile()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\tdpSettings.json";
        }

        public object steamAppCounter(String appName = "", String mode = "count")
        {
            // mode == "count" return int count of all open steam processes
            // mode == "attach" return null and loop until all steam processes are closed. 

            object rv = null;

            int exitCode = 0;
            List<String> file = new List<string>();
            try
            {
                List<Process> procs = Process.GetProcesses().ToList();
                Process steam = procs.Find(x => x.ProcessName.ToLower() == "steam");

                file.Add($"Mode: {mode}");
                file.Add($"App Name: {appName}");


                List<Process> procIdList = new List<Process>();
                if (steam != null)
                {
                    file.Add($"Steam ID: {steam.Id}");
                    int steamId = steam.Id;

                    foreach (var proc in procs)
                    {
                        using (ManagementObject mo = new ManagementObject($"win32_process.handle='{proc.Id}'"))
                        {
                            if (mo != null)
                            {
                                try
                                {
                                    mo.Get();
                                    int parentPid = Convert.ToInt32(mo["ParentProcessId"]);
                                    if (parentPid == steamId)
                                    {
                                        Console.Out.WriteLine($"{proc.ProcessName} is running as a child to {mo["ParentProcessId"]}");
                                        if (proc.ProcessName.ToLower() != "steamwebhelper")
                                        {
                                            if (proc.ProcessName.ToLower() == "gameoverlayui")
                                            {
                                                procIdList.Add(proc);
                                                Console.Out.WriteLine($"{proc.ProcessName} was added to the running game count.");
                                                // Attach onto Process and wait for it to exit


                                            }

                                        }
                                    }

                                }
                                catch (Exception ex)
                                {
                                    // the process ended between polling all of the procs and requesting the management object
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.Out.WriteLine($"Steam is not Running");
                }
                Console.Out.WriteLine($"Games Running: {procIdList.Count}");

                switch (mode)
                {
                    case "attach":
                        // Hold Until Process Terminate
                        Console.Out.WriteLine($"Attaching to Games Running:\n{String.Join("\n", procIdList.Select(p => $"{p.Id} - {p.ProcessName}"))}");
                        file.Add($"Attaching to Games Running:\n{String.Join("\n", procIdList.Select(p => $"{p.Id} - {p.ProcessName}"))}");
                        while (procIdList.Count > 0)
                        {
                            if (!procIdList[0].HasExited)
                            {
                                procIdList[0].WaitForExit();
                            }
                            procIdList.RemoveAt(0);
                        }
                        file.Add("Done Waiting on Attached Processes. Exiting Now.");
                        exitCode = 0;
                        rv = exitCode;
                        break;
                    case "count":
                    default:
                        file.Add($"Counted {procIdList.Count} Steam Processes Running");
                        exitCode = procIdList.Count;
                        rv = exitCode;
                        break;
                }
            }
            catch (Exception ex)
            {
                file.Add(ex.Message);
            }

            File.AppendAllLines(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\steamAppCounter.log", file);

            return rv;
        }
    }
}
