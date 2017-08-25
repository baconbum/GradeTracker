using System;
using System.Drawing;
using System.Windows.Forms;
using GradeTracker.UserControls;

namespace GradeTracker.Forms
{
	public class GradeTrackerForm : Form
	{
		private TabControl gradeTrackerTabs;
		private TabPage splashTab;
		private TabPage coursesTab;
		private TabPage studentsTab;

		private SplashUserControl splashControl;
		private CoursesUserControl coursesControl;
		private StudentsUserControl studentsControl;

		public GradeTrackerForm()
		{
			InitializeSplashTab();
			InitializeCoursesTab();
			InitializeStudentsTab();
			InitializeGradeTrackersTabs();
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

			splashControl.CoursesButtonClicked += Splash_CoursesButtonClicked;
			splashControl.StudentsButtonClicked += Splash_StudentsButtonClicked;

			splashTab.Controls.Add(splashControl);
		}

		private void Splash_CoursesButtonClicked (object sender, EventArgs e)
		{
			gradeTrackerTabs.SelectedIndex = 1;
		}

		private void Splash_StudentsButtonClicked (object sender, EventArgs e)
		{
			gradeTrackerTabs.SelectedIndex = 2;
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
	}
}
