using System;
using System.Drawing;
using System.Windows.Forms;
using GradeTracker.Data;

namespace GradeTracker.Forms
{
	/// <summary>
	/// A form to add or edit students.
	/// </summary>
	public class StudentForm : Form
	{
		#region Class fields
		Student student = null;

		TableLayoutPanel tableLayoutPanel;

		Label firstNameLabel;
		TextBox firstNameTextBox;

		Label lastNameLabel;
		TextBox lastNameTextBox;

		Button submitButton;
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="GradeTracker.Forms.StudentForm"/> class for adding a student to the database.
		/// </summary>
		public StudentForm()
		{
			Text = "Add Student";

			InitalizeTableLayoutPanel();
			InitializeFormFields();
			InitializeButtons();

			Size =				new Size(400, 125);
			FormBorderStyle =	FormBorderStyle.FixedSingle;
			MaximizeBox =		false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GradeTracker.Forms.StudentForm"/> class for editing a student in the database.
		/// </summary>
		/// <param name="student">The student to edit.</param>
		public StudentForm(Student student)
		{
			Text = "Edit Student";

			this.student = student;

			InitalizeTableLayoutPanel();
			InitializeFormFields();
			InitializeButtons();

			Size =				new Size(400, 125);
			FormBorderStyle =	FormBorderStyle.FixedSingle;
			MaximizeBox =		false;

			firstNameTextBox.Text = student.FirstName;
			lastNameTextBox.Text = student.LastName;
		}

		/// <summary>
		/// Initalizes the table layout panel.
		/// </summary>
		private void InitalizeTableLayoutPanel()
		{
			tableLayoutPanel = new TableLayoutPanel() {
				Dock =			DockStyle.Fill,
				RowCount =		3,
				ColumnCount =	2
			};
		}

		/// <summary>
		/// Initializes the form fields.
		/// </summary>
		private void InitializeFormFields()
		{
			firstNameLabel = new Label() {
				Text = "First Name",
				Anchor = AnchorStyles.Left
			};

			firstNameTextBox = new TextBox() {
				Anchor = AnchorStyles.Left | AnchorStyles.Right
			};

			tableLayoutPanel.Controls.Add(firstNameLabel, 0, 0);
			tableLayoutPanel.Controls.Add(firstNameTextBox, 1, 0);

			lastNameLabel = new Label() {
				Text = "Last Name",
				Anchor = AnchorStyles.Left
			};

			lastNameTextBox = new TextBox() {
				Anchor = AnchorStyles.Left | AnchorStyles.Right
			};

			tableLayoutPanel.Controls.Add(lastNameLabel, 0, 1);
			tableLayoutPanel.Controls.Add(lastNameTextBox, 1, 1);

			Controls.Add(tableLayoutPanel);
		}

		/// <summary>
		/// Initializes the buttons.
		/// </summary>
		private void InitializeButtons()
		{
			submitButton = new Button() {
				Text = "Submit",
				Anchor = AnchorStyles.Bottom
			};

			submitButton.Click += SubmitButton_Click;

			tableLayoutPanel.Controls.Add(submitButton, 0, 2);

			tableLayoutPanel.SetColumnSpan(submitButton, 2);
		}

		/// <summary>
		/// Resets the form fields to their default values.
		/// </summary>
		private void ResetForm()
		{
			firstNameTextBox.Text = String.Empty;
			lastNameTextBox.Text = String.Empty;
		}

		/// <summary>
		/// Validates the form fields.
		/// </summary>
		/// <returns><c>true</c>, if form was validated, <c>false</c> otherwise.</returns>
		private bool ValidateForm()
		{
			if (String.IsNullOrWhiteSpace(firstNameTextBox.Text))
			{
				MessageBox.Show(this, "First Name cannot be empty.", "Invalid Student",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}

			if (String.IsNullOrWhiteSpace(lastNameTextBox.Text))
			{
				MessageBox.Show(this, "Last Name cannot be empty.", "Invalid Student",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}

			return true;
		}

		/// <summary>
		/// Handles the form's submit action.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void SubmitButton_Click(object sender, EventArgs e)
		{
			if (!ValidateForm()) return;

			string firstName =	firstNameTextBox.Text;
			string lastName =	lastNameTextBox.Text;

			if (student == null)
			{
				if (Student.Add(firstName, lastName))
				{
					MessageBox.Show(this, String.Format("Added {0}, {1}", firstName, lastName),
						"Student Added", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					ResetForm();
				}
				else
				{
					MessageBox.Show(this, String.Format("Error adding student {0}, {1}", lastName, firstName),
						"Error Adding Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				if (student.Edit(firstName, lastName))
				{
					MessageBox.Show(this, String.Format("Updated {0}, {1}", firstName, lastName),
						"Student Updated", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					MessageBox.Show(this, String.Format("Error updating student {0}, {1}", lastName, firstName),
						"Error Updating Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
