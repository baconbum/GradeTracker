using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GradeTracker.Data;
using GradeTracker.Forms;

namespace GradeTracker.UserControls
{
	public class CourseDashboardUserControl : UserControl
	{
		private Button AddNewCourseButton;
		private Button HomeButton;
		private DataGridView CoursesGrid;

		private enum CoursesGridColumn {
			Enrollment = 1,
			Tasks = 2,
			Grades = 3,
			Edit = 4,
			Delete = 5
		};

		public EventHandler HomeButtonClicked;

		public CourseDashboardUserControl()
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
			CourseForm courseForm = new CourseForm();
			courseForm.Show();
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
				ReadOnly =				true,
				AllowUserToAddRows =	false,
				Location =	new Point(AddNewCourseButton.Left, AddNewCourseButton.Height + AddNewCourseButton.Top + 10),
				Anchor =	AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right
			};

			CoursesGrid.Columns.Add(new DataGridViewTextBoxColumn(){ HeaderText = "Name" });

			CoursesGrid.Columns.Add(new DataGridViewButtonColumn(){
				HeaderText = "View Enrollment",
				Text = "View",
				UseColumnTextForButtonValue = true
			});

			CoursesGrid.Columns.Add(new DataGridViewButtonColumn(){
				HeaderText = "View Tasks",
				Text = "Tasks",
				UseColumnTextForButtonValue = true
			});

			CoursesGrid.Columns.Add(new DataGridViewButtonColumn(){
				HeaderText = "Current Grades",
				Text = "Grades",
				UseColumnTextForButtonValue = true
			});

			CoursesGrid.Columns.Add(new DataGridViewButtonColumn(){
				HeaderText = "Edit",
				Text = "Edit",
				UseColumnTextForButtonValue = true
			});

			CoursesGrid.Columns.Add(new DataGridViewButtonColumn(){
				HeaderText = "Delete",
				Text = "Delete",
				UseColumnTextForButtonValue = true
			});

			CoursesGrid.CellClick += CoursesGrid_Clicked;

			Controls.Add(CoursesGrid);
		}

		public override void Refresh()
		{
			base.Refresh();

			CoursesGrid.Rows.Clear();

			List<Course> courses = Course.GetCourses();

			foreach(Course course in courses)
			{
				DataGridViewRow row = new DataGridViewRow(){ Tag = course };

				row.Cells.Add(new DataGridViewTextBoxCell(){ Value = course.Name });

				CoursesGrid.Rows.Add(row);
			}
		}

		private void CoursesGrid_Clicked(object sender, DataGridViewCellEventArgs e)
		{
			DataGridViewRow row = CoursesGrid.Rows[e.RowIndex];
			Course course = (Course)row.Tag;

			switch (e.ColumnIndex)
			{
				case (int)CoursesGridColumn.Enrollment:
					MessageBox.Show(String.Format("View enrollment clicked for course \"{0}\"", course.Name));
					break;
				case (int)CoursesGridColumn.Tasks:
					MessageBox.Show(String.Format("View tasks clicked for course \"{0}\"", course.Name));
					break;
				case (int)CoursesGridColumn.Grades:
					MessageBox.Show(String.Format("View grades clicked for course \"{0}\"", course.Name));
					break;
				case (int)CoursesGridColumn.Edit:
					CourseForm courseForm = new CourseForm(course);
					courseForm.Show();
					break;
				case (int)CoursesGridColumn.Delete:
					switch (MessageBox.Show(String.Format("Are you sure you want to delete {0}", course.Name),
						"Delete Course", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
					{
						case DialogResult.OK:
							course.Delete();
							break;
						default:
							break;
					}
					Refresh();
					break;
			}
		}
	}
}
