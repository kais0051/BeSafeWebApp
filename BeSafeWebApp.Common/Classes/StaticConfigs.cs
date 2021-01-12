using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using Microsoft.Extensions.Configuration;

namespace BeSafeWebApp.Common
{
    public class StaticConfigs
    {
        //Read key and get value from AppConfig section of AppSettings.json.
        //public static string GetConfig(string keyName)
        //{
        //    var rtnValue = string.Empty;
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //        .AddEnvironmentVariables();

        //    IConfigurationRoot configuration = builder.Build();
        //    var value = configuration["AppConfig:" + keyName];
        //    if (!string.IsNullOrEmpty(value))
        //    {
        //        rtnValue = value;
        //    }
        //    return rtnValue;
        //}

        //Read key and get value from AppSettings section of web.config.
        public static string GetAppSetting(string keyName)
        {
            var rtnString = string.Empty;
            //var configPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Web.config");
            var configPath = Path.Combine(Directory.GetCurrentDirectory(), "Web.config");            
            XmlDocument x = new XmlDocument();
            x.Load(configPath);
            XmlNodeList nodeList = x.SelectNodes("//appSettings/add");
            foreach (XmlNode node in nodeList)
            {
                if (node.Attributes["key"].Value == keyName)
                {
                    rtnString = node.Attributes["value"].Value;
                    break;
                }
            }
            return rtnString;            
        }
    }
}
