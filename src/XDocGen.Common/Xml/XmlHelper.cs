using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XDocGen.Common.Xml
{
    public static class XmlHelper
    {
        public static void GetMainParts(string xmlDoc, out string summary, out string remarks, out string returns)
        {
            summary = null;
            remarks = null;
            returns = null;

            try
            {
                XDocument xml = XDocument.Parse(xmlDoc);

                if (!xml.Elements().Any(e => e.Name == "member"))
                {
                    return;
                }

                var xSummary = xml.Element("member").Elements().FirstOrDefault(e => e.Name == "summary");
                var xRemarks = xml.Element("member").Elements().FirstOrDefault(e => e.Name == "remarks");
                var xReturns = xml.Element("member").Elements().FirstOrDefault(e => e.Name == "returns");

                if (xSummary != null)
                {
                    summary = xSummary.Value.Trim();
                }

                if (xRemarks != null)
                {
                    remarks = xRemarks.Value.Trim();
                }

                if (xReturns != null)
                {
                    returns = xReturns.Value.Trim();
                }
            }
            catch
            {
            }
        }
    }
}
