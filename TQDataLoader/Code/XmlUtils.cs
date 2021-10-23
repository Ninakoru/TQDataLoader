namespace TQDataLoader.Code
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using System.IO;

    public static class XmlUtils
    {
        public static string SerializeObject(Object obj, Type objType)
        {
            var xsSubmit = new XmlSerializer(objType);

            var xml = string.Empty;

            using (var sww = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, obj);
                    xml = sww.ToString();
                }
            }

            return xml;
        }

        public static Object Deserialize(string input, Type objType)
        {
            XmlSerializer ser = null;

            try
            {
                ser = new XmlSerializer(objType);
            }
            catch (Exception)
            { 
                // Expected exception.
            }

            using (StringReader sr = new StringReader(input))
            {
                return ser.Deserialize(sr);
            }
        }
    }
}
