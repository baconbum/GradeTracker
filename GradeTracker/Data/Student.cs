using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;

namespace GradeTracker.Data
{
	/// <summary>
	/// A student
	/// </summary>
	public class Student
	{
		#region Class properties
		/// <summary>
		/// Gets or sets the student database identifier.
		/// </summary>
		/// <value>The student database identifier.</value>
		public int Id {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the student's first name.
		/// </summary>
		/// <value>The student's first name.</value>
		public string FirstName {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the student's last name.
		/// </summary>
		/// <value>The student's last name.</value>
		public string LastName {
			get;
			set;
		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="GradeTracker.Data.Student"/> class.
		/// </summary>
		/// <param name="id">Database identifier.</param>
		/// <param name="firstName">Student's first name.</param>
		/// <param name="lastName">Student's last name.</param>
		public Student(int id, string firstName, string lastName)
		{
			Id =		id;
			FirstName =	firstName;
			LastName =	lastName;
		}

		/// <summary>
		/// Retrieves all students from the database.
		/// </summary>
		/// <returns>A list of all students.</returns>
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

		/// <summary>
		/// Add a student to the database.
		/// </summary>
		/// <param name="firstName">Student's first name.</param>
		/// <param name="lastName">Student's last name.</param>
		/// <returns>True if student is successfully added, otherwise false.</returns>
		public static bool Add(string firstName, string lastName)
		{
			SqliteConnection conn = DatabaseConnection.GetConnection();
			conn.Open();
			SqliteCommand command = conn.CreateCommand();

			const string studentInsertFormat =
				"INSERT INTO Students(FirstName, LastName) " +
				"VALUES ('{0}', '{1}')";

			command.CommandText = String.Format(studentInsertFormat, firstName, lastName);

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
		/// Delete this student from the database.
		/// </summary>
		/// <returns>True if student is successfully deleted, otherwise false.</returns>
		public bool Delete()
		{
			return Delete(Id);
		}

		/// <summary>
		/// Delete the specified student from the database.
		/// </summary>
		/// <param name="studentId">Student database identifier.</param>
		/// <returns>True if student is successfully deleted, otherwise false.</returns>
		public static bool Delete(int studentId)
		{
			SqliteConnection conn = DatabaseConnection.GetConnection();
			conn.Open();
			SqliteCommand command = conn.CreateCommand();

			const string studentDeleteFormat =
				"DELETE FROM Students " +
				"WHERE ID = {0}";

			command.CommandText = String.Format(studentDeleteFormat, studentId);

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
		/// Edit this student in the database.
		/// </summary>
		/// <param name="firstName">Student's new first name.</param>
		/// <param name="lastName">Student's new last name.</param>
		/// <returns>True if student is successfully edited, otherwise false.</returns>
		public bool Edit(string firstName, string lastName)
		{
			return Edit(Id, firstName, lastName);
		}

		/// <summary>
		/// Edit the specified student in the database.
		/// </summary>
		/// <param name="studentId">Student database identifier.</param>
		/// <param name="firstName">Student's new first name.</param>
		/// <param name="lastName">Student's new last name.</param>
		/// <returns>True if student is successfully edited, otherwise false.</returns>
		public static bool Edit(int studentId, string firstName, string lastName)
		{
			SqliteConnection conn = DatabaseConnection.GetConnection();
			conn.Open();
			SqliteCommand command = conn.CreateCommand();

			const string studentUpdateFormat =
				"UPDATE Students " +
				"SET FirstName = '{1}', " +
				"LastName = '{2}' " +
				"WHERE ID = {0}";

			command.CommandText = String.Format(studentUpdateFormat, studentId, firstName, lastName);

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

		public List<StudentCourse> GetEnrollment()
		{
			List<StudentCourse> courses = new List<StudentCourse>();

			SqliteConnection conn = DatabaseConnection.GetConnection();
			conn.Open();
			SqliteCommand command = conn.CreateCommand();

			const string enrollmentSqlFormat =
				"SELECT c.ID, c.Name, c.StartDate, c.EndDate, " +
				"CASE WHEN sc.StudentID IS NOT NULL THEN 1 ELSE 0 END " +
				"FROM Courses c " +
				"LEFT JOIN (SELECT * FROM StudentCourses WHERE StudentID = {0}) sc " +
				"ON c.ID = sc.CourseID";

			command.CommandText = String.Format(enrollmentSqlFormat, Id);
			SqliteDataReader reader = command.ExecuteReader();

			while(reader.Read())
			{
				int id =				reader.GetInt32(0);
				string name =			reader.GetString(1);
				DateTime startDate =	reader.GetDateTime(2);
				DateTime endDate =		reader.GetDateTime(3);
				bool isEnrolled =		reader.GetBoolean(4);

				courses.Add(new StudentCourse(id, name, startDate, endDate, Id, isEnrolled));
			}

			// clean up
			reader.Close();
			conn.Close();

			return courses;
		}
	}
}
