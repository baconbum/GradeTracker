using System;

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
	}
}

