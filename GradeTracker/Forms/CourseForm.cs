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

		FlowLayoutPanel flowLayoutPanel;

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

			InitalizeFlowLayoutPanel();
			InitializeFormFields();
			InitializeButtons();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GradeTracker.Forms.CourseForm"/> class for editing a course in the database.
		/// </summary>
		/// <param name="course">The course to edit.</param>
		public CourseForm(Course course)
		{
			Text = "Edit Course";

			this.course = course;

			InitalizeFlowLayoutPanel();
			InitializeFormFields();
			InitializeButtons();

			nameTextBox.Text =		course.Name;
			startDatePicker.Value =	course.StartDate;
			endDatePicker.Value =	course.EndDate;
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
			FlowLayoutPanel namePanel = new FlowLayoutPanel() {
				AutoSize = true
			};

			nameLabel = new Label() {
				Text = "Name",
				Anchor = AnchorStyles.Left
			};

			nameTextBox = new TextBox() {};

			namePanel.Controls.Add(nameLabel);
			namePanel.Controls.Add(nameTextBox);

			FlowLayoutPanel startDatePanel = new FlowLayoutPanel() {
				AutoSize = true
			};

			startDateLabel = new Label() {
				Text = "Start Date",
				Anchor = AnchorStyles.Left
			};

			startDatePicker = new DateTimePicker() {};

			startDatePanel.Controls.Add(startDateLabel);
			startDatePanel.Controls.Add(startDatePicker);

			FlowLayoutPanel endDatePanel = new FlowLayoutPanel() {
				AutoSize = true
			};

			endDateLabel = new Label() {
				Text = "End Date",
				Anchor = AnchorStyles.Left
			};

			endDatePicker = new DateTimePicker() {};

			endDatePanel.Controls.Add(endDateLabel);
			endDatePanel.Controls.Add(endDatePicker);

			flowLayoutPanel.Controls.Add(namePanel);
			flowLayoutPanel.Controls.Add(startDatePanel);
			flowLayoutPanel.Controls.Add(endDatePanel);

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
