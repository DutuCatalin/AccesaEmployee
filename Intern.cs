using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AccesaEmployee
{
    [DataContract]
    public class Intern:Employee
	{
        [DataMember]
        private readonly string _universityName;
        [DataMember]
        private readonly int _yearOfStudy;
        [DataMember]
        private readonly EmployeePosition _targetPosition;

		public string UniversityName => _universityName;
		public int YearOfStudy => _yearOfStudy;
		public EmployeePosition TargetPosition => _targetPosition;
		public Intern(string name, float capacity) 
			: base(name, EmployeePosition.Intern, capacity)
		{
		}
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"University : {_universityName} \nYear: {_yearOfStudy} \nTarget position : {_targetPosition}");
        }

    }
}
