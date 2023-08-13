using System;
using System.Collections.Specialized;
using System.Threading;
using System.Web;
using Newtonsoft.Json;
using OBSWebsocketDotNet.Types;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace LiveMai.Web;

public static class StartBiliLive
{
    public static ChromeDriver driver;
    
    public static void StartLive()
    {
        // flow
        
        driver.Navigate().GoToUrl("https://link.bilibili.com/p/center/index?spm_id_from=333.1007.0.0#/my-room/start-live");
        Thread.Sleep(2000);


        var category =
            driver.FindElement(
                By.ClassName("category-toggle"));
        category.Click();
        
        Thread.Sleep(2000);

        var categoryName = driver.FindElement(By.XPath("/html/body/div[1]/div/main/div/div[1]/div[2]/div/div[1]/section/div[1]/div[1]/div[2]/div[2]/div/div[1]/a"));
        categoryName.Click();


        var start = driver.FindElement(By.XPath("/html/body/div[1]/div/main/div/div[1]/div[2]/div/div[1]/section/div[1]/div[9]/button"));
        start.Click();

        var defaultSrt =
            "srt://live-push.bilivideo.com:1937?streamid=#!::h=live-push.bilivideo.com,r=live-bvc/?streamname=live_12472249_4695452,key={0},schedule=srtts,pflag=1";

// Find the element using relative XPath

        Thread.Sleep(5000);
        IWebElement element = driver.FindElement(By.XPath("//*[@id='live-center-app']/div/main/div/div[1]/div[2]/div/div[1]/section/div[1]/div[6]/div[1]/div[1]/div[2]/div[2]"));

        // Get the value of the 'data-clipboard-text' attribute
        string attributeValue = element.GetAttribute("data-clipboard-text");

        var linkSrt = string.Format(defaultSrt, attributeValue.GetAllMatchedItems("&key=", "&schedule"));

        var settings = Obs.Client.GetStreamServiceSettings();

        settings.Settings.Server = linkSrt;
        settings.Settings.Key = attributeValue;
        
        Obs.Client.SetStreamServiceSettings(settings);
        
        // new StreamingService()
        // {
        //     Settings = new StreamingServiceSettings()
        //     {
        //         Server = linkSrt,
        //         Key = attributeValue,
        //         UseAuth = false
        //     },
        //     Type = "Custom"
        // }
        
        driver.Close();

    }
    
    public static void Login()
    {
        driver.Navigate().GoToUrl("https://passport.bilibili.com/login");

        while (driver.Title == "账号登录")
        {
            Console.Out.WriteLine("logining in");
        }
    }

    public static void Init()
    {
        var a = new DriverManager().SetUpDriver(new ChromeConfig());
        var options = new ChromeOptions();
        options.AddArguments($"user-data-dir={Environment.GetEnvironmentVariable("HOME")}/profile");
        driver = new ChromeDriver(a, options);
    }
}