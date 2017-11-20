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

			nameTextBox.Text = course.Name;
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
			nameTextBox.Text = String.Empty;
		}

		private void SubmitButtonClicked(object sender, EventArgs e)
		{
			string name =	nameTextBox.Text;

			if (course == null)
			{
				MessageBox.Show("Adding Course");
			}
			else
			{
				MessageBox.Show("Editing Course");
			}
		}
	}
}

