using System;
using System.Drawing;
using System.Windows.Forms;
using GradeTracker.UserControls;

namespace GradeTracker.Forms
{
	/// <summary>
	/// The main form of the Grade Tracker application.
	/// </summary>
	public class GradeTrackerForm : Form
	{
		#region Form elements
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
		private CourseDashboardUserControl courseDashboardControl;
		private StudentDashboardUserControl studentDashboardControl;
		#endregion

		/// <summary>
		/// Maps the tabs of the Grade Tracker form.
		/// </summary>
		private enum GradeTrackerTabsIndex {
			Splash =	0,
			Courses =	1,
			Students =	2
		};

		/// <summary>
		/// Initializes a new instance of the <see cref="GradeTracker.Forms.GradeTrackerForm"/> class.
		/// </summary>
		public GradeTrackerForm()
		{
			Text = "Grade Tracker";
			MinimumSize = new Size(600, 400);

			InitializeMenuStrip();
			InitializeSplashTab();
			InitializeCoursesTab();
			InitializeStudentsTab();
			InitializeGradeTrackerTabs();
		}

		/// <summary>
		/// Initializes the menu strip.
		/// </summary>
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

		/// <summary>
		/// Initializes the splash tab.
		/// </summary>
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

		/// <summary>
		/// Initializes the courses tab.
		/// </summary>
		private void InitializeCoursesTab()
		{
			coursesTab = new TabPage() {
				Text = "Courses",
				Dock = DockStyle.Fill
			};

			courseDashboardControl = new CourseDashboardUserControl() {
				Dock = DockStyle.Fill
			};

			courseDashboardControl.HomeButtonClicked += ShowSplashTab;

			coursesTab.Controls.Add(courseDashboardControl);
		}

		/// <summary>
		/// Initializes the students tab.
		/// </summary>
		private void InitializeStudentsTab()
		{
			studentsTab = new TabPage() {
				Text = "Students",
				Dock = DockStyle.Fill
			};

			studentDashboardControl = new StudentDashboardUserControl() {
				Dock = DockStyle.Fill
			};

			studentDashboardControl.HomeButtonClicked += ShowSplashTab;

			studentsTab.Controls.Add(studentDashboardControl);
		}

		/// <summary>
		/// Initializes the grade tracker tabs.
		/// </summary>
		private void InitializeGradeTrackerTabs()
		{
			gradeTrackerTabs = new TabControl() {
				Dock = DockStyle.Fill
			};

			gradeTrackerTabs.TabPages.Add(splashTab);
			gradeTrackerTabs.TabPages.Add(coursesTab);
			gradeTrackerTabs.TabPages.Add(studentsTab);

			Controls.Add(gradeTrackerTabs);
		}

		/// <summary>
		/// Exits the program.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void ExitProgram (object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Shows the splash tab.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void ShowSplashTab (object sender, EventArgs e)
		{
			gradeTrackerTabs.SelectedIndex = (int)GradeTrackerTabsIndex.Splash;
		}

		/// <summary>
		/// Shows the courses tab.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void ShowCoursesTab (object sender, EventArgs e)
		{
			courseDashboardControl.Refresh();
			gradeTrackerTabs.SelectedIndex = (int)GradeTrackerTabsIndex.Courses;
		}

		/// <summary>
		/// Shows the students tab.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void ShowStudentsTab (object sender, EventArgs e)
		{
			studentDashboardControl.Refresh();
			gradeTrackerTabs.SelectedIndex = (int)GradeTrackerTabsIndex.Students;
		}
	}
}
