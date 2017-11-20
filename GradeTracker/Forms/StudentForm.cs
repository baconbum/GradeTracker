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

		FlowLayoutPanel flowLayoutPanel;

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

			InitalizeFlowLayoutPanel();
			InitializeFormFields();
			InitializeButtons();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GradeTracker.Forms.StudentForm"/> class for editing a student in the database.
		/// </summary>
		/// <param name="student">The student to edit.</param>
		public StudentForm(Student student)
		{
			Text = "Edit Student";

			this.student = student;

			InitalizeFlowLayoutPanel();
			InitializeFormFields();
			InitializeButtons();

			firstNameTextBox.Text = student.FirstName;
			lastNameTextBox.Text = student.LastName;
		}

		/// <summary>
		/// Initalizes the flow layout panel.
		/// </summary>
		private void InitalizeFlowLayoutPanel()
		{
			flowLayoutPanel = new FlowLayoutPanel() {
				FlowDirection = FlowDirection.TopDown,
				Dock = DockStyle.Fill
			};
		}

		/// <summary>
		/// Initializes the form fields.
		/// </summary>
		private void InitializeFormFields()
		{
			FlowLayoutPanel firstNamePanel = new FlowLayoutPanel() {
				AutoSize = true
			};

			firstNameLabel = new Label() {
				Text = "First Name",
				Anchor = AnchorStyles.Left
			};

			firstNameTextBox = new TextBox() {};

			firstNamePanel.Controls.Add(firstNameLabel);
			firstNamePanel.Controls.Add(firstNameTextBox);

			FlowLayoutPanel lastNamePanel = new FlowLayoutPanel() {
				AutoSize = true
			};

			lastNameLabel = new Label() {
				Text = "Last Name",
				Anchor = AnchorStyles.Left
			};

			lastNameTextBox = new TextBox() {};

			lastNamePanel.Controls.Add(lastNameLabel);
			lastNamePanel.Controls.Add(lastNameTextBox);

			flowLayoutPanel.Controls.Add(firstNamePanel);
			flowLayoutPanel.Controls.Add(lastNamePanel);

			Controls.Add(flowLayoutPanel);
		}

		/// <summary>
		/// Initializes the buttons.
		/// </summary>
		private void InitializeButtons()
		{
			FlowLayoutPanel buttonsPanel = new FlowLayoutPanel() {
				AutoSize = true
			};

			submitButton = new Button() {
				Text = "Submit",
				Anchor = AnchorStyles.Bottom
			};

			submitButton.Click += SubmitButton_Click;

			buttonsPanel.Controls.Add(submitButton);

			flowLayoutPanel.Controls.Add(buttonsPanel);
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
