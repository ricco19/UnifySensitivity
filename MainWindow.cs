using System;
using System.Windows.Forms;

namespace UnifySensitivity {
	public partial class MainWindow : Form {
		public MainWindow() {
			InitializeComponent();
			NumBoxOWSense.MouseEnter += new EventHandler(NumBoxOWSense_MouseEnter);
			NumBoxSourceSense.MouseEnter += new EventHandler(NumBoxSourceSense_MouseEnter);
			NumBoxSourceYaw.MouseEnter += new EventHandler(NumBoxSourceYaw_MouseEnter);
			NumBoxPUBGSense.MouseEnter += new EventHandler(NumBoxPUBGSense_MouseEnter);
			NumBoxPUBGConverted.MouseEnter += new EventHandler(NumBoxPUBGConverted_MouseEnter);
			NumBoxPUBGFOV.MouseEnter += new EventHandler(NumBoxPUBGFOV_MouseEnter);
			NumBoxDPI.MouseEnter += new EventHandler(NumBoxDPI_MouseEnter);
			NumBoxCmPer360.MouseEnter += new EventHandler(NumBoxCmPer360_MouseEnter);
			NumBoxInPer360.MouseEnter += new EventHandler(NumBoxInPer360_MouseEnter);
			NumBoxDePerCm.MouseEnter += new EventHandler(NumBoxDePerCm_MouseEnter);
			NumBoxDePerIn.MouseEnter += new EventHandler(NumBoxDePerIn_MouseEnter);
			// Initalize to source sense 3.0
			NumBoxSourceSense.Value = 3.0M;
			decimal cm_per_rotation = 914.4M / (NumBoxSourceSense.Value * NumBoxSourceYaw.Value * NumBoxDPI.Value);
			try {
				SetMeasurementsFromCmPer(cm_per_rotation);
				SetOWFromCurrentMeasurements();
				SetPUBGConvFromCurrentMeasurements();
				SetPUBGSenseFromCurrentMeasurements();
			} catch (ArgumentOutOfRangeException) {
				MessageBox.Show("Went out of range you pleblord.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void SetMeasurementsFromCmPer(decimal cm_per_rotation) {
			decimal in_per_rotation = cm_per_rotation / 2.54M;
			try {
				NumBoxCmPer360.Value = cm_per_rotation;
				NumBoxInPer360.Value = in_per_rotation;
				NumBoxDePerCm.Value = 360 / cm_per_rotation;
				NumBoxDePerIn.Value = 360 / in_per_rotation;
			} catch (ArgumentOutOfRangeException) {
				MessageBox.Show("Went out of range you pleblord.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		// Updater functions
		private void SetSourceFromCurrentMeasurements() {
			NumBoxSourceSense.Value = 914.4M / (NumBoxCmPer360.Value * NumBoxDPI.Value * NumBoxSourceYaw.Value);
		}
		private void SetOWFromCurrentMeasurements() {
			NumBoxOWSense.Value = 138544 / (NumBoxCmPer360.Value * NumBoxDPI.Value);
		}
		private void SetPUBGConvFromCurrentMeasurements() {
			decimal m = NumBoxCmPer360.Value * NumBoxDPI.Value * NumBoxPUBGFOV.Value;
			NumBoxPUBGConverted.Value = 32918.4M / m;
		}
		private void SetPUBGSenseFromCurrentMeasurements() {
			double m = Convert.ToDouble(NumBoxCmPer360.Value * NumBoxDPI.Value * NumBoxPUBGFOV.Value);
			NumBoxPUBGSense.Value = Convert.ToDecimal(21.7147 * Math.Log(16459200 / m));
		}

		// OVERWATCH
		private void NumBoxOWSense_ValueChanged(object sender, EventArgs e) {
			if (NumBoxOWSense != ActiveControl) {
				return;
			}
			decimal cm_per_rotation = 138544 / (NumBoxOWSense.Value * NumBoxDPI.Value);
			try {
				SetMeasurementsFromCmPer(cm_per_rotation);
				SetSourceFromCurrentMeasurements();
				SetPUBGConvFromCurrentMeasurements();
				SetPUBGSenseFromCurrentMeasurements();
			} catch (ArgumentOutOfRangeException) {
				MessageBox.Show("Went out of range you pleblord.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		// SOURCE/QUAKE ENGINE
		private void NumBoxSourceSense_ValueChanged(object sender, EventArgs e) {
			if (NumBoxSourceSense != ActiveControl) {
				return;
			}
			decimal cm_per_rotation = 914.4M / (NumBoxSourceSense.Value * NumBoxSourceYaw.Value * NumBoxDPI.Value);
			try {
				SetMeasurementsFromCmPer(cm_per_rotation);
				SetOWFromCurrentMeasurements();
				SetPUBGConvFromCurrentMeasurements();
				SetPUBGSenseFromCurrentMeasurements();
			} catch (ArgumentOutOfRangeException) {
				MessageBox.Show("Went out of range you pleblord.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
		private void NumBoxSourceYaw_ValueChanged(object sender, EventArgs e) {
			if (NumBoxSourceYaw != ActiveControl) {
				return;
			}
			decimal cm_per_rotation = 914.4M / (NumBoxSourceSense.Value * NumBoxSourceYaw.Value * NumBoxDPI.Value);
			try {
				SetMeasurementsFromCmPer(cm_per_rotation);
				SetOWFromCurrentMeasurements();
				SetPUBGConvFromCurrentMeasurements();
				SetPUBGSenseFromCurrentMeasurements();
			} catch (ArgumentOutOfRangeException) {
				MessageBox.Show("Went out of range you pleblord.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		// PUBG
		private void NumBoxPUBGSense_ValueChanged(object sender, EventArgs e) {
			if (NumBoxPUBGSense != ActiveControl) {
				return;
			}
			double m = 16459200 * Math.Pow(2.71828, -0.0460518 * Convert.ToDouble(NumBoxPUBGSense.Value));
			decimal cm_per_rotation = Convert.ToDecimal(m) / (NumBoxDPI.Value * NumBoxPUBGFOV.Value);
			try {
				SetMeasurementsFromCmPer(cm_per_rotation);
				SetSourceFromCurrentMeasurements();
				SetOWFromCurrentMeasurements();
				SetPUBGConvFromCurrentMeasurements();
			} catch (ArgumentOutOfRangeException) {
				MessageBox.Show("Went out of range you pleblord.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
		private void NumBoxPUBGConverted_ValueChanged(object sender, EventArgs e) {
			if (NumBoxPUBGConverted != ActiveControl) {
				return;
			}
			decimal m = NumBoxPUBGConverted.Value * NumBoxDPI.Value * NumBoxPUBGFOV.Value;
			decimal cm_per_rotation = 32918.4M / m;
			try {
				SetMeasurementsFromCmPer(cm_per_rotation);
				SetSourceFromCurrentMeasurements();
				SetOWFromCurrentMeasurements();
				SetPUBGSenseFromCurrentMeasurements();
			} catch (ArgumentOutOfRangeException) {
				MessageBox.Show("Went out of range you pleblord.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
		private void NumBoxPUBGFOV_ValueChanged(object sender, EventArgs e) {
			if (NumBoxPUBGFOV != ActiveControl) {
				return;
			}
			decimal m = NumBoxPUBGConverted.Value * NumBoxDPI.Value * NumBoxPUBGFOV.Value;
			decimal cm_per_rotation = 32918.4M / m;
			try {
				SetMeasurementsFromCmPer(cm_per_rotation);
				SetSourceFromCurrentMeasurements();
				SetOWFromCurrentMeasurements();
			} catch (ArgumentOutOfRangeException) {
				MessageBox.Show("Went out of range you pleblord.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		// MOUSE SETTINGS		
		private void NumBoxDPI_ValueChanged(object sender, EventArgs e) {
			if (NumBoxDPI != ActiveControl) {
				return;
			}
			decimal cm_per_rotation = 914.4M / (NumBoxSourceSense.Value * NumBoxSourceYaw.Value * NumBoxDPI.Value);
			try {
				SetMeasurementsFromCmPer(cm_per_rotation);
			} catch (ArgumentOutOfRangeException) {
				MessageBox.Show("Went out of range you pleblord.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		// MEASUREMENTS 
		private void NumBoxCmPer360_ValueChanged(object sender, EventArgs e) {
			if (NumBoxCmPer360 != ActiveControl) {
				return;
			}
			decimal cm_per_rotation = NumBoxCmPer360.Value;
			decimal in_per_rotation = cm_per_rotation / 2.54M;
			try {
				NumBoxInPer360.Value = in_per_rotation;
				NumBoxDePerCm.Value = 360 / cm_per_rotation;
				NumBoxDePerIn.Value = 360 / in_per_rotation;
				SetSourceFromCurrentMeasurements();
				SetOWFromCurrentMeasurements();
				SetPUBGSenseFromCurrentMeasurements();
				SetPUBGConvFromCurrentMeasurements();
			} catch (ArgumentOutOfRangeException) {
				MessageBox.Show("Went out of range you pleblord.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
		private void NumBoxInPer360_ValueChanged(object sender, EventArgs e) {
			if (NumBoxInPer360 != ActiveControl) {
				return;
			}
			decimal in_per_rotation = NumBoxInPer360.Value;
			decimal cm_per_rotation = in_per_rotation * 2.54M;
			try {
				NumBoxCmPer360.Value = cm_per_rotation;
				NumBoxDePerCm.Value = 360 / cm_per_rotation;
				NumBoxDePerIn.Value = 360 / in_per_rotation;
				SetSourceFromCurrentMeasurements();
				SetOWFromCurrentMeasurements();
				SetPUBGSenseFromCurrentMeasurements();
				SetPUBGConvFromCurrentMeasurements();
			} catch (ArgumentOutOfRangeException) {
				MessageBox.Show("Went out of range you pleblord.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
		private void NumBoxDePerCm_ValueChanged(object sender, EventArgs e) {
			if (NumBoxDePerCm != ActiveControl) {
				return;
			}
			decimal cm_per_rotation =  360 / NumBoxDePerCm.Value;
			decimal in_per_rotation = cm_per_rotation / 2.54M;
			try {
				NumBoxCmPer360.Value = cm_per_rotation;
				NumBoxInPer360.Value = in_per_rotation;
				NumBoxDePerIn.Value = 360 / in_per_rotation;
				SetSourceFromCurrentMeasurements();
				SetOWFromCurrentMeasurements();
				SetPUBGSenseFromCurrentMeasurements();
				SetPUBGConvFromCurrentMeasurements();
			} catch (ArgumentOutOfRangeException) {
				MessageBox.Show("Went out of range you pleblord.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
		private void NumBoxDePerIn_ValueChanged(object sender, EventArgs e) {
			if (NumBoxDePerIn != ActiveControl) {
				return;
			}
			decimal in_per_rotation = 360 / NumBoxDePerIn.Value;
			decimal cm_per_rotation = in_per_rotation * 2.54M;
			try {
				NumBoxCmPer360.Value = cm_per_rotation;
				NumBoxInPer360.Value = in_per_rotation;
				NumBoxDePerCm.Value = 360 / cm_per_rotation;
				SetSourceFromCurrentMeasurements();
				SetOWFromCurrentMeasurements();
				SetPUBGSenseFromCurrentMeasurements();
				SetPUBGConvFromCurrentMeasurements();
			} catch (ArgumentOutOfRangeException) {
				MessageBox.Show("Went out of range you pleblord.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		// Mouse hover events
		private void NumBoxOWSense_MouseEnter(object sender, EventArgs e) {
			statusText.Text = "Overwatch in game sensitivity value.";
		}
		private void NumBoxSourceSense_MouseEnter(object sender, EventArgs e) {
			statusText.Text = "Quake engine based games (Source, CSGO, ect) game sensitivity value.";
		}
		private void NumBoxSourceYaw_MouseEnter(object sender, EventArgs e) {
			statusText.Text = "m_yaw value in quake engine games, typically left at 0.022.";
		}
		private void NumBoxPUBGSense_MouseEnter(object sender, EventArgs e) {
			statusText.Text = "PUBG in game sensitivity value (0-100).";
		}
		private void NumBoxPUBGConverted_MouseEnter(object sender, EventArgs e) {
			statusText.Text = "PUBG config file 'converted' value.";
		}
		private void NumBoxPUBGFOV_MouseEnter(object sender, EventArgs e) {
			statusText.Text = "PUBG FOV setting, TPP is set to 80, FPP has a slider from 80-103.";
		}
		private void NumBoxDPI_MouseEnter(object sender, EventArgs e) {
			statusText.Text = "The hardware DPI of your mouse (ie 400, 800, 1600).";
		}
		private void NumBoxCmPer360_MouseEnter(object sender, EventArgs e) {
			statusText.Text = "The number of centimeters it takes to do a full 360 degree turn in game.";
		}
		private void NumBoxInPer360_MouseEnter(object sender, EventArgs e) {
			statusText.Text = "The number of inches it takes to do a full 360 degree turn in game.";
		}
		private void NumBoxDePerCm_MouseEnter(object sender, EventArgs e) {
			statusText.Text = "The number of degrees you turn in game over one centimeter of mouse movement.";
		}
		private void NumBoxDePerIn_MouseEnter(object sender, EventArgs e) {
			statusText.Text = "The number of degrees you turn in game over one inch of mouse movement.";
		}


	}
}
