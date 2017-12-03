using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GradeTracker.Data;

namespace GradeTracker.Forms
{
	public class StudentCoursesForm : Form
	{
		private Student student;

		private DataGridView coursesGrid;

		/// <summary>
		/// Maps the columns of the Courses grid.
		/// </summary>
		private enum CoursesGridColumn {
			ToggleEnrollment = 2
		};

		public StudentCoursesForm(Student student)
		{
			this.student = student;

			Text = String.Format("Course Enrollment: {0}, {1}", this.student.LastName, this.student.FirstName);
			MinimumSize = new Size(600, 400);

			CreateCoursesGrid();
			Refresh();
		}

		/// <summary>
		/// Creates the Courses grid.
		/// </summary>
		private void CreateCoursesGrid()
		{
			coursesGrid = new DataGridView() {
				ReadOnly =				true,
				AllowUserToAddRows =	false,
				Dock =					DockStyle.Fill,
				AutoSizeColumnsMode =	DataGridViewAutoSizeColumnsMode.Fill,
				ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
			};

			coursesGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Name" });
			coursesGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Is Enrolled" });

			coursesGrid.Columns.Add(new DataGridViewButtonColumn() {
				HeaderText = "Enroll/Withdraw",
				Text = "Toggle Enrollment",
				UseColumnTextForButtonValue = true
			});

			coursesGrid.CellClick += CoursesGrid_Clicked;

			Controls.Add(coursesGrid);
		}

		/// <summary>
		/// Handles the Courses grid's click event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
		private void CoursesGrid_Clicked(object sender, DataGridViewCellEventArgs e)
		{
			DataGridViewRow row = coursesGrid.Rows[e.RowIndex];
			StudentCourse course = (StudentCourse)row.Tag;

			switch (e.ColumnIndex)
			{
				case (int)CoursesGridColumn.ToggleEnrollment:
					if (course.IsEnrolled)
					{
						WithdrawStudentFromCourse(course);
					}
					else
					{
						EnrollStudentInCourse(course);
					}
					Refresh();
					break;
			}
		}

		/// <summary>
		/// Enrolls the student in the specified course.
		/// </summary>
		/// <param name="course">The course to enroll the student in.</param>
		private void EnrollStudentInCourse(Course course)
		{
			switch (MessageBox.Show(this,
				String.Format("Are you sure you want to enroll {0}, {1} in {2}",
					student.LastName, student.FirstName, course.Name),
				"Enroll in Course", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
			{
				case DialogResult.OK:
					if (!student.EnrollInCourse(course.Id))
					{
						MessageBox.Show(this,
							"An error has occurred while attempting to enroll the student in the course",
							"Enrollment error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Withdraws the student from the specified course.
		/// </summary>
		/// <param name="course">The course to withdraw the student from.</param>
		private void WithdrawStudentFromCourse(Course course)
		{
			switch (MessageBox.Show(this,
				String.Format("Are you sure you want to withdraw {0}, {1} from {2}",
					student.LastName, student.FirstName, course.Name),
				"Withdraw from Course", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
			{
				case DialogResult.OK:
					if (!student.WithdrawFromCourse(course.Id))
					{
						MessageBox.Show(this,
							"An error has occurred while attempting to withdraw the student from the course",
							"Withdrawal error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Refresh the form, and ensure the Courses grid is up to date.
		/// </summary>
		public override void Refresh()
		{
			base.Refresh();

			coursesGrid.Rows.Clear();

			List<StudentCourse> courses = student.GetEnrollment();

			foreach(StudentCourse course in courses)
			{
				DataGridViewRow row = new DataGridViewRow(){ Tag = course };

				row.Cells.Add(new DataGridViewTextBoxCell(){ Value = course.Name });
				row.Cells.Add(new DataGridViewTextBoxCell(){ Value = (course.IsEnrolled) ? "Yes" : "No" });

				row.Cells.Add(new DataGridViewButtonCell(){ Value = (course.IsEnrolled) ? "Withdraw" : "Enroll" });

				coursesGrid.Rows.Add(row);
			}
		}
	}
}
