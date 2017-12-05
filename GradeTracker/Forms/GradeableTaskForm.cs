using System;
using System.Drawing;
using System.Windows.Forms;
using GradeTracker.Data;

namespace GradeTracker.Forms
{
	public class GradeableTaskForm : Form
	{
		Course course =			null;
		GradeableTask task =	null;

		#region Form elements
		FlowLayoutPanel flowLayoutPanel;

		Label nameLabel;
		TextBox nameTextBox;

		Label dueDateLabel;
		DateTimePicker dueDatePicker;

		Label potentialMarksLabel;
		TextBox potentialMarksTextBox;

		Label weightLabel;
		TextBox weightTextBox;

		Button submitButton;
		#endregion

		private GradeableTaskForm()
		{
			InitalizeFlowLayoutPanel();
			InitializeFormFields();
			InitializeButtons();
		}

		public GradeableTaskForm(Course course) : this()
		{
			Text = "Add Task";

			this.course = course;
		}

		public GradeableTaskForm(GradeableTask task) : this()
		{
			Text = "Edit Task";

			this.task = task;

			nameTextBox.Text =				task.Name;
			dueDatePicker.Value =			task.DueDate;
			potentialMarksTextBox.Text =	task.PotentialMarks.ToString();
			weightTextBox.Text =			task.Weight.ToString();
		}

		/// <summary>
		/// Initalizes the flow layout panel.
		/// </summary>
		private void InitalizeFlowLayoutPanel()
		{
			flowLayoutPanel = new FlowLayoutPanel() {
				FlowDirection = FlowDirection.TopDown,
				Dock =			DockStyle.Fill
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

			FlowLayoutPanel dueDatePanel = new FlowLayoutPanel() {
				AutoSize = true
			};

			dueDateLabel = new Label() {
				Text = "Due Date",
				Anchor = AnchorStyles.Left
			};

			dueDatePicker = new DateTimePicker() {};

			dueDatePanel.Controls.Add(dueDateLabel);
			dueDatePanel.Controls.Add(dueDatePicker);

			FlowLayoutPanel endDatePanel = new FlowLayoutPanel() {
				AutoSize = true
			};

			FlowLayoutPanel potentialMarksPanel = new FlowLayoutPanel() {
				AutoSize = true
			};

			potentialMarksLabel = new Label() {
				Text = "Potential Marks",
				Anchor = AnchorStyles.Left
			};

			potentialMarksTextBox = new TextBox() {};

			potentialMarksPanel.Controls.Add(potentialMarksLabel);
			potentialMarksPanel.Controls.Add(potentialMarksTextBox);

			FlowLayoutPanel weightPanel = new FlowLayoutPanel() {
				AutoSize = true
			};

			weightLabel = new Label() {
				Text = "Weight",
				Anchor = AnchorStyles.Left
			};

			weightTextBox = new TextBox() {};

			weightPanel.Controls.Add(weightLabel);
			weightPanel.Controls.Add(weightTextBox);

			flowLayoutPanel.Controls.Add(namePanel);
			flowLayoutPanel.Controls.Add(dueDatePanel);
			flowLayoutPanel.Controls.Add(potentialMarksPanel);
			flowLayoutPanel.Controls.Add(weightPanel);

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
		/// Handles the form's submit action.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void SubmitButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show(this, "Submit button clicked", "Task Submit",
				MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}

