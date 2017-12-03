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
				HeaderText = "Add/Remove",
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
			Course course = (Course)row.Tag;

			switch (e.ColumnIndex)
			{
				case (int)CoursesGridColumn.ToggleEnrollment:
					MessageBox.Show(this, String.Format("Toggle enrollment clicked for course \"{0}\"", course.Name));
					Refresh();
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

				coursesGrid.Rows.Add(row);
			}
		}
	}
}

