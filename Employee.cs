using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AccesaEmployee
{
	public abstract class Employee
	{
		private string _name;
		private EmployeePosition _position;
		private float _capacity;//max number of hours per day
		private List<string> _hobbies=new List<string>();

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
        public Employee(XmlReader r) { ReadXml(r); }
        public virtual void WriteXml(XmlWriter w)
        {
            w.WriteElementString("Name", Name);
            w.WriteElementString("Capacity", Capacity.ToString());
            w.WriteElementString("Position", Position.ToString());
            foreach (string hob in Hobbies)
            {
                w.WriteElementString("Hobby", hob);
            }
        }
        public virtual void ReadXml(XmlReader r)
        {
            r.ReadStartElement();
            _name = r.ReadElementContentAsString("name", "");
            _position = (EmployeePosition)r.ReadElementContentAsObject("position", "");
            _capacity = r.ReadElementContentAsFloat("capacity", "");
            foreach (string hob in _hobbies)
            {
                _hobbies = new List<string>() { r.ReadElementContentAsString("hobbie", "") };
            }
            r.ReadEndElement();
        }
        
        
        public virtual void DisplayInfo()
		{
			var sb= new StringBuilder();
			_hobbies.ForEach(x=>sb.Append(x+" "));
			Console.WriteLine($"{_name} ocupa pozitia {_position} si e angajat cu {_capacity} ore pe zi. Lui ii place {sb.ToString()}");
		}
	}
}
