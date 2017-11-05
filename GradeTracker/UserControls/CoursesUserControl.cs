using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GradeTracker.Data;

namespace GradeTracker.UserControls
{
	public class CoursesUserControl : UserControl
	{
		private Button AddNewCourseButton;
		private Button HomeButton;
		private DataGridView CoursesGrid;

		public EventHandler HomeButtonClicked;

		public CoursesUserControl()
		{
			CreateAddNewCourseButton();
			CreateHomeButton();
			CreateCoursesGrid();
		}

		private void CreateAddNewCourseButton()
		{
			AddNewCourseButton = new Button() {
				Text =		"Add New Course",
				Width =		200,
				Location =	new Point(0, 10)
			};
			AddNewCourseButton.Click += new EventHandler(AddNewCourseButton_Click);
			Controls.Add(AddNewCourseButton);
		}

		private void AddNewCourseButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Add New Course Button Clicked");
		}

		private void CreateHomeButton()
		{
			HomeButton = new Button() {
				Text = "Home",
				Anchor = (AnchorStyles.Bottom | AnchorStyles.Left)
			};
			HomeButton.Click += HomeButton_Click;
			Controls.Add(HomeButton);
		}

		private void HomeButton_Click (object sender, EventArgs e)
		{
			HomeButtonClicked?.Invoke(this, e);
		}

		private void CreateCoursesGrid()
		{
			CoursesGrid = new DataGridView() {
				Location =	new Point(AddNewCourseButton.Left, AddNewCourseButton.Height + AddNewCourseButton.Top + 10),
				ReadOnly =	true
			};

			CoursesGrid.Columns.Add(new DataGridViewTextBoxColumn(){ HeaderText = "Name" });

			Controls.Add(CoursesGrid);
		}

		public override void Refresh()
		{
			base.Refresh();

			CoursesGrid.Rows.Clear();

			List<Course> courses = Course.GetCourses();

			foreach(Course course in courses)
			{
				DataGridViewRow row = new DataGridViewRow();

				row.Cells.Add(new DataGridViewTextBoxCell(){ Value = course.Name });

				CoursesGrid.Rows.Add(row);
			}
		}
	}
}
