using System;
using System.Drawing;
using System.Windows.Forms;
using GradeTracker.Data;

namespace GradeTracker.Forms
{
	public class CourseForm : Form
	{
		Course course = null;

		FlowLayoutPanel flowLayoutPanel;

		Label nameLabel;
		TextBox nameTextBox;

		Label startDateLabel;
		DateTimePicker startDatePicker;

		Label endDateLabel;
		DateTimePicker endDatePicker;

		Button submitButton;

		public CourseForm()
		{
			Text = "Add Course";

			InitalizeFlowLayoutPanel();
			CreateFormFields();
			CreateButtons();
		}

		public CourseForm(Course course)
		{
			Text = "Edit Course";

			this.course = course;

			InitalizeFlowLayoutPanel();
			CreateFormFields();
			CreateButtons();

			nameTextBox.Text =		course.Name;
			startDatePicker.Value =	course.StartDate;
			endDatePicker.Value =	course.EndDate;
		}

		private void InitalizeFlowLayoutPanel()
		{
			flowLayoutPanel = new FlowLayoutPanel() {
				FlowDirection = FlowDirection.TopDown,
				Dock = DockStyle.Fill
			};
		}

		private void CreateFormFields()
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

		private void CreateButtons()
		{
			FlowLayoutPanel buttonsPanel = new FlowLayoutPanel() {
				AutoSize = true
			};

			submitButton = new Button() {
				Text = "Submit",
				Anchor = AnchorStyles.Bottom
			};

			submitButton.Click += SubmitButtonClicked;

			buttonsPanel.Controls.Add(submitButton);

			flowLayoutPanel.Controls.Add(buttonsPanel);
		}

		private void ResetForm()
		{
			nameTextBox.Text =		String.Empty;
			startDatePicker.Value =	DateTime.Now;
			endDatePicker.Value =	DateTime.Now;
		}

		private void SubmitButtonClicked(object sender, EventArgs e)
		{
			string name =			nameTextBox.Text;
			DateTime startDate =	startDatePicker.Value;
			DateTime endDate =		endDatePicker.Value;

			if (course == null)
			{
				if (Course.AddCourseToDatabase(name, startDate, endDate))
				{
					MessageBox.Show(String.Format("Added {0}", name),
						"Course Added", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					ResetForm();
				}
				else
				{
					MessageBox.Show(String.Format("Error adding course {0}", name),
						"Error Adding Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				if (course.Edit(name, startDate, endDate))
				{
					MessageBox.Show(String.Format("Updated {0}", name),
						"Course Updated", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					MessageBox.Show(String.Format("Error updating course {0}", name),
						"Error Updating Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
