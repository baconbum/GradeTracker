using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;

namespace GradeTracker.Data
{
	public class Student
	{
		public string FirstName {
			get;
			set;
		}

		public string LastName {
			get;
			set;
		}

		public Student(string firstName, string lastName)
		{
			FirstName = firstName;
			LastName = lastName;
		}

		public static List<Student> GetStudents()
		{
			List<Student> students = new List<Student>();

			SqliteConnection conn = DatabaseConnection.GetConnection();
			conn.Open();
			SqliteCommand command = conn.CreateCommand();

			const string sql =
				"SELECT FirstName, LastName " +
				"FROM Students";
			
			command.CommandText = sql;
			SqliteDataReader reader = command.ExecuteReader();

			while(reader.Read())
			{
				string firstName = reader.GetString(0);
				string lastName = reader.GetString(1);

				students.Add(new Student(firstName, lastName));
			}

			// clean up
			reader.Close();
			conn.Close();

			return students;
		}
	}
}

