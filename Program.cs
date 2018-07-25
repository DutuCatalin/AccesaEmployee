using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace AccesaEmployee
{
	class Program
	{
		static void Main(string[] args)
		{
			var officeManagement= new OfficeManagement();
            var officeManagement1 = new OfficeManagement();
            var officeManagement2 = new OfficeManagement();
            var ds = new DataContractSerializer(typeof(OfficeManagement));
            /*officeManagement.DisplayAllProjects();

			officeManagement.DeleteEmployee(dev);
			officeManagement.DisplayAllEmployees();
			officeManagement.DisplayAllProjects();*/

            PopulateEmployeeList(officeManagement);
			officeManagement.DisplayAllEmployees();
            
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;
            using (XmlWriter w = XmlWriter.Create("xmlfile.xml", settings))
            {
                ds.WriteObject(w, officeManagement);
            }
            using (Stream s = File.OpenRead("xmlfile.xml"))
            {
                officeManagement1 = (OfficeManagement)ds.ReadObject(s);
            }
            officeManagement1.DisplayAllEmployees();

            using (StreamWriter file = File.CreateText(@"C:\Users\Catalin.Oant\Downloads\AccesaEmployee\AccesaEmployee\bin\Debug\json.txt"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, officeManagement);
            }
            //using (Stream s = File.OpenRead(@"C:\Users\Catalin.Oant\Downloads\AccesaEmployee\AccesaEmployee\bin\Debug\json.txt"))
            //{
            //    officeManagement2 = (OfficeManagement)ds.ReadObject(s);
            //}
            //PopulateEmployeeList(officeManagement2);
            //officeManagement2.DisplayAllEmployees();

            Console.ReadLine();

        }

		private static void PopulateEmployeeList(OfficeManagement officeManagement)
		{
			var allInformation = File.ReadAllText(@"c:\Users\Catalin.Oant\Downloads\AccesaEmployee\DOCC.txt");

			var employees = allInformation.Split(new string[] {nameof(Employee),"{", "}"}, StringSplitOptions.RemoveEmptyEntries);
			foreach (var record in employees)
			{
				var trimmedRecord = record.TrimStart(new char[] {'\r', '\n'});
				if (trimmedRecord.StartsWith(nameof(Project)) || trimmedRecord.Equals("\r\n")) continue;
				GetEmployeeFromText(trimmedRecord, officeManagement);
			}
		}

		private static Employee GetEmployeeFromText(string info, OfficeManagement officeMangement)
		{
			if (string.IsNullOrEmpty(info)) return null;
			var lines = info
					.Replace("\t",string.Empty)
					.Trim()
					.Split(new string[]{"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
			if (!lines.Any()) return null;

			var name = GetPropertyValue(nameof(Employee.Name), lines);
			var position= GetPropertyValue(nameof(Employee.Position), lines);
			var capacity = GetPropertyValue(nameof(Employee.Capacity), lines);
			var hobbies = GetPropertyValue(nameof(Employee.Hobbies), lines).Split(',');

			var positionType = EmployeePosition.Intern;
			if (!Enum.TryParse(position, out positionType))
				Console.WriteLine($"Pentru {name} nu s-a putut stabili position type");

			return officeMangement.AddEmployee(name, positionType , Convert.ToSingle(capacity, CultureInfo.InvariantCulture), hobbies.ToList());
		}

		private static string GetPropertyValue(string propertyName, string[] values)
		{
			if (!values.Any()) return string.Empty;
			var adjustedProperty = propertyName + ":";
			return values.SingleOrDefault(x => x.StartsWith(adjustedProperty))
				.TrimStart(adjustedProperty.ToCharArray())
				.TrimEnd("\r\n".ToCharArray());
		}
	}
}
