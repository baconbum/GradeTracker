using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GradeTracker.Data;
using GradeTracker.Forms;

namespace GradeTracker.UserControls
{
	/// <summary>
	/// A user control containing functionality related to students.
	/// </summary>
	public class StudentDashboardUserControl : UserControl
	{
		#region Form elements
		private Button AddNewStudentButton;
		private Button HomeButton;
		private DataGridView StudentsGrid;
		#endregion

		/// <summary>
		/// Maps the columns of the Students grid.
		/// </summary>
		private enum StudentsGridColumn {
			Enrollment =	2,
			Edit =			3,
			Delete =		4
		};

		public EventHandler HomeButtonClicked;

		/// <summary>
		/// Initializes a new instance of the <see cref="GradeTracker.UserControls.StudentDashboardUserControl"/> class.
		/// </summary>
		public StudentDashboardUserControl()
		{
			CreateAddNewStudentButton();
			CreateHomeButton();
			CreateStudentsGrid();
		}

		/// <summary>
		/// Creates the Add New Student button.
		/// </summary>
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

		/// <summary>
		/// Handles the Add New Student button's click event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void AddNewStudentButton_Click(object sender, EventArgs e)
		{
			StudentForm studentForm = new StudentForm();
			studentForm.Show();
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
		/// Creates the Students grid.
		/// </summary>
		private void CreateStudentsGrid()
		{
			StudentsGrid = new DataGridView() {
				ReadOnly =				true,
				AllowUserToAddRows =	false,
				Location =	new Point(AddNewStudentButton.Left, AddNewStudentButton.Height + AddNewStudentButton.Top + 10),
				Anchor =	AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right
			};

			StudentsGrid.Columns.Add(new DataGridViewTextBoxColumn(){ HeaderText = "First Name" });
			StudentsGrid.Columns.Add(new DataGridViewTextBoxColumn(){ HeaderText = "Last Name" });

			StudentsGrid.Columns.Add(new DataGridViewButtonColumn(){
				HeaderText = "View Enrollment",
				Text = "Enrollment",
				UseColumnTextForButtonValue = true
			});

			StudentsGrid.Columns.Add(new DataGridViewButtonColumn(){
				HeaderText = "Edit",
				Text = "Edit",
				UseColumnTextForButtonValue = true
			});

			StudentsGrid.Columns.Add(new DataGridViewButtonColumn(){
				HeaderText = "Delete",
				Text = "Delete",
				UseColumnTextForButtonValue = true
			});

			StudentsGrid.CellClick += StudentsGridClicked;

			Controls.Add(StudentsGrid);
   		}

		/// <summary>
		/// Handles the Student grid's click event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
		private void StudentsGridClicked(object sender, DataGridViewCellEventArgs e)
		{
			DataGridViewRow row = StudentsGrid.Rows[e.RowIndex];
			Student student = (Student)row.Tag;

			switch (e.ColumnIndex)
			{
				case (int)StudentsGridColumn.Enrollment:
					MessageBox.Show(this, String.Format("View enrollment clicked for student {0}, {1}", student.LastName, student.FirstName));
					break;
				case (int)StudentsGridColumn.Edit:
					StudentForm studentForm = new StudentForm(student);
					studentForm.Show();
					break;
				case (int)StudentsGridColumn.Delete:
					switch (MessageBox.Show(this, String.Format("Are you sure you want to delete {0}, {1}", student.LastName, student.FirstName),
						"Delete Student", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
					{
						case DialogResult.OK:
							student.Delete();
							break;
						default:
							break;
					}
					Refresh();
					break;
			}
		}

		/// <summary>
		/// Refresh the form, and ensure the Students grid is up to date.
		/// </summary>
		public override void Refresh()
		{
			base.Refresh();

			StudentsGrid.Rows.Clear();

			List<Student> students = Student.GetStudents();

			foreach(Student student in students)
			{
				DataGridViewRow row = new DataGridViewRow(){ Tag = student };

				row.Cells.Add(new DataGridViewTextBoxCell(){ Value = student.FirstName });
				row.Cells.Add(new DataGridViewTextBoxCell(){ Value = student.LastName });

				StudentsGrid.Rows.Add(row);
			}
		}
	}
}
