using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GradeTracker.Data;
using GradeTracker.Forms;

namespace GradeTracker.UserControls
{
	/// <summary>
	/// A user control containing functionality related to courses.
	/// </summary>
	public class CourseDashboardUserControl : UserControl
	{
		#region Form elements
		private Button AddNewCourseButton;
		private Button HomeButton;
		private DataGridView CoursesGrid;
		#endregion

		/// <summary>
		/// Maps the columns of the Courses grid.
		/// </summary>
		private enum CoursesGridColumn {
			Enrollment =	1,
			Tasks =			2,
			Grades =		3,
			Edit =			4,
			Delete =		5
		};

		public EventHandler HomeButtonClicked;

		/// <summary>
		/// Initializes a new instance of the <see cref="GradeTracker.UserControls.CourseDashboardUserControl"/> class.
		/// </summary>
		public CourseDashboardUserControl()
		{
			CreateAddNewCourseButton();
			CreateHomeButton();
			CreateCoursesGrid();
		}

		/// <summary>
		/// Creates the Add New Course button.
		/// </summary>
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

		/// <summary>
		/// Handles the Add New Course button's click event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void AddNewCourseButton_Click(object sender, EventArgs e)
		{
			CourseForm courseForm = new CourseForm();
			courseForm.Show();
		}

		/// <summary>
		/// Creates the Home button.
		/// </summary>
		private void CreateHomeButton()
		{
			HomeButton = new Button() {
				Text = "Home",
				Anchor = (AnchorStyles.Bottom | AnchorStyles.Left)
			};
			HomeButton.Click += HomeButton_Click;
			Controls.Add(HomeButton);
		}

		/// <summary>
		/// Handles the Home button's click event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void HomeButton_Click (object sender, EventArgs e)
		{
			HomeButtonClicked?.Invoke(this, e);
		}

		/// <summary>
		/// Creates the Courses grid.
		/// </summary>
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

		/// <summary>
		/// Handles the Courses grid's click event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
		private void CoursesGrid_Clicked(object sender, DataGridViewCellEventArgs e)
		{
			DataGridViewRow row = CoursesGrid.Rows[e.RowIndex];
			Course course = (Course)row.Tag;

			switch (e.ColumnIndex)
			{
				case (int)CoursesGridColumn.Enrollment:
					MessageBox.Show(this, String.Format("View enrollment clicked for course \"{0}\"", course.Name));
					break;
				case (int)CoursesGridColumn.Tasks:
					MessageBox.Show(this, String.Format("View tasks clicked for course \"{0}\"", course.Name));
					break;
				case (int)CoursesGridColumn.Grades:
					MessageBox.Show(this, String.Format("View grades clicked for course \"{0}\"", course.Name));
					break;
				case (int)CoursesGridColumn.Edit:
					CourseForm courseForm = new CourseForm(course);
					courseForm.Show();
					break;
				case (int)CoursesGridColumn.Delete:
					switch (MessageBox.Show(this, String.Format("Are you sure you want to delete {0}", course.Name),
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

		/// <summary>
		/// Refresh the form, and ensure the Courses grid is up to date.
		/// </summary>
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
	}
}
