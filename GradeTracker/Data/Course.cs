using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;

namespace GradeTracker.Data
{
	/// <summary>
	/// A course.
	/// </summary>
	public class Course
	{
		#region Class properties
		/// <summary>
		/// Gets or sets the course database identifier.
		/// </summary>
		/// <value>The course database identifier.</value>
		public int Id {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the course name.
		/// </summary>
		/// <value>The course name.</value>
		public string Name {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the course start date.
		/// </summary>
		/// <value>The course start date.</value>
		public DateTime StartDate {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the course end date.
		/// </summary>
		/// <value>The course end date.</value>
		public DateTime EndDate {
			get;
			set;
		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="GradeTracker.Data.Course"/> class.
		/// </summary>
		/// <param name="id">Database identifier.</param>
		/// <param name="name">Course name.</param>
		/// <param name="startDate">Course start date.</param>
		/// <param name="endDate">Course end date.</param>
		public Course(int id, string name, DateTime startDate, DateTime endDate)
		{
			Id =		id;
			Name =		name;
			StartDate =	startDate;
			EndDate =	endDate;
		}

		/// <summary>
		/// Retrieve all courses from the database.
		/// </summary>
		/// <returns>A list of all courses.</returns>
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

		/// <summary>
		/// Add a course to the database.
		/// </summary>
		/// <param name="name">Course name.</param>
		/// <param name="startDate">Course start date.</param>
		/// <param name="endDate">Course end date.</param>
		/// <returns>True if course is successfully added, otherwise false.</returns>
		public static bool Add(string name, DateTime startDate, DateTime endDate)
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

		/// <summary>
		/// Delete this course from the database.
		/// </summary>
		/// <returns>True if course is successfully deleted, otherwise false.</returns>
		public bool Delete()
		{
			return Delete(Id);
		}

		/// <summary>
		/// Delete the specified course from the database.
		/// </summary>
		/// <param name="courseId">Course database identifier.</param>
		/// <returns>True if course is successfully deleted, otherwise false.</returns>
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

		/// <summary>
		/// Edit this course in the database.
		/// </summary>
		/// <param name="name">New course name.</param>
		/// <param name="startDate">New course start date.</param>
		/// <param name="endDate">New course end date.</param>
		/// <returns>True if course is successfully edited, otherwise false.</returns>
		public bool Edit(string name, DateTime startDate, DateTime endDate)
		{
			return Edit(Id, name, startDate, endDate);
		}

		/// <summary>
		/// Edit the specified course in the database.
		/// </summary>
		/// <param name="courseId">Course identifier.</param>
		/// <param name="name">New course name.</param>
		/// <param name="startDate">New course start date.</param>
		/// <param name="endDate">New course end date.</param>
		/// <returns>True if course is successfully edited, otherwise false.</returns>
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

		/// <summary>
		/// Get a list of students with the course's enrollment status.
		/// </summary>
		/// <returns>A list of students with the course's enrollment status.</returns>
		public List<CourseStudent> GetEnrollment()
		{
			List<CourseStudent> students = new List<CourseStudent>();

			SqliteConnection conn = DatabaseConnection.GetConnection();
			conn.Open();
			SqliteCommand command = conn.CreateCommand();

			const string enrollmentSqlFormat =
				"SELECT s.ID, s.FirstName, s.LastName, " +
				"CASE WHEN sc.CourseID IS NOT NULL THEN 1 ELSE 0 END " +
				"FROM Students s " +
				"LEFT JOIN (SELECT * FROM StudentCourses WHERE CourseID = {0}) sc " +
				"ON s.ID = sc.StudentID";

			command.CommandText = String.Format(enrollmentSqlFormat, Id);
			SqliteDataReader reader = command.ExecuteReader();

			while(reader.Read())
			{
				int studentId =			reader.GetInt32(0);
				string firstName =		reader.GetString(1);
				string lastName =		reader.GetString(2);
				bool isEnrolled =		reader.GetBoolean(3);

				students.Add(new CourseStudent(studentId, firstName, lastName, Id, isEnrolled));
			}

			// clean up
			reader.Close();
			conn.Close();

			return students;
		}

		/// <summary>
		/// Enrolls the specified student in the course.
		/// </summary>
		/// <returns><c>true</c>, if in student was enrolled, <c>false</c> otherwise.</returns>
		/// <param name="studentId">Student database identifier.</param>
		public bool EnrollStudent(int studentId)
		{
			SqliteConnection conn = DatabaseConnection.GetConnection();
			conn.Open();
			SqliteCommand command = conn.CreateCommand();

			const string enrollmentSqlFormat =
				"INSERT INTO StudentCourses (StudentID, CourseID) " +
				"VALUES ('{0}', '{1}')";

			command.CommandText = String.Format(enrollmentSqlFormat, studentId, Id);

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
		/// Withdraws the specified student from the course.
		/// </summary>
		/// <returns><c>true</c>, if from student was withdrawn, <c>false</c> otherwise.</returns>
		/// <param name="studentId">Student database identifier.</param>
		public bool WithdrawStudent(int studentId)
		{
			SqliteConnection conn = DatabaseConnection.GetConnection();
			conn.Open();
			SqliteCommand command = conn.CreateCommand();

			const string withdrawlSqlFormat =
				"DELETE FROM StudentCourses " +
				"WHERE StudentID = {0} " +
				"AND CourseID = {1}";

			command.CommandText = String.Format(withdrawlSqlFormat, studentId, Id);

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

		public List<GradeableTask> GetTasks()
		{
			List<GradeableTask> tasks = new List<GradeableTask>();

			SqliteConnection conn = DatabaseConnection.GetConnection();
			conn.Open();
			SqliteCommand command = conn.CreateCommand();

			const string selectTasksSqlFormat =
				"SELECT ID, Name, DueDate, PotentialMarks, Weight " +
				"FROM GradeableTasks " +
				"WHERE CourseID = {0}";

			command.CommandText = String.Format(selectTasksSqlFormat, Id);
			SqliteDataReader reader = command.ExecuteReader();

			while(reader.Read())
			{
				int id =				reader.GetInt32(0);
				string name =			reader.GetString(1);
				DateTime dueDate =		reader.GetDateTime(2);
				double potentialMarks =	reader.GetDouble(3);
				double weight =			reader.GetDouble(4);

				tasks.Add(new GradeableTask(id, this.Id, name, dueDate, potentialMarks, weight));
			}

			// clean up
			reader.Close();
			conn.Close();

			return tasks;
		}

		public bool AddGradeableTask(string name, DateTime dueDate, double potentialMarks, double weight)
		{
			return GradeableTask.Add(Id, name, dueDate, potentialMarks, weight);
		}
	}
}
