using System;

namespace GradeTracker.Data
{
	/// <summary>
	/// An extension of the Student class, with additional properties that link it to a course
	/// and contains enrollment status.
	/// </summary>
	public class CourseStudent : Student
	{
		#region Class properties
		public bool IsEnrolled {
			get;
			set;
		}

		public int CourseID {
			get;
			set;
		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="GradeTracker.Data.CourseStudent"/> class.
		/// </summary>
		/// <param name="id">Database identifier.</param>
		/// <param name="firstName">Student's first name.</param>
		/// <param name="lastName">Student's last name.</param>
		/// <param name="courseId">Course database identifier.</param>
		/// <param name="isEnrolled">The enrollment status of the student in the course.</param>
		public CourseStudent(int id, string firstName, string lastName, int courseId, bool isEnrolled)
			: base(id, firstName, lastName)
		{
			CourseID =		courseId;
			IsEnrolled =	isEnrolled;
		}
	}
}
