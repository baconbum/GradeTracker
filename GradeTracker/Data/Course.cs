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

		public DateTime StartDate {
			get;
			set;
		}
		public DateTime EndDate {
			get;
			set;
		}

		public Course(int id, string name, DateTime startDate, DateTime endDate)
		{
			Id =		id;
			Name =		name;
			StartDate =	startDate;
			EndDate =	endDate;
		}

		public static List<Course> GetCourses()
		{
			List<Course> courses = new List<Course>();

			SqliteConnection conn = DatabaseConnection.GetConnection();
			conn.Open();
			SqliteCommand command = conn.CreateCommand();

			const string sql =
				"SELECT ID, Name, StartDate, EndDate " +
				"FROM Courses";

			command.CommandText = sql;
			SqliteDataReader reader = command.ExecuteReader();

			while(reader.Read())
			{
				int id =				reader.GetInt32(0);
				string name =			reader.GetString(1);
				DateTime startDate =	reader.GetDateTime(2);
				DateTime endDate =		reader.GetDateTime(3);

				courses.Add(new Course(id, name, startDate, endDate));
			}

			// clean up
			reader.Close();
			conn.Close();

			return courses;
		}

		public static bool AddCourseToDatabase(string name, DateTime startDate, DateTime endDate)
		{
			SqliteConnection conn = DatabaseConnection.GetConnection();
			conn.Open();
			SqliteCommand command = conn.CreateCommand();

			const string courseInsertFormat =
				"INSERT INTO Courses(Name, StartDate, EndDate) " +
				"VALUES ('{0}', '{1}', '{2}')";

			command.CommandText = String.Format(courseInsertFormat, name, startDate.ToString("u"), endDate.ToString("u"));

			try {
				command.ExecuteNonQuery();
				return true;
			}
			catch (Exception) {
				return false;
			}
			finally {
				conn.Close();
			}
		}

		public bool Delete()
		{
			return Delete(Id);
		}

		public static bool Delete(int courseId)
		{
			SqliteConnection conn = DatabaseConnection.GetConnection();
			conn.Open();
			SqliteCommand command = conn.CreateCommand();

			const string courseDeleteFormat =
				"DELETE FROM Courses " +
				"WHERE ID = {0}";

			command.CommandText = String.Format(courseDeleteFormat, courseId);

			try {
				command.ExecuteNonQuery();
				return true;
			}
			catch (Exception) {
				return false;
			}
			finally {
				conn.Close();
			}
		}

		public bool Edit(string name, DateTime startDate, DateTime endDate)
		{
			return Edit(Id, name, startDate, endDate);
		}

		public static bool Edit(int courseId, string name, DateTime startDate, DateTime endDate)
		{
			SqliteConnection conn = DatabaseConnection.GetConnection();
			conn.Open();
			SqliteCommand command = conn.CreateCommand();

			const string courseUpdateFormat =
				"UPDATE Courses " +
				"SET Name = '{1}', " +
				"StartDate = '{2}', " +
				"EndDate = '{3}' " +
				"WHERE ID = {0}";

			command.CommandText = String.Format(courseUpdateFormat, courseId, name, startDate.ToString("u"), endDate.ToString("u"));

			try {
				command.ExecuteNonQuery();
				return true;
			}
			catch (Exception) {
				return false;
			}
			finally {
				conn.Close();
			}
		}
	}
}

