﻿using System;
using System.Drawing;
using System.Windows.Forms;
using GradeTracker.Data;

namespace GradeTracker.Forms
{
	public class StudentForm : Form
	{
		Student student = null;

		FlowLayoutPanel flowLayoutPanel;

		Label firstNameLabel;
		TextBox firstNameTextBox;

		Label lastNameLabel;
		TextBox lastNameTextBox;

		Button submitButton;

		public StudentForm()
		{
			Text = "Add Student";

			InitalizeFlowLayoutPanel();
			CreateFormFields();
			CreateButtons();
		}

		public StudentForm(Student student)
		{
			Text = "Edit Student";

			this.student = student;

			InitalizeFlowLayoutPanel();
			CreateFormFields();
			CreateButtons();

			firstNameTextBox.Text = student.FirstName;
			lastNameTextBox.Text = student.LastName;
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
			firstNameTextBox.Text = String.Empty;
			lastNameTextBox.Text = String.Empty;
		}

		private void SubmitButtonClicked(object sender, EventArgs e)
		{
			if (student == null)
			{
				if (Student.AddStudentToDatabase(firstNameTextBox.Text, lastNameTextBox.Text))
				{
					MessageBox.Show(String.Format("Added {0}, {1}", firstNameTextBox.Text, lastNameTextBox.Text));
					ResetForm();
				}
				else
				{
					MessageBox.Show("Error adding student");
				}
			}
			else
			{
				MessageBox.Show(String.Format("Editing student {0}, {1}", student.LastName, student.FirstName));
			}
		}
	}
}