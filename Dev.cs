using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AccesaEmployee
{
	public class Dev:Employee
	{
		private List<string> _technologyStack = new List<string>();

		public List<string> TechnologyStack => _technologyStack;
		public Dev(string name, float capacity) 
			: base(name, EmployeePosition.DEV, capacity)
		{
		}
        public override void WriteXml(XmlWriter w)
        {
            base.WriteXml(w);
            foreach (string techstack in TechnologyStack)
            {
                w.WriteElementString("Technology stack", techstack);
            }
        }

        public override void DisplayInfo()
		{
			base.DisplayInfo();
			var sb = new StringBuilder();
			_technologyStack.ForEach(x => sb.Append(x + ", "));
			Console.WriteLine("Technology stack: \r\n {0}", sb);
		}
        public override void ReadXml(XmlReader r)
        {
            base.ReadXml(r);
            foreach (string testingtool in TechnologyStack)
            {
                _technologyStack = new List<string>() { r.ReadElementContentAsString("Technology stack", "") };
            }
        }
    }
}
