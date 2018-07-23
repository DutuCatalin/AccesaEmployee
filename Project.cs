using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AccesaEmployee
{
	public class Project
	{
		private string _name;
		private string _description;
		private DateTime _deadLine;
		private Dictionary<Employee, float> _team = new Dictionary<Employee, float>();

		public string Name => _name;
		public string Description => _description;
		public DateTime DeadLine => _deadLine;
		public IReadOnlyDictionary<Employee, float> Team => _team;

		public Project(string name, string description, DateTime deadLine)
		{
			_name = name;
			_description = description;
			_deadLine = deadLine;
		}

        public Project(XmlReader r) { ReadXml(r); }
        public virtual void ReadXml(XmlReader r)
        {
            r.ReadStartElement();
            _name = r.ReadElementContentAsString("name", "");
            _description = r.ReadElementContentAsString("description", "");
            _deadLine = Convert.ToDateTime(r.ReadElementContentAsString("deadLine", ""));
            foreach (var teamMember in _team)
            {
            }
            r.ReadEndElement();
        }
        public void WriteXml(XmlWriter w)
        {
            w.WriteElementString("Name", Name);
            w.WriteElementString("Description", Description);
            w.WriteElementString("DeadLine", DeadLine.ToString());

        }

        public void DisplayInfo()
		{
			Console.WriteLine($"Project {_name} that is valid until {_deadLine.ToShortDateString()} is about {_description} and has the following team members:");
			var sb =new StringBuilder();
			foreach (var teamMember in _team)
			{
				sb.AppendLine($"{teamMember.Key.Name} with {teamMember.Value} hours");
			}

			Console.WriteLine(sb);
		}

		public void AddTeamMember(Employee employee, float capacity)
		{
			if (!_team.ContainsKey(employee))
				_team.Add(employee, capacity);
		}

		public void DeleteTeamMember(Employee employee)
		{
			if (_team.ContainsKey(employee))
				_team.Remove(employee);
		}
	}
}
