using System;
using System.Drawing;
using System.Windows.Forms;

namespace GradeTracker.UserControls
{
	/// <summary>
	/// A user control containing the splash screen.
	/// </summary>
	public class SplashUserControl : UserControl
	{
		#region Form elements
		private Button CoursesButton;
		private Button StudentsButton;
		#endregion

		public event EventHandler CoursesButtonClicked;
		public event EventHandler StudentsButtonClicked;

		/// <summary>
		/// Initializes a new instance of the <see cref="GradeTracker.UserControls.SplashUserControl"/> class.
		/// </summary>
		public SplashUserControl()
		{
			CreateCoursesButton();
			CreateStudentsButton();
		}

		/// <summary>
		/// Creates the courses button.
		/// </summary>
		private void CreateCoursesButton()
		{
			CoursesButton = new Button() {
				Text =		"Courses",
				Location =	new Point(0, 10)
			};
			CoursesButton.Click += new EventHandler(CoursesButton_Click);
			Controls.Add(CoursesButton);
		}

		/// <summary>
		/// Handles the Courses button's click event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void CoursesButton_Click(object sender, EventArgs e)
		{
			CoursesButtonClicked?.Invoke(this, e);
		}

		/// <summary>
		/// Creates the Students button.
		/// </summary>
		private void CreateStudentsButton()
		{
			StudentsButton = new Button() {
				Text =		"Students",
				Location =	new Point(CoursesButton.Left, CoursesButton.Height + CoursesButton.Top + 10)
			};
			StudentsButton.Click += new EventHandler(StudentsButton_Click);
			Controls.Add(StudentsButton);
		}

		/// <summary>
		/// Handles the Students button's click event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void StudentsButton_Click(object sender, EventArgs e)
		{
			StudentsButtonClicked?.Invoke(this, e);
		}
	}
}
