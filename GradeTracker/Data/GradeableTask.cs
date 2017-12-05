using System;
using Mono.Data.Sqlite;

namespace GradeTracker.Data
{
	public class GradeableTask
	{
		#region Class properties
		public int Id {
			get;
			set;
		}

		public int CourseId {
			get;
			set;
		}

		public string Name {
			get;
			set;
		}

		public DateTime DueDate {
			get;
			set;
		}

		public double PotentialMarks {
			get;
			set;
		}

		public double Weight {
			get;
			set;
		}
		#endregion

		public GradeableTask(int id, int courseId, string name, DateTime dueDate, double potentialMarks, double weight)
		{
			Id =				id;
			CourseId =			courseId;
			Name =				name;
			DueDate =			dueDate;
			PotentialMarks =	potentialMarks;
			Weight =			weight;
		}

		/// <summary>
		/// Add a task to the database.
		/// </summary>
		/// <param name="courseId">Associated course database identifier.</param>
		/// <param name="name">Task name.</param>
		/// <param name="dueDate">Task due date.</param>
		/// <param name="potentialMarks">The maximum attainable marks for the task.</param>
		/// <param name="weight">The weight of the task.</param>
		public static bool Add(int courseId, string name, DateTime dueDate, double potentialMarks, double weight)
		{
			SqliteConnection conn = DatabaseConnection.GetConnection();
			conn.Open();
			SqliteCommand command = conn.CreateCommand();

			const string taskInsertFormat =
				"INSERT INTO GradeableTasks(CourseID, Name, DueDate, PotentialMarks, Weight) " +
				"VALUES ('{0}', '{1}', '{2}', {3}, {4})";

			command.CommandText = String.Format(taskInsertFormat, courseId, name, dueDate.ToString("u"), potentialMarks, weight);

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

		/// <summary>
		/// Delete this task from the database.
		/// </summary>
		/// <returns>True if task is successfully deleted, otherwise false.</returns>
		public bool Delete()
		{
			return Delete(Id);
		}

		/// <summary>
		/// Delete the specified task from the database.
		/// </summary>
		/// <param name="taskId">Task database identifier.</param>
		/// <returns>True if task is successfully deleted, otherwise false.</returns>
		public static bool Delete(int taskId)
		{
			SqliteConnection conn = DatabaseConnection.GetConnection();
			conn.Open();
			SqliteCommand command = conn.CreateCommand();

			const string taskDeleteFormat =
				"DELETE FROM GradeableTasks " +
				"WHERE ID = {0}";

			command.CommandText = String.Format(taskDeleteFormat, taskId);

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

		public bool Edit(string name, DateTime dueDate, double potentialMarks, double weight)
		{
			return Edit(Id, name, dueDate, potentialMarks, weight);
		}

		public static bool Edit(int taskId, string name, DateTime dueDate, double potentialMarks, double weight)
		{
			SqliteConnection conn = DatabaseConnection.GetConnection();
			conn.Open();
			SqliteCommand command = conn.CreateCommand();

			const string taskUpdateFormat =
				"UPDATE GradeableTasks " +
				"SET Name = '{1}', " +
				"DueDate = '{2}', " +
				"PotentialMarks = {3}, " +
				"Weight = {4} " +
				"WHERE ID = {0}";

			command.CommandText = String.Format(taskUpdateFormat, taskId, name, dueDate.ToString("u"), potentialMarks, weight);

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
