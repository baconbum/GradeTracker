using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;

namespace GradeTracker.Data
{
	public class Student
	{
		public int Id {
			get;
			set;
		}

		public string FirstName {
			get;
			set;
		}

		public string LastName {
			get;
			set;
		}

		public Student(int id, string firstName, string lastName)
		{
			Id =		id;
			FirstName =	firstName;
			LastName =	lastName;
		}

		public static List<Student> GetStudents()
		{
			List<Student> students = new List<Student>();

			SqliteConnection conn = DatabaseConnection.GetConnection();
			conn.Open();
			SqliteCommand command = conn.CreateCommand();

			const string sql =
				"SELECT ID, FirstName, LastName " +
				"FROM Students";
			
			command.CommandText = sql;
			SqliteDataReader reader = command.ExecuteReader();

			while(reader.Read())
			{
				int id =			reader.GetInt32(0);
				string firstName =	reader.GetString(1);
				string lastName =	reader.GetString(2);

				students.Add(new Student(id, firstName, lastName));
			}

			// clean up
			reader.Close();
			conn.Close();

			return students;
		}
	}
}

