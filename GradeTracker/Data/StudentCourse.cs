using System;
using GradeTracker.Data;

namespace GradeTracker
{
	/// <summary>
	/// An extension of the Course class, with additional properties that link it to a student
	/// and contains enrollment status.
	/// </summary>
	public class StudentCourse : Course
	{
		#region Class properties
		public bool IsEnrolled {
			get;
			set;
		}

		public int StudentID {
			get;
			set;
		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="GradeTracker.Data.StudentCourse"/> class.
		/// </summary>
		/// <param name="id">Database identifier.</param>
		/// <param name="name">Course name.</param>
		/// <param name="startDate">Course start date.</param>
		/// <param name="endDate">Course end date.</param>
		/// <param name="studentID">The student's database identifier.</param> 
		/// <param name="isEnrolled">The enrollment status of the student in the course.</param>
		public StudentCourse(int id, string name, DateTime startDate, DateTime endDate, int studentID, bool isEnrolled)
			: base(id, name, startDate, endDate)
		{
			IsEnrolled = isEnrolled;
			StudentID = studentID;
		}
	}
}
