using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GradeTracker.Data;

namespace GradeTracker.UserControls
{
	public class StudentsUserControl : UserControl
	{
		private Button AddNewStudentButton;
		private Button HomeButton;
		private DataGridView StudentsGrid;

		public EventHandler HomeButtonClicked;

		public StudentsUserControl()
		{
			CreateAddNewStudentButton();
			CreateHomeButton();
			CreateStudentsGrid();
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

		private void CreateStudentsGrid()
		{
			StudentsGrid = new DataGridView() {
				Location =	new Point(AddNewStudentButton.Left, AddNewStudentButton.Height + AddNewStudentButton.Top + 10),
				ReadOnly =	true
			};

			StudentsGrid.Columns.Add(new DataGridViewTextBoxColumn(){ HeaderText = "First Name" });
			StudentsGrid.Columns.Add(new DataGridViewTextBoxColumn(){ HeaderText = "Last Name" });

			Controls.Add(StudentsGrid);
		}

		public override void Refresh()
		{
			base.Refresh();

			StudentsGrid.Rows.Clear();

			List<Student> students = Student.GetStudents();

			foreach(Student student in students)
			{
				DataGridViewRow row = new DataGridViewRow();

				row.Cells.Add(new DataGridViewTextBoxCell(){ Value = student.FirstName });
				row.Cells.Add(new DataGridViewTextBoxCell(){ Value = student.LastName });

				StudentsGrid.Rows.Add(row);
			}
		}
	}
}
