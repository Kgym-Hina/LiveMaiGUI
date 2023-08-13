using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using LiveMai;
using LiveMai.Web;

namespace LiveMaiGUI;

public static class InitServices
{
    public static void initFolder()
    {
        Console.Out.WriteLine("[Startup] mkdir-ing");
        try
        {
            Directory.CreateDirectory($"{Environment.GetEnvironmentVariable("HOME")}/VideoCache");
            Directory.CreateDirectory($"{Environment.GetEnvironmentVariable("HOME")}/Records");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    public static bool initStream()
    {
        StartBiliLive.Init();
        StartBiliLive.Login();
        return true;
    }

    public static bool initOBS()
    {
        File.WriteAllText($"{Environment.GetEnvironmentVariable("HOME")}/startup.sh", "nohup obs");
        var obsProc = new ProcessStartInfo("/bin/bash", $"{Environment.GetEnvironmentVariable("HOME")}/startup.sh");
        Process.Start(obsProc);

        Thread.Sleep(5000);

        Console.Out.WriteLine("[Startup] Connecting to OBS");
        Obs.Client.ConnectAsync("ws://127.0.0.1:4455","");

        while (!Obs.Client.IsConnected)
        {
            Console.Out.WriteLine("[Startup] Connecting...");
            Thread.Sleep(500);
        }
        
        Thread.Sleep(5000);
        
        StartBiliLive.StartLive();
        
        Obs.Client.StartStream();
        return true;

    }

    public static bool initCaddyProxy()
    {
        Console.Out.WriteLine("[Startup] Starting Caddy");
        File.WriteAllText($"{Environment.GetEnvironmentVariable("HOME")}/startup_caddy.sh", "nohup caddy reverse-proxy --from :8080 --to :5000");
        var caddyProc = new ProcessStartInfo("/bin/bash", $"{Environment.GetEnvironmentVariable("HOME")}/startup_caddy.sh");
        Process.Start(caddyProc);
        return true;

    }

    public static bool initCaddyFile()
    {
        Console.Out.WriteLine("[Startup] Starting Caddyfile");
        File.WriteAllText($"{Environment.GetEnvironmentVariable("HOME")}/startup_caddyfile.sh", "nohup caddy file-server --listen :9090 --root " +
            $"{Environment.GetEnvironmentVariable("HOME")}/Records/");
        var caddyfileProc = new ProcessStartInfo("/bin/bash", $"{Environment.GetEnvironmentVariable("HOME")}/startup_caddyfile.sh");
        Process.Start(caddyfileProc);
        return true;

    }

    public static bool initWebServer()
    {
        Console.Out.WriteLine("[Startup] Starting RecordServer");
        File.WriteAllText($"{Environment.GetEnvironmentVariable("HOME")}/startup_rec.sh", 
            $"cd {Environment.GetEnvironmentVariable("HOME")}/Recorder/\n" +
            "./RecordAppBlazor");
        var ws = new ProcessStartInfo("/bin/bash", $"{Environment.GetEnvironmentVariable("HOME")}/startup_rec.sh");
        Process.Start(ws);
        return true;

    }
}