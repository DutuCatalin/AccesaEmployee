using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace AccesaEmployee
{
    [DataContract , KnownType(typeof(Intern)), KnownType (typeof(QA)), KnownType(typeof(Dev))]
	public  class Employee
	{
        [DataMember]
		private readonly string _name;
        [DataMember]
		private readonly EmployeePosition _position;
        [DataMember]
        private readonly float _capacity;//max number of hours per day
        [DataMember]
        private readonly List<string> _hobbies=new List<string>();

		public string Name => _name;
		public EmployeePosition Position => _position;
		public float Capacity => _capacity;
		public List<string> Hobbies => _hobbies;

		protected Employee(string name, EmployeePosition position, float capacity)
		{
			_name = name;
			_position = position;
			_capacity = capacity;
		}
        
        public virtual void DisplayInfo()
		{
			var sb= new StringBuilder();
			_hobbies.ForEach(x=>sb.Append(x+" "));
			Console.WriteLine($"{_name} ocupa pozitia {_position} si e angajat cu {_capacity} ore pe zi. Lui ii place {sb.ToString()}");
		}
	}
}
