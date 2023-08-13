using System;
using Avalonia.Controls;
using Avalonia.Input;

namespace LiveMaiGUI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public override void Show()
    {
        base.Show();
        InitService();

    }

    private async void InitService()
    {
        
        try
        {
            var streamService = InitServices.initStream();
            Stream.Text = streamService ? "GOOD" : "BAD   ";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Stream.Text = "ERRO";
        }
        
        try
        {
            var streamService = InitServices.initOBS();
            Obs.Text = streamService ? "GOOD" : "BAD   ";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Obs.Text = "ERRO";
        }
        
        try
        {
            var streamService = InitServices.initWebServer();
            Web.Text = streamService ? "GOOD" : "BAD   ";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Web.Text = "ERRO";
        }
        
        try
        {
            var streamService = InitServices.initCaddyFile();
            Caddy1.Text = streamService ? "GOOD" : "BAD   ";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Caddy1.Text = "ERRO";
        }
        
        try
        {
            var streamService = InitServices.initCaddyProxy();
            Caddy2.Text = streamService ? "GOOD" : "BAD   ";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Caddy2.Text = "ERRO";
        }
    }
}