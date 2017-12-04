using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GradeTracker.Data;

namespace GradeTracker
{
	public class CourseStudentsForm : Form
	{
		private Course course;

		private DataGridView studentsGrid;

		/// <summary>
		/// Maps the columns of the Students grid.
		/// </summary>
		private enum StudentsGridColumn {
			ToggleEnrollment = 3
		};

		public CourseStudentsForm(Course course)
		{
			this.course = course;

			Text = String.Format("Course Enrollment: {0}", this.course.Name);
			MinimumSize = new Size(600, 400);

			CreateStudentsGrid();
			Refresh();
		}

		/// <summary>
		/// Creates the Students grid.
		/// </summary>
		private void CreateStudentsGrid()
		{
			studentsGrid = new DataGridView() {
				ReadOnly =				true,
				AllowUserToAddRows =	false,
				Dock =					DockStyle.Fill,
				AutoSizeColumnsMode =	DataGridViewAutoSizeColumnsMode.Fill,
				ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
			};

			studentsGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "First Name" });
			studentsGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Last Name" });
			studentsGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Is Enrolled" });

			studentsGrid.Columns.Add(new DataGridViewButtonColumn() {
				HeaderText = "Enroll/Withdraw",
				Text = "Toggle Enrollment",
				UseColumnTextForButtonValue = true
			});

			studentsGrid.CellClick += StudentsGrid_Clicked;

			Controls.Add(studentsGrid);
		}

		/// <summary>
		/// Handles the Students grid's click event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
		private void StudentsGrid_Clicked(object sender, DataGridViewCellEventArgs e)
		{
			DataGridViewRow row = studentsGrid.Rows[e.RowIndex];
			CourseStudent student = (CourseStudent)row.Tag;

			switch (e.ColumnIndex)
			{
				case (int)StudentsGridColumn.ToggleEnrollment:
					if (student.IsEnrolled)
					{
						WithdrawStudentFromCourse(student);
					}
					else
					{
						EnrollStudentInCourse(student);
					}
					Refresh();
					break;
			}
		}

		/// <summary>
		/// Enrolls the specified student in the course.
		/// </summary>
		/// <param name="student">The student to enroll in the course.</param>
		private void EnrollStudentInCourse(Student student)
		{
			switch (MessageBox.Show(this,
				String.Format("Are you sure you want to enroll {0}, {1} in {2}",
					student.LastName, student.FirstName, course.Name),
				"Enroll in Course", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
			{
				case DialogResult.OK:
					if (!course.EnrollInCourse(student.Id))
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
		/// Withdraws the specified student from the course.
		/// </summary>
		/// <param name="student">The student to withdraw from the course.</param>
		private void WithdrawStudentFromCourse(Student student)
		{
			switch (MessageBox.Show(this,
				String.Format("Are you sure you want to withdraw {0}, {1} from {2}",
					student.LastName, student.FirstName, course.Name),
				"Withdraw from Course", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
			{
				case DialogResult.OK:
					if (!course.WithdrawFromCourse(student.Id))
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
		/// Refresh the form, and ensure the Students grid is up to date.
		/// </summary>
		public override void Refresh()
		{
			base.Refresh();

			studentsGrid.Rows.Clear();

			List<CourseStudent> students = course.GetEnrollment();

			foreach(CourseStudent student in students)
			{
				DataGridViewRow row = new DataGridViewRow(){ Tag = student };

				row.Cells.Add(new DataGridViewTextBoxCell(){ Value = student.FirstName });
				row.Cells.Add(new DataGridViewTextBoxCell(){ Value = student.LastName });
				row.Cells.Add(new DataGridViewTextBoxCell(){ Value = (student.IsEnrolled) ? "Yes" : "No" });

				row.Cells.Add(new DataGridViewButtonCell(){ Value = (student.IsEnrolled) ? "Withdraw" : "Enroll" });

				studentsGrid.Rows.Add(row);
			}
		}
	}
}
