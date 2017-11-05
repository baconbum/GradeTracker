using System;
using System.Drawing;
using System.Windows.Forms;
using GradeTracker.UserControls;

namespace GradeTracker.Forms
{
	public class GradeTrackerForm : Form
	{
		#region Class variables and properties
		private MenuStrip menuStrip;
		private ToolStripMenuItem fileMenuItem;
		private ToolStripMenuItem exitMenuItem;
		private ToolStripMenuItem windowMenuItem;
		private ToolStripMenuItem splashMenuItem;
		private ToolStripMenuItem coursesMenuItem;
		private ToolStripMenuItem studentsMenuItem;

		private TabControl gradeTrackerTabs;
		private TabPage splashTab;
		private TabPage coursesTab;
		private TabPage studentsTab;

		private SplashUserControl splashControl;
		private CoursesUserControl coursesControl;
		private StudentsUserControl studentsControl;
		#endregion

		public GradeTrackerForm()
		{
			InitializeMenuStrip();
			InitializeSplashTab();
			InitializeCoursesTab();
			InitializeStudentsTab();
			InitializeGradeTrackersTabs();
		}

		private void InitializeMenuStrip()
		{
			menuStrip = new MenuStrip();

			#region Initialize file drop down
			fileMenuItem = new ToolStripMenuItem() {
				Text = "File"
			};

			exitMenuItem = new ToolStripMenuItem() {
				Text = "Exit"
			};

			exitMenuItem.Click += ExitProgram;

			menuStrip.Items.Add(fileMenuItem);

			fileMenuItem.DropDownItems.Add(exitMenuItem);
			#endregion

			#region Initialize window drop down
			windowMenuItem = new ToolStripMenuItem() {
				Text = "Window"
			};

			splashMenuItem = new ToolStripMenuItem() {
				Text = "Home"
			};

			coursesMenuItem = new ToolStripMenuItem() {
				Text = "Courses"
			};

			studentsMenuItem = new ToolStripMenuItem() {
				Text = "Students"
			};

			splashMenuItem.Click += ShowSplashTab;
			coursesMenuItem.Click += ShowCoursesTab;
			studentsMenuItem.Click += ShowStudentsTab;

			windowMenuItem.DropDownItems.Add(splashMenuItem);
			windowMenuItem.DropDownItems.Add(coursesMenuItem);
			windowMenuItem.DropDownItems.Add(studentsMenuItem);

			menuStrip.Items.Add(windowMenuItem);
			#endregion

			Controls.Add(menuStrip);
		}

		private void InitializeSplashTab()
		{
			splashTab = new TabPage() {
				Text = "Home",
				Dock = DockStyle.Fill
			};

			splashControl = new SplashUserControl() {
				Dock = DockStyle.Fill
			};

			splashControl.CoursesButtonClicked += ShowCoursesTab;
			splashControl.StudentsButtonClicked += ShowStudentsTab;

			splashTab.Controls.Add(splashControl);
		}

		private void InitializeCoursesTab()
		{
			coursesTab = new TabPage() {
				Text = "Courses",
				Dock = DockStyle.Fill
			};

			coursesControl = new CoursesUserControl() {
				Dock = DockStyle.Fill
			};

			coursesControl.HomeButtonClicked += ShowSplashTab;

			coursesTab.Controls.Add(coursesControl);
		}

		private void InitializeStudentsTab()
		{
			studentsTab = new TabPage() {
				Text = "Students",
				Dock = DockStyle.Fill
			};

			studentsControl = new StudentsUserControl() {
				Dock = DockStyle.Fill
			};

			studentsControl.HomeButtonClicked += ShowSplashTab;

			studentsTab.Controls.Add(studentsControl);
		}

		private void InitializeGradeTrackersTabs()
		{
			gradeTrackerTabs = new TabControl() {
				Dock = DockStyle.Fill
			};

			gradeTrackerTabs.TabPages.Add(splashTab);
			gradeTrackerTabs.TabPages.Add(coursesTab);
			gradeTrackerTabs.TabPages.Add(studentsTab);

			Controls.Add(gradeTrackerTabs);
		}

		private void ExitProgram (object sender, EventArgs e)
		{
			Close();
		}

		private void ShowSplashTab (object sender, EventArgs e)
		{
			gradeTrackerTabs.SelectedIndex = 0;
		}

		private void ShowCoursesTab (object sender, EventArgs e)
		{
			coursesControl.Refresh();
			gradeTrackerTabs.SelectedIndex = 1;
		}

		private void ShowStudentsTab (object sender, EventArgs e)
		{
			studentsControl.Refresh();
			gradeTrackerTabs.SelectedIndex = 2;
		}
	}
}