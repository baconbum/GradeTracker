using System;
using System.Drawing;
using System.Windows.Forms;
using GradeTracker.Data;

namespace GradeTracker.Forms
{
	public class CourseGradeableTasksForm : Form
	{
		Course course;

		#region Form elements
		private Button addNewTaskButton;
		private DataGridView tasksGrid;
		#endregion

		/// <summary>
		/// Maps the columns of the Tasks grid.
		/// </summary>
		private enum GradeableTasksGridColumn {
			Edit = 4,
			Delete = 5
		};

		public CourseGradeableTasksForm(Course course)
		{
			this.course = course;

			Text = String.Format("Course Tasks: {0}", this.course.Name);
			MinimumSize = new Size(600, 600);

			InitializeAddNewTaskButton();
			InitializeTasksGrid();
			Refresh();
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
			new GradeableTaskForm(course).Show();
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

			tasksGrid.Columns.Add(new DataGridViewButtonColumn() {
				HeaderText =	"Edit",
				Text =			"Edit",
				UseColumnTextForButtonValue = true
			});

			tasksGrid.Columns.Add(new DataGridViewButtonColumn() {
				HeaderText =	"Delete",
				Text =			"Delete",
				UseColumnTextForButtonValue = true
			});

			tasksGrid.CellClick += TasksGrid_CellClick;

			Controls.Add(tasksGrid);
		}

		/// <summary>
		/// Handles the Task grid's click event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
		private void TasksGrid_CellClick (object sender, DataGridViewCellEventArgs e)
		{
			DataGridViewRow row = tasksGrid.Rows[e.RowIndex];
			GradeableTask task = (GradeableTask)row.Tag;

			switch (e.ColumnIndex)
			{
				case (int)GradeableTasksGridColumn.Edit:
					new GradeableTaskForm(task).Show();
					break;
				case (int)GradeableTasksGridColumn.Delete:
					switch (MessageBox.Show(this, String.Format("Are you sure you want to delete {0}", task.Name),
						"Delete Task", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
					{
						case DialogResult.OK:
							task.Delete();
							break;
						default:
							break;
					}
					Refresh();
					break;
			}
		}

		/// <summary>
		/// Refresh the form, and ensure the Tasks grid is up to date.
		/// </summary>
		public override void Refresh()
		{
			base.Refresh();

			tasksGrid.Rows.Clear();

			foreach(GradeableTask task in course.GetTasks())
			{
				DataGridViewRow row = new DataGridViewRow(){ Tag = task };

				row.Cells.Add(new DataGridViewTextBoxCell(){ Value = task.Name });
				row.Cells.Add(new DataGridViewTextBoxCell(){ Value = task.DueDate.ToShortDateString() });
				row.Cells.Add(new DataGridViewTextBoxCell(){ Value = task.PotentialMarks.ToString() });
				row.Cells.Add(new DataGridViewTextBoxCell(){ Value = String.Format("{0}%", task.Weight.ToString()) });

				tasksGrid.Rows.Add(row);
			}
		}
	}
}
