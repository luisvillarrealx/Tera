using System.Xml;

namespace Tera_API.Models
{
    public class Logs
    {
        public static class WriteLog
        {
            public static void Log(string Source, string desError, string sLocalPath, string Params)
            {
                Console.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " = " + Source + ":" + desError);

                try
                {
                    XmlDocument xml;
                    XmlElement xmlE;
                    string sFile = "Log_" + SimpleDate(DateTime.Now, false, false) + ".xml";

                    xml = new XmlDocument();
                    try
                    {
                        xml.Load(Path.Combine(sLocalPath, sFile));
                    }
                    catch (Exception)
                    {
                        xml = new XmlDocument();
                        xml.AppendChild(xml.CreateElement("LOGS"));
                    }

                    xmlE = xml.CreateElement("LOG");
                    xmlE.SetAttribute("DATERUN", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                    xmlE.SetAttribute("LOG", Source + ": " + desError);
                    xmlE.SetAttribute("PARAMS", Params);
                    xml.DocumentElement.AppendChild(xmlE);
                    xml.Save(System.IO.Path.Combine(sLocalPath, sFile));
                }
                catch (Exception ex)
                {
                }
            }

            private static string SimpleDate(DateTime dDate, bool DMY = false, bool WithTime = true)
            {
                string sDate = string.Empty;
                string sTime = string.Empty;

                if (!DMY)
                    sDate = dDate.Year.ToString() + dDate.Month.ToString().PadLeft(2, '0') + dDate.Day.ToString().PadLeft(2, '0');
                else
                    sDate = dDate.Day.ToString().PadLeft(2, '0') + dDate.Month.ToString().PadLeft(2, '0') + dDate.Year.ToString();

                if (WithTime)
                {
                    sTime = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');
                    return sDate + " " + sTime;
                }
                else
                    return sDate;
            }
        }
    }
}
