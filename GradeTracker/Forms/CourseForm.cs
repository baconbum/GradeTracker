using System;
using System.Drawing;
using System.Windows.Forms;
using GradeTracker.Data;

namespace GradeTracker.Forms
{
	/// <summary>
	/// A form to add or edit courses.
	/// </summary>
	public class CourseForm : Form
	{
		#region Class fields
		Course course = null;

		TableLayoutPanel tableLayoutPanel;

		Label nameLabel;
		TextBox nameTextBox;

		Label startDateLabel;
		DateTimePicker startDatePicker;

		Label endDateLabel;
		DateTimePicker endDatePicker;

		Button submitButton;
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="GradeTracker.Forms.CourseForm"/> class for adding a course to the database.
		/// </summary>
		public CourseForm()
		{
			Text = "Add Course";

			InitializeForm();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GradeTracker.Forms.CourseForm"/> class for editing a course in the database.
		/// </summary>
		/// <param name="course">The course to edit.</param>
		public CourseForm(Course course)
		{
			Text = "Edit Course";

			this.course = course;

			InitializeForm();

			nameTextBox.Text =		course.Name;
			startDatePicker.Value =	course.StartDate;
			endDatePicker.Value =	course.EndDate;
		}

		/// <summary>
		/// Initializes the form.
		/// </summary>
		private void InitializeForm()
		{
			InitalizeTableLayoutPanel();
			InitializeFormFields();
			InitializeButtons();

			Size =				new Size(400, 175);
			FormBorderStyle =	FormBorderStyle.FixedSingle;
			MaximizeBox =		false;
		}

		/// <summary>
		/// Initalizes the table layout panel.
		/// </summary>
		private void InitalizeTableLayoutPanel()
		{
			tableLayoutPanel = new TableLayoutPanel() {
				Dock =			DockStyle.Fill,
				RowCount =		4,
				ColumnCount =	2
			};
		}

		/// <summary>
		/// Initializes the form fields.
		/// </summary>
		private void InitializeFormFields()
		{
			nameLabel = new Label() {
				Text = "Name",
				Anchor = AnchorStyles.Left
			};

			nameTextBox = new TextBox() {
				Anchor = AnchorStyles.Left | AnchorStyles.Right
			};

			tableLayoutPanel.Controls.Add(nameLabel, 0, 0);
			tableLayoutPanel.Controls.Add(nameTextBox, 1, 0);

			startDateLabel = new Label() {
				Text = "Start Date",
				Anchor = AnchorStyles.Left
			};

			startDatePicker = new DateTimePicker() {
				Anchor = AnchorStyles.Left | AnchorStyles.Right
			};

			tableLayoutPanel.Controls.Add(startDateLabel, 0, 1);
			tableLayoutPanel.Controls.Add(startDatePicker, 1, 1);

			endDateLabel = new Label() {
				Text = "End Date",
				Anchor = AnchorStyles.Left
			};

			endDatePicker = new DateTimePicker() {
				Anchor = AnchorStyles.Left | AnchorStyles.Right
			};

			tableLayoutPanel.Controls.Add(endDateLabel, 0, 2);
			tableLayoutPanel.Controls.Add(endDatePicker, 1, 2);

			Controls.Add(tableLayoutPanel);
		}

		/// <summary>
		/// Initializes the buttons.
		/// </summary>
		private void InitializeButtons()
		{
			submitButton = new Button() {
				Text =		"Submit",
				Anchor =	AnchorStyles.Bottom
			};

			submitButton.Click += SubmitButton_Click;

			tableLayoutPanel.Controls.Add(submitButton, 0, 3);

			tableLayoutPanel.SetColumnSpan(submitButton, 2);
		}

		/// <summary>
		/// Resets the form fields to their default values.
		/// </summary>
		private void ResetForm()
		{
			nameTextBox.Text =		String.Empty;
			startDatePicker.Value =	DateTime.Now;
			endDatePicker.Value =	DateTime.Now;
		}

		/// <summary>
		/// Validates the form fields.
		/// </summary>
		/// <returns><c>true</c>, if form was validated, <c>false</c> otherwise.</returns>
		private bool ValidateForm()
		{
			if (String.IsNullOrWhiteSpace(nameTextBox.Text))
			{
				MessageBox.Show(this, "Name cannot be empty.", "Invalid Course",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}

			if (startDatePicker.Value > endDatePicker.Value)
			{
				MessageBox.Show(this, "Start Date cannot be greater than End Date.", "Invalid Course",
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

			string name =			nameTextBox.Text;
			DateTime startDate =	startDatePicker.Value;
			DateTime endDate =		endDatePicker.Value;

			if (course == null)
			{
				if (Course.Add(name, startDate, endDate))
				{
					MessageBox.Show(this, String.Format("Added {0}", name),
						"Course Added", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					ResetForm();
				}
				else
				{
					MessageBox.Show(this, String.Format("Error adding course {0}", name),
						"Error Adding Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				if (course.Edit(name, startDate, endDate))
				{
					MessageBox.Show(this, String.Format("Updated {0}", name),
						"Course Updated", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					MessageBox.Show(this, String.Format("Error updating course {0}", name),
						"Error Updating Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
