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
		TableLayoutPanel tableLayoutPanel;

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
			InitializeTableLayoutPanel();
			InitializeFormFields();
			InitializeButtons();

			Size =				new Size(400, 200);
			FormBorderStyle =	FormBorderStyle.FixedSingle;
			MaximizeBox =		false;
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
		/// Initializes the table layout panel.
		/// </summary>
		private void InitializeTableLayoutPanel()
		{
			tableLayoutPanel = new TableLayoutPanel() {
				Dock =	DockStyle.Fill,
				RowCount = 5,
				ColumnCount = 2
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

			dueDateLabel = new Label() {
				Text = "Due Date",
				Anchor = AnchorStyles.Left
			};

			dueDatePicker = new DateTimePicker() {
				Anchor = AnchorStyles.Left | AnchorStyles.Right
			};

			tableLayoutPanel.Controls.Add(dueDateLabel, 0, 1);
			tableLayoutPanel.Controls.Add(dueDatePicker, 1, 1);

			potentialMarksLabel = new Label() {
				Text = "Potential Marks",
				Anchor = AnchorStyles.Left
			};

			potentialMarksTextBox = new TextBox() {
				Anchor = AnchorStyles.Left | AnchorStyles.Right
			};

			tableLayoutPanel.Controls.Add(potentialMarksLabel, 0, 2);
			tableLayoutPanel.Controls.Add(potentialMarksTextBox, 1, 2);

			weightLabel = new Label() {
				Text = "Weight",
				Anchor = AnchorStyles.Left
			};

			weightTextBox = new TextBox() {
				Anchor = AnchorStyles.Left | AnchorStyles.Right
			};

			tableLayoutPanel.Controls.Add(weightLabel, 0, 3);
			tableLayoutPanel.Controls.Add(weightTextBox, 1, 3);

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

			tableLayoutPanel.Controls.Add(submitButton, 0, 4);

			tableLayoutPanel.SetColumnSpan(submitButton, 2);
		}

		/// <summary>
		/// Resets the form fields to their default values.
		/// </summary>
		private void ResetForm()
		{
			nameTextBox.Text =				String.Empty;
			dueDatePicker.Value =			DateTime.Now;
			potentialMarksTextBox.Text =	String.Empty;
			weightTextBox.Text =			String.Empty;
		}

		/// <summary>
		/// Validates the form fields.
		/// </summary>
		/// <returns><c>true</c>, if form was validated, <c>false</c> otherwise.</returns>
		private bool ValidateForm()
		{
			if (String.IsNullOrWhiteSpace(nameTextBox.Text))
			{
				MessageBox.Show(this, "Name cannot be empty.", "Invalid Task",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				
				nameTextBox.Focus();
				return false;
			}

			double potentialMarks;

			if (String.IsNullOrWhiteSpace(potentialMarksTextBox.Text) ||
				!Double.TryParse(potentialMarksTextBox.Text, out potentialMarks))
			{
				MessageBox.Show(this, "Potential Marks is not a valid number.", "Invalid Task",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				potentialMarksTextBox.Focus();
				return false;
			}

			if (potentialMarks < 0)
			{
				MessageBox.Show(this, "Potential Marks cannot be less than 0.", "Invalid Task",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				potentialMarksTextBox.Focus();
				return false;
			}

			double weight;

			if (String.IsNullOrWhiteSpace(weightTextBox.Text) ||
				!Double.TryParse(weightTextBox.Text, out weight))
			{
				MessageBox.Show(this, "Weight is not a valid number.", "Invalid Task",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				weightTextBox.Focus();
				return false;
			}

			if (weight < 0 || weight > 100)
			{
				MessageBox.Show(this, "Weight must be between 0 and 100.", "Invalid Task",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				weightTextBox.Focus();
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
			DateTime dueDate =		dueDatePicker.Value;
			double potentialMarks =	Double.Parse(potentialMarksTextBox.Text);
			double weight =			Double.Parse(weightTextBox.Text);

			if (task == null && course != null)
			{
				if (course.AddGradeableTask(name, dueDate, potentialMarks, weight))
				{
					MessageBox.Show(this, String.Format("Added {0}", name),
						"Task Added", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					ResetForm();
				}
				else
				{
					MessageBox.Show(this, String.Format("Error adding task {0}", name),
						"Error Adding Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else if (task != null)
			{
				if (task.Edit(name, dueDate, potentialMarks, weight))
				{
					MessageBox.Show(this, String.Format("Updated {0}", name),
						"Task Updated", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					MessageBox.Show(this, String.Format("Error updating task {0}", name),
						"Error Updating Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}

