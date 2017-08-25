using System;
using System.Drawing;
using System.Windows.Forms;

namespace GradeTracker.UserControls
{
	public class CoursesUserControl : UserControl
	{
		private Button AddNewCourseButton;

		public CoursesUserControl()
		{
			CreateAddNewCourseButton();
		}

		private void CreateAddNewCourseButton()
		{
			AddNewCourseButton = new Button() {
				Text =		"Add New Student",
				Location =	new Point(0, 10)
			};
			AddNewCourseButton.Click += new EventHandler(AddNewCourseButton_Click);
			Controls.Add(AddNewCourseButton);
		}

		private void AddNewCourseButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Add New Course Button Clicked");
		}
	}
}
