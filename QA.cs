using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Runtime.Serialization;


namespace AccesaEmployee
{
    [DataContract]
    public class QA:Employee
	{
        [DataMember]
        private List<string> _testingTools = new List<string>();
        public List<string> TestingTools => _testingTools ;
		public QA(string name, float capacity) : base(name, EmployeePosition.QA, capacity)
		{
		}

        public override void DisplayInfo()
		{
			base.DisplayInfo();
			var sb=new StringBuilder();
			_testingTools.ForEach(x=>sb.Append(x+ ", "));
			Console.WriteLine("Testing tools experience: \r\n {0}", sb);
		}
    }
}
