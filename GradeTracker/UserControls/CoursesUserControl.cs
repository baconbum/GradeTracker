using System;
using System.Drawing;
using System.Windows.Forms;

namespace GradeTracker.UserControls
{
	public class CoursesUserControl : UserControl
	{
		private Button AddNewCourseButton;
		private Button HomeButton;

		public EventHandler HomeButtonClicked;

		public CoursesUserControl()
		{
			CreateAddNewCourseButton();
			CreateHomeButton();
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
	}
}
