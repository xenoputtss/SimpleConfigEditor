using System.Linq;
using System.Xml.Linq;

namespace SimpleConfigEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            var doc = XDocument.Load($@"C:\Program Files\365POS\KioskController.exe.config");

            var node = doc.Element("configuration")
                .Element("KioskExecutables")
                .Element("Executables");

            node.Elements("add").Where(e => e.Attribute("appName").Value == "Sync").Remove();
            node.Elements("add").Where(e => e.Attribute("appName").Value == "Nsync").Remove();
            node.Elements("add").Where(e => e.Attribute("appName").Value == "SyncDashboard").Remove();
            node.Add(
                new XElement("add",
                    new XAttribute("appName", "Nsync"),
                    new XAttribute("exeName", "Kiosk_Sync.exe"),
                    new XAttribute("startOrder", "1"),
                    new XAttribute("startMinimized", "true"),
                    new XAttribute("workingDirectory", ".\\Nsync")
            ));
            node.Add(
                new XElement("add",
                    new XAttribute("appName", "SyncDashboard"),
                    new XAttribute("exeName", "SyncDashboard.exe"),
                    new XAttribute("startOrder", "2"),
                    new XAttribute("startMinimized", "true"),
                    new XAttribute("workingDirectory", ".\\NSYNCDashboard")
            ));
            doc.Save($@"C:\Program Files\365POS\KioskController.exe.config");
        }
    }
}
