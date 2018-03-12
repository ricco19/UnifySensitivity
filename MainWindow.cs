using System;
using System.Windows.Forms;

namespace UnifySensitivity {
    internal partial class MainWindow : Form {
        internal MainWindow() {
            InitializeComponent();
            NumBoxOWSense.MouseEnter += new EventHandler(NumBoxOWSense_MouseEnter);
            NumBoxSourceSense.MouseEnter += new EventHandler(NumBoxSourceSense_MouseEnter);
            NumBoxSourceYaw.MouseEnter += new EventHandler(NumBoxSourceYaw_MouseEnter);
            NumBoxPubgSense.MouseEnter += new EventHandler(NumBoxPubgSense_MouseEnter);
            NumBoxPubgConv.MouseEnter += new EventHandler(NumBoxPubgConv_MouseEnter);
            NumBoxPubgFov.MouseEnter += new EventHandler(NumBoxPubgFov_MouseEnter);
            NumBoxDPI.MouseEnter += new EventHandler(NumBoxDPI_MouseEnter);
            NumBoxCmPer360.MouseEnter += new EventHandler(NumBoxCmPer360_MouseEnter);
            NumBoxInPer360.MouseEnter += new EventHandler(NumBoxInPer360_MouseEnter);
            NumBoxDePerCm.MouseEnter += new EventHandler(NumBoxDePerCm_MouseEnter);
            NumBoxDePerIn.MouseEnter += new EventHandler(NumBoxDePerIn_MouseEnter);
        }
        /*
        All controls are calculated from cm/360 so:
          -> Calculate cm/360 from the _changed_ control
          -> Check active control to prevent infinite loop
          -> Update all other controls using the cm/360 value

        m_yaw, mouse dpi, and pubg fov are essenitally constants only changed by the user
        */
        private void UpdateAllNumBoxes(decimal cm) {
            // Constant short hands
            decimal dpi = NumBoxDPI.Value;
            decimal yaw = NumBoxSourceYaw.Value;
            decimal pfov = NumBoxPubgFov.Value;
            try {
                // Overwatch
                if (NumBoxOWSense != ActiveControl) {
                    NumBoxOWSense.Value = Overwatch.SenseFromCm(cm, dpi);
                }
                // Source
                if (NumBoxSourceSense != ActiveControl) {
                    NumBoxSourceSense.Value = Quake.SenseFromCm(cm, dpi, yaw);
                }
                // PUBG
                if (NumBoxPubgSense != ActiveControl) {
                    NumBoxPubgSense.Value = PUBG.SenseFromCm(cm, dpi, pfov);
                }
                // PUBG (Converted)
                if (NumBoxPubgConv != ActiveControl) {
                    NumBoxPubgConv.Value = PUBG.ConvFromCm(cm, dpi, pfov);
                }
                // Measurements      
                if (NumBoxCmPer360 != ActiveControl) {
                    NumBoxCmPer360.Value = cm;
                }
                if (NumBoxInPer360 != ActiveControl) {
                    NumBoxInPer360.Value = cm / 2.54M;
                }
                if (NumBoxDePerCm != ActiveControl) {
                    NumBoxDePerCm.Value = 360 / cm;
                }
                if (NumBoxDePerIn != ActiveControl) {
                    NumBoxDePerIn.Value = 914.4M / cm;
                }
            } catch (ArgumentOutOfRangeException) {
                MessageBox.Show(
                    "Went out of range you pleblord.",
                    "WARNING",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        // Box change events
        // OVERWATCH
        private void NumBoxOWSense_ValueChanged(object sender, EventArgs e) {
            if (NumBoxOWSense != ActiveControl) {
                return;
            }
            decimal cm = Overwatch.CmFromSense(
                NumBoxOWSense.Value, 
                NumBoxDPI.Value
            );
            UpdateAllNumBoxes(cm);
        }
        // SOURCE/QUAKE ENGINE
        private void NumBoxSourceSense_ValueChanged(object sender, EventArgs e) {
            if (NumBoxSourceSense != ActiveControl) {
                return;
            }
            decimal cm = Quake.CmFromSense(
                NumBoxSourceSense.Value, 
                NumBoxDPI.Value, 
                NumBoxSourceYaw.Value
            );
            UpdateAllNumBoxes(cm);
        }
        private void NumBoxSourceYaw_ValueChanged(object sender, EventArgs e) {
            if (NumBoxSourceYaw != ActiveControl) {
                return;
            }
            decimal cm = Quake.CmFromSense(
                NumBoxSourceSense.Value, 
                NumBoxDPI.Value, 
                NumBoxSourceYaw.Value
            );
            UpdateAllNumBoxes(cm);
        }
        // PUBG
        private void NumBoxPubgSense_ValueChanged(object sender, EventArgs e) {
            if (NumBoxPubgSense != ActiveControl) {
                return;
            }
            decimal p = PUBG.CmFromSense(
                NumBoxPubgSense.Value, 
                NumBoxDPI.Value, 
                NumBoxPubgFov.Value
            );
            UpdateAllNumBoxes(p);
        }
        private void NumBoxPubgConv_ValueChanged(object sender, EventArgs e) {
            if (NumBoxPubgConv != ActiveControl) {
                return;
            }
            decimal p = PUBG.CmFromConv(
                NumBoxPubgConv.Value,
                NumBoxDPI.Value,
                NumBoxPubgFov.Value
            );
            UpdateAllNumBoxes(p);
        }
        private void NumBoxPubgFov_ValueChanged(object sender, EventArgs e) {
            if (NumBoxPubgFov != ActiveControl) {
                return;
            }
            decimal p = PUBG.CmFromConv(
                NumBoxPubgConv.Value,
                NumBoxDPI.Value,
                NumBoxPubgFov.Value
            );
            UpdateAllNumBoxes(p);
        }
        // MOUSE SETTINGS
        private void NumBoxDPI_ValueChanged(object sender, EventArgs e) {
            if (NumBoxDPI != ActiveControl) {
                return;
            }
            // Just recalculate using Overwatch value
            decimal cm = Overwatch.CmFromSense(
                NumBoxOWSense.Value,
                NumBoxDPI.Value
            );
            UpdateAllNumBoxes(cm);
        }
        // MEASUREMENTS 
        private void NumBoxCmPer360_ValueChanged(object sender, EventArgs e) {
            if (NumBoxCmPer360 != ActiveControl) {
                return;
            }
            UpdateAllNumBoxes(NumBoxCmPer360.Value);
        }
        private void NumBoxInPer360_ValueChanged(object sender, EventArgs e) {
            if (NumBoxInPer360 != ActiveControl) {
                return;
            }
            UpdateAllNumBoxes(NumBoxInPer360.Value * 2.54M);
        }
        private void NumBoxDePerCm_ValueChanged(object sender, EventArgs e) {
            if (NumBoxDePerCm != ActiveControl) {
                return;
            }
            UpdateAllNumBoxes(360 / NumBoxDePerCm.Value);
        }
        private void NumBoxDePerIn_ValueChanged(object sender, EventArgs e) {
            if (NumBoxDePerIn != ActiveControl) {
                return;
            }
            UpdateAllNumBoxes(914.4M / NumBoxDePerIn.Value);
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
        private void NumBoxPubgSense_MouseEnter(object sender, EventArgs e) {
            statusText.Text = "PUBG in game sensitivity value (0-100).";
        }
        private void NumBoxPubgConv_MouseEnter(object sender, EventArgs e) {
            statusText.Text = "PUBG config file 'converted' value.";
        }
        private void NumBoxPubgFov_MouseEnter(object sender, EventArgs e) {
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
