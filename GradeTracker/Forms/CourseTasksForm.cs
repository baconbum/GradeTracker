using System;
using System.Drawing;
using System.Windows.Forms;
using GradeTracker.Data;

namespace GradeTracker.Forms
{
	public class CourseTasksForm : Form
	{
		Course course;

		#region Form elements
		private Button addNewTaskButton;
		private DataGridView tasksGrid;
		#endregion

		public CourseTasksForm(Course course)
		{
			this.course = course;

			Text = String.Format("Course Tasks: {0}", this.course.Name);
			MinimumSize = new Size(600, 600);

			InitializeAddNewTaskButton();
			InitializeTasksGrid();
		}

		/// <summary>
		/// Initializes the Add New Task button.
		/// </summary>
		private void InitializeAddNewTaskButton()
		{
			addNewTaskButton = new Button() {
				Text =		"Add New Task",
				Width =		200,
				Location =	new Point(0, 10)
			};
			addNewTaskButton.Click += new EventHandler(AddNewTaskButton_Click);
			Controls.Add(addNewTaskButton);
		}

		/// <summary>
		/// Handles the Add New Task button's click event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void AddNewTaskButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show(this, "Add new task button clicked.", "Add New Task",
				MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		/// <summary>
		/// Initializes the Tasks grid.
		/// </summary>
		private void InitializeTasksGrid()
		{
			tasksGrid = new DataGridView() {
				ReadOnly =				true,
				AllowUserToAddRows =	false,
				Location =	new Point(addNewTaskButton.Left, addNewTaskButton.Height + addNewTaskButton.Top + 10),
				Anchor =	AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right
			};

			tasksGrid.Columns.Add(new DataGridViewTextBoxColumn(){ HeaderText = "Task" });
			tasksGrid.Columns.Add(new DataGridViewTextBoxColumn(){ HeaderText = "Due Date" });
			tasksGrid.Columns.Add(new DataGridViewTextBoxColumn(){ HeaderText = "Marks" });
			tasksGrid.Columns.Add(new DataGridViewTextBoxColumn(){ HeaderText = "Weight" });

			Controls.Add(tasksGrid);
		}
	}
}
