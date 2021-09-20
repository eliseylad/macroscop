using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace xml_work
{
    class Elem
    {
        public string id { get; set; }
        public string name { get; set; }
        public string sound { get; set; }
        public string parent { get; set; }
        public Elem()
        {

        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            string Url = "http://demo.macroscop.com/configex?login=root";
            List<Elem> elems = new List<Elem>();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
           // XmlReader reader = XmlReader.Create("configex.xml", settings);
            XmlReader reader = XmlReader.Create(Url, settings);
            reader.MoveToContent();
            string name = "";
            while(reader.Read())
            {
                
                if(reader.NodeType==XmlNodeType.Element) 
                {
                    if(reader.Name== "ChannelInfo")
                    {
                        elems.Add(new Elem() { id = reader.GetAttribute("Id"), name = reader.GetAttribute("Name"), sound = reader.GetAttribute("IsSoundOn") });
                    }
                    
                    if(reader.Name== "SecObjectInfo")
                    {
                        name = reader.GetAttribute("Name");
                    }
                    if(reader.Name== "ChannelId")
                    {
                        string str = reader.ReadElementContentAsString();
                        foreach (Elem elem in elems)
                        {
                            if (string.Equals(str, elem.id, StringComparison.Ordinal))
                                elem.parent = name;
                        }
                    }                        
                    
                }    

            }
            
            foreach (Elem elem in elems)
                {
                    Console.WriteLine("Name=" + elem.name + " Sound=" + elem.name + " Parent=" + elem.parent);
                }
            reader.Close();
            Console.ReadKey();
        }
    }
}
