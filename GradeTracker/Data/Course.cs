using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;

namespace GradeTracker.Data
{
	public class Course
	{
		public string Name {
			get;
			set;
		}

		public Course(string name)
		{
			Name = name;
		}

		public static List<Course> GetCourses()
		{
			List<Course> courses = new List<Course>();

			SqliteConnection conn = DatabaseConnection.GetConnection();
			conn.Open();
			SqliteCommand command = conn.CreateCommand();

			const string sql =
				"SELECT Name " +
				"FROM Courses";

			command.CommandText = sql;
			SqliteDataReader reader = command.ExecuteReader();

			while(reader.Read())
			{
				string name = reader.GetString(0);

				courses.Add(new Course(name));
			}

			// clean up
			reader.Close();
			conn.Close();

			return courses;
		}
	}
}

