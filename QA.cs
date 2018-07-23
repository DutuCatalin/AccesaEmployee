using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json.Linq;


namespace AccesaEmployee
{
	public class QA:Employee
	{
		private List<string> _testingTools = new List<string>();
		public List<string> TestingTools => _testingTools ;
		public QA(string name, float capacity) : base(name, EmployeePosition.QA, capacity)
		{
		}
        public override void WriteXml(XmlWriter w)
        {
            base.WriteXml(w);
            foreach (string testingtool in TestingTools)
            {
                w.WriteElementString("Testing tools", testingtool);
            }
        }
        public override void DisplayInfo()
		{
			base.DisplayInfo();
			var sb=new StringBuilder();
			_testingTools.ForEach(x=>sb.Append(x+ ", "));
			Console.WriteLine("Testing tools experience: \r\n {0}", sb);
		}
        public override void ReadXml(XmlReader r)
        {
            base.ReadXml(r);
            foreach (string testingtool in TestingTools)
            {
                _testingTools = new List<string>() { r.ReadElementContentAsString("Testing tools", "") };
            }
        }
        public override void PropertyJ(JObject r)
        {
            base.PropertyJ(r);
            foreach (string testingtool in TestingTools)
            {
                r = new JObject(new JProperty("Testing tools ", _testingTools));
            }
        }
    }
}
