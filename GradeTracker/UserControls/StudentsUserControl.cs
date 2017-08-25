using System;
using System.Drawing;
using System.Windows.Forms;

namespace GradeTracker.UserControls
{
	public class StudentsUserControl : UserControl
	{
		private Button AddNewStudentButton;

		public StudentsUserControl()
		{
			CreateAddNewStudentButton();
		}

		private void CreateAddNewStudentButton()
		{
			AddNewStudentButton = new Button() {
				Text =		"Add New Student",
				Location =	new Point(0, 10)
			};
			AddNewStudentButton.Click += new EventHandler(AddNewStudentButton_Click);
			Controls.Add(AddNewStudentButton);
		}

		private void AddNewStudentButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Add New Student Button Clicked");
		}
	}
}
