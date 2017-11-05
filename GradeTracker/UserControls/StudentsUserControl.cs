using System;
using System.Drawing;
using System.Windows.Forms;

namespace GradeTracker.UserControls
{
	public class StudentsUserControl : UserControl
	{
		private Button AddNewStudentButton;
		private Button HomeButton;

		public EventHandler HomeButtonClicked;

		public StudentsUserControl()
		{
			CreateAddNewStudentButton();
			CreateHomeButton();
		}

		private void CreateAddNewStudentButton()
		{
			AddNewStudentButton = new Button() {
				Text =		"Add New Student",
				Width =		200,
				Location =	new Point(0, 10)
			};
			AddNewStudentButton.Click += new EventHandler(AddNewStudentButton_Click);
			Controls.Add(AddNewStudentButton);
		}

		private void AddNewStudentButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Add New Student Button Clicked");
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
