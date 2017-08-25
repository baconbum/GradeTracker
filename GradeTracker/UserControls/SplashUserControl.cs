using System;
using System.Drawing;
using System.Windows.Forms;

namespace GradeTracker.UserControls
{
	public class SplashUserControl : UserControl
	{
		private Button CoursesButton;
		private Button StudentsButton;

		public event EventHandler CoursesButtonClicked;
		public event EventHandler StudentsButtonClicked;

		public SplashUserControl()
		{
			CreateCoursesButton();
			CreateStudentsButton();
		}

		private void CreateCoursesButton()
		{
			CoursesButton = new Button() {
				Text =		"Courses",
				Location =	new Point(0, 10)
			};
			CoursesButton.Click += new EventHandler(CoursesButton_Click);
			Controls.Add(CoursesButton);
		}

		private void CoursesButton_Click(object sender, EventArgs e)
		{
			CoursesButtonClicked?.Invoke(this, e);
		}

		private void CreateStudentsButton()
		{
			StudentsButton = new Button() {
				Text =		"Students",
				Location =	new Point(CoursesButton.Left, CoursesButton.Height + CoursesButton.Top + 10)
			};
			StudentsButton.Click += new EventHandler(StudentsButton_Click);
			Controls.Add(StudentsButton);
		}

		private void StudentsButton_Click(object sender, EventArgs e)
		{
			StudentsButtonClicked?.Invoke(this, e);
		}
	}
}
