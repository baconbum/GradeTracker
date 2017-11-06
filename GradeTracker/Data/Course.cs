using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;

namespace GradeTracker.Data
{
	public class Course
	{
		public int Id {
			get;
			set;
		}

		public string Name {
			get;
			set;
		}

		public Course(int id, string name)
		{
			Id =	id;
			Name =	name;
		}

		public static List<Course> GetCourses()
		{
			List<Course> courses = new List<Course>();

			SqliteConnection conn = DatabaseConnection.GetConnection();
			conn.Open();
			SqliteCommand command = conn.CreateCommand();

			const string sql =
				"SELECT ID, Name " +
				"FROM Courses";

			command.CommandText = sql;
			SqliteDataReader reader = command.ExecuteReader();

			while(reader.Read())
			{
				int id =		reader.GetInt32(0);
				string name =	reader.GetString(1);

				courses.Add(new Course(id, name));
			}

			// clean up
			reader.Close();
			conn.Close();

			return courses;
		}
	}
}

